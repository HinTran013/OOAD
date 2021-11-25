using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class issueReportDTO
    {
        private string _title;
        private string _date;
        private string _staffID;
        private string _issueType;
        private string _issueID;
        private string _description;

        public issueReportDTO()
        {
            this._title = null;
            this._date = null;
            this._staffID = null;
            this._issueType = null;
            this._issueID = null;
            this._description = null;
        }

        //constructor with params
        public issueReportDTO(
            string title,
            string date,
            string staffID,
            string issueType,
            string issueID,
            string description
        )
        {
            this._title = title;
            this._date = date;
            this._staffID = staffID;
            this._issueType = issueType;
            this._issueID = issueID;
            this._description = description;
        }

        public string title { get => _title; set => _title = value; }
        public string date { get => _date; set => _date = value; }
        public string staffID { get => _staffID; set => _staffID = value; }
        public string issueType { get => _issueType; set => _issueType = value; }
        public string issueID { get => _issueID; set => _issueID = value; }
        public string description { get => _description; set => _description = value; }
    }
}
