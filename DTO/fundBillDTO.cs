using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class fundBillDTO
    {
        private string _fundID;
        private string _date;
        private string _fundType;
        private double _totalMoney;
        private string _description;
        //
        private string _accountantID;

        public fundBillDTO()
        {
            this._fundID = null;
            this._date = null;
            this._fundType = null;
            this._totalMoney = -1;
            this._description = null;
            this._accountantID = null;
        }

        //constructor with params
        public fundBillDTO(
        string id,
        string date,
        string type,
        double totalMoney,
        string description,
        string accountantid = null
        )
        {
            this._fundID = id;
            this._date = date;
            this._fundType = type;
            this._totalMoney = totalMoney;
            this._description = description;
            this._accountantID = accountantid;
        }

        public string fundID { get => _fundID; set => _fundID = value; }
        public string date { get => _date; set => _date = value; }
        public string fundType { get => _fundType; set => _fundType = value; }
        public double totalMoney { get => _totalMoney; set => _totalMoney = value; }
        public string description { get => _description; set => _description = value; }
        public string accountantID { get => _accountantID; set => _accountantID = value; }
    }
}
