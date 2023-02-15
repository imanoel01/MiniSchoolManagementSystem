using SchoolManagementSystem.Application.ViewModel;
using SchoolManagementSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Concrete
{
    public interface IExamService
    {
        Task<ResponseModel<byte[]>> GenerateAllResult();
        Task<ResponseModel> SubmitExamResult(SubmitExamResult request);
    }
}
