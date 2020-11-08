using BankingApi.Core.Common;
using BankingApi.Core.Domain;
using BankingApi.Services.Model;
using BankingApi.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Services.Contracts
{
    public interface IInstitutionDataService
    {
        BasicResponse<IPagedList<Institution>> GetAllInstitutions(PagingRequestData pagingData = null);

        Task<BasicResponse> AddInstitution(Institution institution);
    }
}
