using DocumentHandler.DTO;
using DocumentHandler.Interfaces;
using System.Diagnostics;

namespace DocumentHandler.Handlers
{
    public class ErrorHandler : IErrorHandler
    {
        List<ResumeMakerError> errors = new();

        public ErrorHandler()
        {

        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void AddError(Exception exception, StackTrace stackTrace)
        {
            StackFrame? myFrame = null;
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var f = stackTrace.GetFrame(i);
                var declaringType = f?.GetMethod()?.DeclaringType?.FullName ?? "";

                if (declaringType.StartsWith("DocumentHandler"))
                {
                    myFrame = f;
                    break;
                }
            }

            var frame = myFrame ?? stackTrace.GetFrame(0);

            string methodName = frame?.GetMethod()?.Name ?? "Unknown";
            string className = frame?.GetMethod()?.DeclaringType?.FullName ?? "Unknown";
            int lineNumber = frame?.GetFileLineNumber() ?? -1;
            string fileName = frame?.GetFileName() ?? "Unknown";

            ResumeMakerError newError = new ResumeMakerError()
            {
                MethodName = methodName,
                ClassName = className,
                Location = $"{className}.{methodName} in {fileName}",
                LineNumber = lineNumber,
                FileName = fileName,
                Message = exception.Message,
                Time = DateTime.Now
            };

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

        public void AddError(string errorMessage)
        {
            ResumeMakerError newError = new ResumeMakerError()
            {
                Message = errorMessage,
                Time = DateTime.Now
            };

            errors.Add(newError);
        }
    }
}
