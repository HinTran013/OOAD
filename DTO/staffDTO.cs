using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class staffDTO
    {
        private string _ID;
        private string _name;
        private string _birthDate;
        private Nullable<bool> _gender;
        private string _email;
        private string _phoneNumber;
        private string _salary;
        private string _type;

        public staffDTO()
        {
            this._ID = null;
            this._name = null;
            this._birthDate = null;
            this._gender = null;
            this._email = null;
            this._phoneNumber = null;
            this._salary = null;
            this._type = null;
        }

        //constructor with params
        public staffDTO(string ID,
        string name,
        string birthDate,
        Nullable<bool> gender,
        string email,
        string phoneNumber,
        string salary,
        string type)
        {
            this._ID = ID;
            this._name = name;
            this._birthDate = birthDate;
            this._gender = gender;
            this._email = email;
            this._phoneNumber = phoneNumber;
            this._salary = salary;
            this._type = type;
        }

        public string ID { get => _ID; set => _ID = value; }
        public string name { get => _name; set => _name = value; }
        public string birthDate { get => _birthDate; set => _birthDate = value; }
        public bool gender { get => (bool)_gender; set => _gender = value; }
        public string email { get => _email; set => _email = value; }
        public string phoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string salary { get => _salary; set => _salary = value; }
        public string type { get => _type; set => _type = value; }
    }
}
