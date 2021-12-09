using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class reportPricesDTO
    {
        private string _reportPriceID;
        private string _date;
        private string _subject;
        private string _accountantID;

        public reportPricesDTO()
        {
            _reportPriceID = null;
            _date = null;
            _subject = null;
            _accountantID = null;
        }

        public reportPricesDTO(string reportPriceID, string date, string subject, string accountantID = null)
        {
            _reportPriceID = reportPriceID;
            _date = date;
            _subject = subject;
            _accountantID = accountantID;
        }

        public string reportPriceID { get => _reportPriceID; set => _reportPriceID = value; }
        public string date { get => _date; set => _date = value; }
        public string subject { get => _subject; set => _subject = value; }
        public string accountantID { get => _accountantID; set => _accountantID = value; }
    }
}
