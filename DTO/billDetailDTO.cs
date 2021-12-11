using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class billDetailDTO
    {
        private string _service;
        private int _unitPrice;
        private int _unitQuantity;

        public billDetailDTO()
        {
            this._service = null;
            this._unitPrice = -1;
            this._unitQuantity = -1;
        }

        public billDetailDTO(
            string service,
            int unitPrice,
            int unitQuantity
        )
        {
            this._service = service;
            this._unitPrice = unitPrice;
            this._unitQuantity = unitQuantity;
        }

        public string service { get => _service; set => _service = value; }
        public int unitPrice { get => _unitPrice; set => _unitPrice = value; }
        public int unitQuantity { get => _unitQuantity; set => _unitQuantity = value; }
    }
}
