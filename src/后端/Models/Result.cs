namespace icechat.api.Models
{
    public class Result<T>
    {
        public int Code { get; set; } // 业务状态码  0-成功  1-失败
        public string Message { get; set; } // 提示信息
        public T Data { get; set; } // 响应数据

        public Result()
        {
        }

        public Result(int code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

    }
    public class Result : Result<object>
    {
        public Result()
        {
        }

        public Result(int code, string message, object data) : base(code, message, data)
        {
        }

        public static Result<E> Success<E>(E data)
        {
            return new Result<E>(0, "操作成功", data);
        }

        // 快速返回操作成功响应结果
        public static Result Success()
        {
            return new Result(0, "操作成功", null);
        }

        public static Result Error(string message)
        {
            return new Result(1, message, null);
        }

        public static Result<E> Error<E>(string message, E data)
        {
            return new Result<E>(1, message, data);
        }
    }
}
