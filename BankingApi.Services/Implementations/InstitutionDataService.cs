using BankingApi.Core.Common;
using BankingApi.Core.Domain;
using BankingApi.Data;
using BankingApi.Services.Contracts;
using BankingApi.Services.Model;
using BankingApi.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankingApi.Services.Implementations
{
    public class InstitutionDataService : IInstitutionDataService
    {
        private readonly IJsonRepository<Institution> _institutionRepository;

        public InstitutionDataService(IJsonRepository<Institution> institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        public async Task<BasicResponse> AddInstitution(Institution institution)
        {
            var response = new BasicResponse();
            institution.Id = _institutionRepository.GetNextKey();
            await _institutionRepository.Insert(institution);
            response.WasSuccessful = true;

            return response;
        }

        public BasicResponse<IPagedList<Institution>> GetAllInstitutions(PagingRequestData pagingData=null)
        {
            var response = new BasicResponse<IPagedList<Institution>>();
            var query = _institutionRepository.GetAll();

            pagingData = pagingData ?? new PagingRequestData();
            var pagedList = new PagedList<Institution>(query, pagingData.PageIndex, pagingData.PageSize, pagingData.CountOnly);
            response.Object = pagedList;
            response.WasSuccessful = true;

            return response;
        }
    }
}
