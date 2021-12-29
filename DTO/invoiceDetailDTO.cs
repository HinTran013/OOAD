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
        private double _unitPrice;

        public invoiceDetailDTO()
        {
            this._service = null;
            this._unitQuantity = -1;
            this._unitPrice = -1;
        }

        public invoiceDetailDTO(
            string service,
            int unitQuantity,
            double price = 0
        )
        {
            this._service = service;
            this._unitQuantity = unitQuantity;
            this._unitPrice = price;
        }

        public string service { get => _service; set => _service = value; }
        public int unitQuantity { get => _unitQuantity; set => _unitQuantity = value; }
        public double unitPrice { get => _unitPrice; set => _unitPrice = value; }
    }
}
