using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class invoiceDetailDTO
    {
        private string _service;
        private int _unitQuantity;
        private int _unitPrice;

        public invoiceDetailDTO()
        {
            this._service = null;
            this._unitQuantity = -1;
            this._unitPrice = -1;
        }

        public invoiceDetailDTO(
            string service,
            int unitQuantity,
            int unitPrice
        )
        {
            this._service = service;
            this._unitQuantity = unitQuantity;
            this._unitPrice = unitPrice;
        }

        public string service { get => _service; set => _service = value; }
        public int unitQuantity { get => _unitQuantity; set => _unitQuantity = value; }
        public int unitPrice { get => _unitPrice; set => unitPrice = value; }
    }
}
