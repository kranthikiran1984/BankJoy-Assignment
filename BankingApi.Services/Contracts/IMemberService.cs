using BankingApi.Core.Domain;
using BankingApi.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Services.Contracts
{
    public interface IMemberService
    {
        BasicResponse<List<Member>> GetAllMembers();
        BasicResponse<List<Member>> GetAllMembersByInstitution(int institutionId);
        BasicResponse<Member> GetMemberById(int memberId);
        Task<BasicResponse> AddMember(Member member, int institutionId);
        Task<BasicResponse<Member>> UpdateMember(Member member);
        Task<BasicResponse> DeleteMember(int memberId);
    }
}
