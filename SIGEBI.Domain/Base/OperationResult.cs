
namespace SIGEBI.Domain.Base
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic? Data { get; set; }
        public OperationResult() { }

        protected OperationResult(bool isSuccess, string message, dynamic? data = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public static OperationResult Success(string message, dynamic? data = null)
        {
            return new OperationResult(true, message, data);
        }
        public static OperationResult Failure(string message, List<System.ComponentModel.DataAnnotations.ValidationResult> validationResults)
        {
            return new OperationResult(false, message);
        }

        public static OperationResult Failure(string message)
        {
            return new OperationResult(false, message);

        }
    }
}
