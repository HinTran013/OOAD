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
        private string _createDate;
        private string _dueDate;
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
            this._createDate = null;
            this._objectId = null;
            this._totalMoney = -1;
            this._billDetails = null;
            this._dueDate = null;
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
            string createDate,
            string dueDate,
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
            this._createDate = createDate;
            this._dueDate = dueDate;
            this._objectId = null;
            if (billDetails != null)
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
            string createDate,
            string dueDate,
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
            this._createDate = createDate;
            this._dueDate = dueDate;
            this._objectId = objectId;
            if (billDetails != null)
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
        public string dueDate { get => _dueDate; set => _dueDate = value; }
        public string createDate { get => _createDate; set => _createDate = value; }
        public List<billDetailDTO> billDetails { get => _billDetails; set => _billDetails = value; }
        public int totalMoney { get => _totalMoney; set => _totalMoney = value; }
        public ObjectId? objectId { get => _objectId; set => _objectId = value; }
    }
}
