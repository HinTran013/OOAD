using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using MongoDB.Bson;

namespace BUS
{
    public class issueReportBUS
    {
        static issueReportDAL issue = new issueReportDAL();

        public static bool AddNewIssueReport(issueReportDTO newIssue)
        {
            return issue.InserNewIssueReportRecord(newIssue);
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
        public static bool UpdateStateByID(ObjectId id, bool newState)
        {
            return issue.UpdateStateByID(id, newState);
        }
    }
}
