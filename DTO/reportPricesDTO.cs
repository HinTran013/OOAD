using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class reportPricesDTO
    {
        
        private string _date;
        private string _subject;
        private string _accountantID;
        private List<servicesDTO> _newServices;
        private List<double> _newPrices;
        private bool _state;

        public reportPricesDTO()
        {
           
            _date = null;
            _subject = null;
            _newServices = null;
            _newPrices = null;
            _accountantID = null;
            _state = false;
        }

        public reportPricesDTO(string date, string subject, List<servicesDTO>list, List<double> prices , string accountantID = null, bool state = false)
        {
            _date = date;
            _subject = subject;
            _newServices = list;
            _newPrices = prices;
            _accountantID = accountantID;
            _state = state;
        }

       
        public string date { get => _date; set => _date = value; }
        public string subject { get => _subject; set => _subject = value; }
        public string accountantID { get => _accountantID; set => _accountantID = value; }
        public List<servicesDTO> newServices { get => _newServices; set => _newServices = value; }
        public List<double> newPrices { get => _newPrices; set => _newPrices = value; }
        public bool state { get => _state; set => _state = value; }
    }
}
