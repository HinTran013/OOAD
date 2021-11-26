using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class accountDTO
    {
        private string _staffID ;
        private string _userName;
        private string _password;

        public accountDTO()
        {
            this._staffID = null;
            this._userName = null;
            this._password = null;
        }

        public accountDTO(
            string staffID,
            string userName,
            string password
        )
        {
            this._staffID = staffID;
            this._userName = userName;
            this._password = password;
        }

        public string staffID { get => _staffID; set => _staffID = value; }
        public string userName { get => _userName; set => _userName = value; }
        public string password { get => _password; set => _password = value; }
    }
}
