using DocumentFormat.OpenXml.Presentation;
using DocumentHandler.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentHandler.Interfaces
{
    public interface IErrorHandler
    {
        public void Init();

        public void AddError(Exception exception);

        public void ClearAllErrors();

        public List<ResumeMakerError> GetErros();

    }
}
