namespace VaccineDose
{

    public class Response<T>
    {
        public T ResponseData { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Response(bool status, string message, T data)
        {
            IsSuccess = status;
            Message = message;
            ResponseData = data;
        }

        //public static Response<T> Success(T data)
        //{
        //    return new Response<T> { Data = data, ResponseStatus = Status.Success };
        //}

        //public static Response<T> Error(string message)
        //{
        //    return new Response<T> { ResponseStatus = Status.Error, Message = message };
        //}
    }

}

