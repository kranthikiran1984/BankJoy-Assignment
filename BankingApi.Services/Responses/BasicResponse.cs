using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace BankingApi.Services.Responses
{
    public class BasicResponse : IResponse
    {
        public BasicResponse()
        {
            DALErrors = new List<DALError>();
            ValidationErrors = new List<ValidationFailure>();
            Exceptions = new List<Exception>();
        }
        public IEnumerable<DALError> DALErrors { get; set; }
        public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
        public bool WasSuccessful { get; set; }
        public IEnumerable<Exception> Exceptions { get; set; }
    }
}
