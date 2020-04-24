using Cloudbass.Utilities.CustomException;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbassManager.Filters
{
    public class JobNotFoundExceptionFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is JobNotFoundException ex)
            {
                return error.WithMessage($"Job with id {ex.JobId} not found");
            }

            return error;
        }
    }
}
