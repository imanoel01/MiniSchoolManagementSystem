using Report.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Concrete
{
    public interface IReportService
    {
      
        byte[] GenerateReportAsync(List<ExamResultsData> examResultData);
    }
}
