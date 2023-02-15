using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Common.Models
{
    public class ResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ResponseModel Success(string? message = null)
        {
            return new ResponseModel()
            {
                IsSuccessful = true,
                Message = message ?? "Request was Successful"
            };
        }

        public static ResponseModel Failure(string? message = null) // , Dictionary<string, string> errors = null
        {
            return new ResponseModel()
            {
                Message = message ?? "Request was not completed",
                //Errors = errors
            };
        }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T? Data { get; set; }

        public static ResponseModel<T> Success(T data, string? message = null)
        {
            return new ResponseModel<T>()
            {
                IsSuccessful = true,
                Message = message ?? "Request was Successful",
                Data = data
            };
        }
        public static new ResponseModel<T> Failure(string? message = null)
        {
            return new ResponseModel<T>()
            {
                IsSuccessful = false,
                Message = message ?? "Request was not completed",
            };
        }
    }

    public class ResponseErrorModel : ResponseModel
    {
        public IDictionary<string, string[]>? Errors { get; set; }

        public static ResponseModel Failure(IDictionary<string, string[]>? errors = null, string? message = null)
        {
            return new ResponseErrorModel()
            {
                Message = message ?? "Request was not completed",
                Errors = errors ?? new Dictionary<string, string[]>()
            };
        }
    }
}
