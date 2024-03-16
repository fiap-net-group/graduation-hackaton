﻿namespace Graduation.Hackaton.VideoProcessing.MVC.Models.Responses
{
    public class BaseResponseWithValue<T> : BaseResponse
    {
        public T Value { get; set; }

        public BaseResponseWithValue<T> AsError(T value, ResponseMessage? message = null, params string[] errors)
        {
            AsError(message, errors);
            Value = value;
            return this;
        }

        public new BaseResponseWithValue<T> AsError(ResponseMessage? message = null, params string[] errors)
        {
            errors ??= Array.Empty<string>();
            base.AsError(message, errors);
            return this;
        }

        public BaseResponseWithValue<T> AsSuccess(T value)
        {
            base.AsSuccess();
            Value = value;
            return this;
        }
    }
}
