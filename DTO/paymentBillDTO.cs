using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class paymentBillDTO
    {
        private string _date;
        private double _totalMoney;
        private string _accountantID;

        public paymentBillDTO()
        {
            this._date = null;
            this._totalMoney = -1;
            this._accountantID = null;
        }

        //constructor with params
        public paymentBillDTO(
        string date,
        double totalMoney,
        string accountantID)
        {
            this._date = date;
            this._totalMoney = totalMoney;
            this._accountantID = accountantID;
        }

        public string date { get => _date; set => _date = value; }
        public double totalMoney { get => _totalMoney; set => _totalMoney = value; }
        public string accountantID { get => _accountantID; set => _accountantID = value; }
    }
}
