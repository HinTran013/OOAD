using System;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class servicesDTO
    {
        private ObjectId? _serviceID;
        private string _name;
        private double _price;
        private string _description;

        public servicesDTO()
        {
            _serviceID = null;
            _name = null;
            _price = -1;
            _description = null;
        }

        public servicesDTO(ObjectId? serviceID, string name, double price, string description = "")
        {
            _serviceID = serviceID;
            _name = name;
            _price = price;
            _description = description;
        }


        public ObjectId? serviceID { get => _serviceID; set => _serviceID = value; }
        public double price { get => _price; set => _price = value; }
        public string description { get => _description; set => _description = value; }
        public string name { get => _name; set => _name = value; }
    }
}
