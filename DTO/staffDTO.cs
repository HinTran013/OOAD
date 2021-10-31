using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class staffDTO
    {
        private string _ID;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private Nullable<bool> _gender;
        private string _email;
        private string _phoneNumber;
        private string _salary;

        public staffDTO()
        {
            this._ID = null;
            this._firstName = null;
            this._lastName = null;
            this._birthDate = new DateTime(0, 0, 0);
            this._gender = null;
            this._email = null;
            this._phoneNumber = null;
            this._salary = null;
        }

        //constructor with params
        public staffDTO (string ID,
        string firstName,
        string lastName,
        DateTime birthDate,
        Nullable<bool> gender,
        string email,
        string phoneNumber,
        string salary)
        {
            this._ID = ID;
            this._firstName = firstName;
            this._lastName = lastName;
            this._birthDate = birthDate;
            this._gender = gender;
            this._email = email;
            this._phoneNumber = phoneNumber;
            this._salary = salary;
        }

        public string ID { get => _ID; set => _ID = value; }
        public string firstName { get => _firstName; set => _firstName = value; }
        public string lastName { get => _lastName; set => _lastName = value; }
        public DateTime birthDate { get => _birthDate; set => _birthDate = value; }
        public bool gender { get => (bool)_gender; set => _gender = value; }
        public string email { get => _email; set => _email = value; }
        public string phoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string salary { get => _salary; set => _salary = value; }
    }
}
