using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class issueReportBUS
    {
        static issueReportDAL issue = new issueReportDAL();

        public static bool AddNewIssueReport(issueReportDTO newIssue)
        {
            return issue.InserNewIssueReportRecord(newIssue);
        }
        public static long CountAllIssueReport()
        {
            return issue.CountAllIssueReport();
        }
        public static long CountAllUnsolvedIssues()
        {
            return issue.CountAllUnsolvedIssues();
        }
        public static long CountAllSolvedIssues()
        {
            return issue.CountAllSolvedIssues();
        }
        public static List<issueReportDTO> GetAllIssueReports()
        {
            return issue.GetAllIssueReports();
        }
    }
}
