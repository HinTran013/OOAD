using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class fundBillDTO
    {
        private string _ID;
        private string _date;
        private double _totalMoney;
        private string _accountantID;
        private string _description;

        public fundBillDTO()
        {
            this._ID = null;
            this._date = null;
            this._totalMoney = -1;
            this._accountantID = null;
            this._description = null;
        }

        //constructor with params
        public fundBillDTO(string ID,
        string date,
        double totalMoney,
        string description,
        string accountantID
        )
        {
            this._ID = ID;
            this._date = date;
            this._totalMoney = totalMoney;
            this._accountantID = accountantID;
            this._description = description;
        }

        public string ID { get => _ID; set => _ID = value; }
        public string date { get => _date; set => _date = value; }
        public double totalMoney { get => _totalMoney; set => _totalMoney = value; }
        public string description { get => _description; set => _description = value; }
        public string accountantID { get => _accountantID; set => _accountantID = value; }
    }
}
