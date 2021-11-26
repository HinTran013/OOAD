using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class issueBUS
    {
        static issueDAL issue = new issueDAL();

        public static bool AddNewIssue(issueDTO newIssue)
        {
            return issue.InsertNewIssueRecord(newIssue);
        }
    }
}
