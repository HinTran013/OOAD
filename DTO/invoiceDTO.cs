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
        private string _customerAddress;
        private string _customerPhoneNo;
        private string _customerEmail;
        private string _customerRequestDetail;
        private bool? _isCompleted;
        private string _staffUsername;
        private invoiceDetailDTO[] _invoiceDetails;

        public invoiceDTO()
        {
            this._customerName = null;
            this._customerAddress = null;
            this._customerRequestDetail = null;
            this._staffUsername = null;
            this._isCompleted = null;
            this._customerPhoneNo = null;
            this._customerEmail = null;
        }

        public invoiceDTO(
            string customerName,
            string customerAddress,
            string customerPhoneNo,
            string customerEmail,
            string customerRequestDetail,
            string staffUsername,
            bool? isCompleted,
            invoiceDetailDTO[] invoiceDetails
        )
        {
            this._customerName = customerName;
            this._customerAddress = customerAddress;
            this._customerPhoneNo = customerPhoneNo;
            this._customerRequestDetail = customerRequestDetail;
            this._isCompleted = isCompleted;
            this._staffUsername = staffUsername;
            this._invoiceDetails = invoiceDetails;
            this._customerEmail = customerEmail;
        }

        public string customerAddress { get => _customerAddress; set => _customerAddress = value; }
        public string customerPhoneNo { get => _customerPhoneNo; set => _customerPhoneNo = value; }
        public string customerEmail { get => _customerEmail; set => _customerEmail = value; }
        public invoiceDetailDTO[] invoiceDetails { get => _invoiceDetails; set => _invoiceDetails = value; }
        public bool? isCompleted { get => _isCompleted; set => _isCompleted = value; }
        public string customerName { get => _customerName; set => _customerName = value; }
        public string customerRequestDetail { get => _customerRequestDetail; set => _customerRequestDetail = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
    }
}
