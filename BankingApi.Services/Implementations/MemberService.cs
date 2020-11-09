using BankingApi.Core.Domain;
using BankingApi.Data;
using BankingApi.Services.Contracts;
using BankingApi.Services.Events;
using BankingApi.Services.Responses;
using BankingApi.Services.Validations;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly IJsonRepository<Member> _memberRepository;
        private readonly MemberValidator _membervalidator;
        private readonly IEventPublisher _eventPublisher;

        public MemberService(IJsonRepository<Member> memberRepository, MemberValidator membervalidator, IEventPublisher eventPublisher)
        {
            _memberRepository = memberRepository;
            _membervalidator = membervalidator;
            _eventPublisher = eventPublisher;
        }

        public async Task<BasicResponse> AddMember(Member member, int institutionId)
        {
            var response = new BasicResponse();
            var validationResults = await _membervalidator.ValidateAsync(member, options => options.IncludeRuleSets("New Account Validation"));

            if(validationResults.IsValid)
            {
                member.Id = _memberRepository.GetNextKey();
                member.InstitutionId = institutionId;

                await _memberRepository.Insert(member);
                response.WasSuccessful = true;

                _eventPublisher.Publish(new MemberCreatedEvent(member));
            }

            return response;
        }

        public async Task<BasicResponse> DeleteMember(int memberId)
        {
            var response = new BasicResponse();
            await _memberRepository.Delete(memberId);
            response.WasSuccessful = true;

            return response;
        }

        public BasicResponse<List<Member>> GetAllMembers()
        {
            var response = new BasicResponse<List<Member>>();
            var members = _memberRepository.GetAll().ToList();
            response.WasSuccessful = true;
            response.Object = members;

            return response;
        }

        public BasicResponse<List<Member>> GetAllMembersByInstitution(int institutionId)
        {
            var response = new BasicResponse<List<Member>>();
            var members = _memberRepository.GetAll().Where(x => x.InstitutionId == institutionId).ToList();
            response.WasSuccessful = true;
            response.Object = members;

            return response;
        }

        public BasicResponse<Member> GetMemberById(int memberId)
        {
            var response = new BasicResponse<Member>();
            var member = _memberRepository.GetById(memberId);
            response.WasSuccessful = true;
            response.Object = member;

            return response;
        }

        public async Task<BasicResponse<Member>> UpdateMember(Member member)
        {
            //need to add validations prior to update
            var response = new BasicResponse<Member>();
            await _memberRepository.Update(member);

            response.Object = member;
            response.WasSuccessful = true;
            return response;
        }
    }
}
