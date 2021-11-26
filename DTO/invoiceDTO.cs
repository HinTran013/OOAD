using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class invoiceDTO
    {
        private string _customerName;
        private string _customerRequirement;
        private double _total;
        private string _staffID;
        private string _customerID;

        public invoiceDTO()
        {
            this._customerName = null;
            this._customerRequirement = null;
            this._total = -1;
            this._staffID = null;
            this._customerID = null;
        }

        public invoiceDTO(
            string customerName,
            string customerRequirement,
            double total,
            string staffID,
            string customerID
        )
        {
            this._customerName = customerName;
            this._customerRequirement = customerRequirement;
            this._total = total;
            this._staffID = staffID;
            this._customerID = customerID;
        }

        public string customerName { get => _customerName; set => _customerName = value; }
        public string customerRequirement { get => _customerRequirement; set => _customerRequirement = value; }
        public double total { get => _total; set => _total = value; }
        public string staffID { get => _staffID; set => _staffID = value; }
        public string customerID { get => _customerID; set => _customerID = value; }
    }
}
