using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class reportPricesDetailDTO
    {
        private string _reportPricesID;
        private string _photoType;
        private Nullable<double> _oldPrice;
        private Nullable<double> _newPrice;

        public reportPricesDetailDTO(string reportPricesID, string photoType, double oldPrices, double newPrices)
        {
            _reportPricesID = reportPricesID;
            _photoType = photoType;
            _oldPrice = oldPrices;
            _newPrice = newPrices;
        }

        public reportPricesDetailDTO()
        {
            _reportPricesID = null;
            _photoType = null;
            _oldPrice = null;
            _newPrice = null;
        }

        public string reportPricesID { get => _reportPricesID; set => _reportPricesID = value; }
        public string photoType { get => _photoType; set => _photoType = value; }
        public double? oldPrice { get => _oldPrice; set => _oldPrice = value; }
        public double? newPrice { get => _newPrice; set => _newPrice = value; }
    }
}
