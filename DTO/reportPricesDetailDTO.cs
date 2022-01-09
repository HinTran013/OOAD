using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class reportPricesDetailDTO
    {
        private string _serviceType;
        private int _oldPrice;
        private int _newPrice;

        public reportPricesDetailDTO(string serviceType, int oldPrice, int newPrice)
        {
            this._serviceType = serviceType;
            this._oldPrice = oldPrice;
            this._newPrice = newPrice;
        }

        public reportPricesDetailDTO()
        {
            _serviceType = null;
            _oldPrice = -1;
            _newPrice = -1;
        }

        public string serviceType { get => _serviceType; set => _serviceType = value; }
        public int oldPrice { get => _oldPrice; set => _oldPrice = value; }
        public int newPrice { get => _newPrice; set => _newPrice = value; }
    }
}
