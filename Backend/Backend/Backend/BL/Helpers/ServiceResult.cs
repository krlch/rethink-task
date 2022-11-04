using Backend.BL.Enums;

namespace Backend.BL.utils
{
   
    public class ServiceResult
    {
        public ServiceResultType Result { get; set; }

        public ServiceResult()
        {

        }
        public ServiceResult(ServiceResultType result)
        {
            Result = result;
        }

        public ServiceResult(ServiceResultType result, string message)
        {
            Result = result;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public ServiceResult()
        {
        }

        public ServiceResult(ServiceResultType result)
        {
            Result = result;
        }

        public ServiceResult(ServiceResultType result, string message)
        {
            Result = result;
        }

        public ServiceResult(ServiceResultType result, T data)
        {
            Result = result;
            Data = data;
        }

        public ServiceResult(ServiceResultType result, string message, T data)
        {
            Result = result;
            Data = data;
        }
    }
}
