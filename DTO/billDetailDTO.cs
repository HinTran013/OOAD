using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class billDetailDTO
    {
        private string _billID;
        private string _photoType;
        private long _unitPrice;
        private long _unitQuantity;

        public billDetailDTO()
        {
            this._billID = null;
            this._photoType = null;
            this._unitPrice = -1;
            this._unitQuantity = -1;
        }

        public billDetailDTO(
            string billID,
            string photoType,
            long unitPrice,
            long unitQuantity
        )
        {
            this._billID = billID;
            this._photoType = photoType;
            this._unitPrice = unitPrice;
            this._unitQuantity = unitQuantity;
        }

        public string billID { get => _billID; set => _billID = value; }
        public string photoType { get => _photoType; set => _photoType = value; }
        public long unitPrice { get => _unitPrice; set => _unitPrice = value; }
        public long unitQuantity { get => _unitQuantity; set => _unitQuantity = value; }
    }
}
