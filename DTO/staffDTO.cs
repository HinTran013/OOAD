using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class staffDTO
    {
        private string _name;
        private string _birthDate;
        private Nullable<bool> _gender;
        private string _email;
        private string _phoneNumber;
        private int _salary;
        private string _type;
        private string _description;
        private string _address;
        private string _username;
        private string _password;

        public staffDTO()
        {
            this._name = null;
            this._birthDate = null;
            this._gender = null;
            this._email = null;
            this._phoneNumber = null;
            this._salary = 0;
            this._description = null;
            this._address = null;
            this._type = null;
            this._username = null;
            this._password = null;
        }

        //constructor with params
        public staffDTO(
        string name,
        string birthDate,
        Nullable<bool> gender,
        string email,
        string phoneNumber,
        int salary,
        string address,
        string type,
        string description,
        string username,
        string password)
        {
            this._name = name;
            this._birthDate = birthDate;
            this._gender = gender;
            this._email = email;
            this._phoneNumber = phoneNumber;
            this._salary = salary;
            this._type = type;
            this._username = username;
            this._password = password;
            this._address = address;
            this._description = description;
        }

        public string name { get => _name; set => _name = value; }
        public string birthDate { get => _birthDate; set => _birthDate = value; }
        public bool gender { get => (bool)_gender; set => _gender = value; }
        public string email { get => _email; set => _email = value; }
        public string phoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public int salary { get => _salary; set => _salary = value; }
        public string type { get => _type; set => _type = value; }
        public string username { get => _username; set => _username = value; }
        public string password { get => _password; set => _password = value; }
        public string address { get => _address; set => _address = value; }
        public string description { get => _description; set => _description = value; }
    }
}
