using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class issueDTO
    {
        private string _issueName;
        private string _issueDate;
        private string _issueType;
        private string _issueDescription;
        private string _staffUsername;
        private Nullable<bool> _isSolved;

        public issueDTO()
        {
            this._issueName = null;
            this._issueDate = null;
            this._issueType = null;
            this._issueDescription = null;
            this._staffUsername = null;
            this._isSolved = null;
        }

        public issueDTO(
            string issueName,
            string issueDate,
            string issueType,
            string issueDescription,
            string staffUsername,
            Nullable<bool> isSolved
        )
        {
            this._issueName = issueName;
            this._issueDate = issueDate;
            this._issueType = issueType;
            this._issueDescription = issueDescription;
            this._staffUsername = staffUsername;
            this._isSolved = isSolved;
        }

        public string issueName { get => _issueName; set => _issueName = value; }
        public string issueDate { get => _issueDate; set => _issueDate = value; }
        public string issueType { get => _issueType; set => _issueType = value; }
        public string issueDescription { get => _issueDescription; set => issueDescription = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
        public Nullable<bool> isSolved { get => _isSolved; set => _isSolved = value; }

    }
}
