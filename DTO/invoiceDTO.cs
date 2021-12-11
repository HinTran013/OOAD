using MongoDB.Bson;
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
        private string _state;
        private string _staffUsername;
        private string _date;
        private List<invoiceDetailDTO> _invoiceDetails;
        private ObjectId? _objectId;

        public invoiceDTO()
        {
            this._customerName = null;
            this._customerAddress = null;
            this._customerRequestDetail = null;
            this._staffUsername = null;
            this._state = null;
            this._customerPhoneNo = null;
            this._customerEmail = null;
            this._date = null;
            this._objectId = null;
            this._invoiceDetails = null;
        }

        public invoiceDTO(
            string customerName,
            string customerAddress,
            string customerPhoneNo,
            string customerEmail,
            string customerRequestDetail,
            string staffUsername,
            string state,
            string date,
            List<invoiceDetailDTO> invoiceDetails
        )
        {
            this._customerName = customerName;
            this._customerAddress = customerAddress;
            this._customerPhoneNo = customerPhoneNo;
            this._customerRequestDetail = customerRequestDetail;
            this._state = state;
            this._staffUsername = staffUsername;
            this._invoiceDetails = invoiceDetails;
            this._customerEmail = customerEmail;
            this._date = date;
            this._objectId = null;
        }
        public invoiceDTO(
            string customerName,
            string customerAddress,
            string customerPhoneNo,
            string customerEmail,
            string customerRequestDetail,
            string staffUsername,
            string state,
            string date,
            List<invoiceDetailDTO> invoiceDetails,
            ObjectId? objectId
        )
        {
            this._customerName = customerName;
            this._customerAddress = customerAddress;
            this._customerPhoneNo = customerPhoneNo;
            this._customerRequestDetail = customerRequestDetail;
            this._state = state;
            this._staffUsername = staffUsername;
            this._invoiceDetails = invoiceDetails;
            this._customerEmail = customerEmail;
            this._date = date;
            this._objectId = objectId;
        }
        public ObjectId? objectId { get => _objectId; set => _objectId = value; }
        public string date { get => _date; set => _date = value; }
        public string customerAddress { get => _customerAddress; set => _customerAddress = value; }
        public string customerPhoneNo { get => _customerPhoneNo; set => _customerPhoneNo = value; }
        public string customerEmail { get => _customerEmail; set => _customerEmail = value; }
        public List<invoiceDetailDTO> invoiceDetails { get => _invoiceDetails; set => _invoiceDetails = value; }
        public string state { get => _state; set => _state = value; }
        public string customerName { get => _customerName; set => _customerName = value; }
        public string customerRequestDetail { get => _customerRequestDetail; set => _customerRequestDetail = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
    }
}
