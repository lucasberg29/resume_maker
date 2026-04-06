using DocumentHandler.DTO;
using DocumentHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHandler.Handlers
{
    public class ErrorHandler : IErrorHandler
    {
        List<ResumeMakerError> errors = new();

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void AddError(Exception exception)
        {
            ResumeMakerError newError = new ResumeMakerError(exception);
            errors.Add(newError);
        }

        public void ClearAllErrors()
        {
            errors.Clear();
        }

        public List<ResumeMakerError> GetErros()
        {
            List<ResumeMakerError> errorsCopy = new List<ResumeMakerError>(errors);
            errors.Clear();

            return errorsCopy;
        }


    }
}
