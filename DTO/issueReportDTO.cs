using MongoDB.Bson;
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
        private string _staffUsername;
        private string _issueType;
        private string _description;
        private Nullable<bool> _isSolved;
        private ObjectId? _objectId;

        public issueReportDTO()
        {
            this._title = null;
            this._date = null;
            this._staffUsername = null;
            this._issueType = null;
            this._description = null;
            this._isSolved = null;
            this._objectId = null;
        }

        //constructor with params
        public issueReportDTO(
            string title,
            string date,
            string issueType,
            string description,
            string staffUsername,
            bool? isSolved,
            ObjectId? objectId = null
        )
        {
            this._title = title;
            this._date = date;
            this._staffUsername = staffUsername;
            this._issueType = issueType;
            this._description = description;
            this._isSolved = isSolved;
            this._objectId = objectId;
        }

        public string title { get => _title; set => _title = value; }
        public string date { get => _date; set => _date = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
        public string issueType { get => _issueType; set => _issueType = value; }
        public string description { get => _description; set => _description = value; }
        public bool? isSolved { get => _isSolved; set => _isSolved = value; }
        public ObjectId? objectId { get => _objectId; set => _objectId = value; }
    }
}
