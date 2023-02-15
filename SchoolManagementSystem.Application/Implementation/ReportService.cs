using SchoolManagementSystem.Application.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.NETCore;
using Microsoft.AspNetCore.Hosting;
using Report.Datas;

namespace SchoolManagementSystem.Application.Implementation
{
    public class ReportService : IReportService
    {
        private readonly IWebHostEnvironment _environment;

        public ReportService(IWebHostEnvironment hostingEnvironment)
        {
            _environment = hostingEnvironment;
        }
        public byte[] GenerateReportAsync(List<ExamResultsData> examResultData)
        {
            
            var path = $"{_environment.WebRootPath}\\ReportFiles\\Report2.rdlc";


            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");
            LocalReport report = new LocalReport();
            report.ReportPath = path;
           
            report.DataSources.Add(new ReportDataSource("dsNewResult", examResultData));
            return report.Render("PDF");
        }

    }
}
