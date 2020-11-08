using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Responses
{
    public interface IResponse
    {
        IEnumerable<DALError> DALErrors { get; set; }
        IEnumerable<ValidationFailure> ValidationErrors { get; set; }
        bool WasSuccessful { get; set; }
        IEnumerable<Exception> Exceptions { get; set; }
    }
}
