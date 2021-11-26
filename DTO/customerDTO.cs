using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class customerDTO
    {
        private string _name;
        private string _address;
        private string _phoneNumber;
        private string _email;

        public customerDTO()
        {
            this._name = null;
            this._address = null;
            this._phoneNumber = null;
            this._email = null;
        }

        public customerDTO(
            string name,
            string address,
            string phoneNumber,
            string email
        )
        {
            this._name = name;
            this._address = address;
            this._phoneNumber = phoneNumber;
            this._email = email;
        }

        public string name { get => _name; set => _name = value; }
        public string address { get => _address; set => _address = value; }
        public string phoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string email { get => _email; set => _email = value; }
    }
}
