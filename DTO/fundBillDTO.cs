using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class fundBillDTO
    {
        private string _date;
        private double _totalMoney;
        private string _staffUsername;
        private ObjectId? _objectId;
        private ObjectId? _objectIdFromInvoice;
        //

        public fundBillDTO()
        {
            this._objectId = null;
            this._date = null;
            this._totalMoney = -1;
            this._staffUsername = null;
            this._objectIdFromInvoice = null;
        }

        //constructor with params
        public fundBillDTO(
        string date,
        double totalMoney,
        string staffUsername,
        ObjectId? objectIdFromInvoice = null,
        ObjectId? objectId = null
        )
        {
            this._objectId = objectId;
            this._date = date;
            this._objectIdFromInvoice = objectIdFromInvoice;
            this._totalMoney = totalMoney;
            this._staffUsername = staffUsername;
        }

        public ObjectId? objectId { get => _objectId; set => _objectId = value; }
        public string date { get => _date; set => _date = value; }
        public ObjectId? objectIdFromInvoice { get => _objectIdFromInvoice; set => _objectIdFromInvoice = value; }
        public double totalMoney { get => _totalMoney; set => _totalMoney = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
    }
}
