using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class paymentBillDTO
    {
        private string _customerName;
        private string _customerAddress;
        private string _customerPhoneNo;
        private string _customerEmail;
        private string _customerRequestDetail;
        private string _state;
        private string _staffUsername;
        private string _date;
        private List<billDetailDTO> _billDetails;
        private int _totalMoney;
        private ObjectId? _objectId;

        public paymentBillDTO()
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
            this._totalMoney = -1;
            this._billDetails = null;
        }

        //constructor with params
        public paymentBillDTO
        (
            string customerName,
            string customerAddress,
            string customerPhoneNo,
            string customerEmail,
            string customerRequestDetail,
            string staffUsername,
            string state,
            string date,
            List<billDetailDTO> billDetails
        ){
            this._customerName = customerName;
            this._customerAddress = customerAddress;
            this._customerPhoneNo = customerPhoneNo;
            this._customerRequestDetail = customerRequestDetail;
            this._state = state;
            this._staffUsername = staffUsername;
            this._billDetails = billDetails;
            this._customerEmail = customerEmail;
            this._date = date;
            this._objectId = null;
            foreach(billDetailDTO billDetail in billDetails)
            {
                this._totalMoney += billDetail.unitPrice;
            }
        }
        public paymentBillDTO
        (
            string customerName,
            string customerAddress,
            string customerPhoneNo,
            string customerEmail,
            string customerRequestDetail,
            string staffUsername,
            string state,
            string date,
            List<billDetailDTO> billDetails,
            ObjectId? objectId
        )
        {
            this._customerName = customerName;
            this._customerAddress = customerAddress;
            this._customerPhoneNo = customerPhoneNo;
            this._customerRequestDetail = customerRequestDetail;
            this._state = state;
            this._staffUsername = staffUsername;
            this._billDetails = billDetails;
            this._customerEmail = customerEmail;
            this._date = date;
            this._objectId = objectId;
            foreach (billDetailDTO billDetail in billDetails)
            {
                this._totalMoney += billDetail.unitPrice;
            }
        }

        public string customerName { get => _customerName; set => _customerName = value; }
        public string customerAddress { get => _customerAddress; set => _customerAddress = value; }
        public string customerPhoneNo { get => _customerPhoneNo; set => _customerPhoneNo = value; }
        public string customerEmail { get => _customerEmail; set => _customerEmail = value; }
        public string customerRequestDetail { get => _customerRequestDetail; set => _customerRequestDetail = value; }
        public string state { get => _state; set => _state = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
        public string date { get => _date; set => _date = value; }
        public List<billDetailDTO> billDetails { get => _billDetails; set => _billDetails = value; }
        public int totalMoney { get => _totalMoney; set => _totalMoney = value; }
        public ObjectId? bbjectId { get => _objectId; set => _objectId = value; }
    }
}
