﻿using BankingApi.Core.Domain;
using BankingApi.Data;
using BankingApi.Services.Contracts;
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

        public MemberService(IJsonRepository<Member> memberRepository, MemberValidator membervalidator)
        {
            _memberRepository = memberRepository;
            _membervalidator = membervalidator;
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

        public async Task<BasicResponse> UpdateMember(Member member)
        {
            var response = new BasicResponse();
            await _memberRepository.Update(member);

            response.WasSuccessful = true;
            return response;
        }
    }
}
