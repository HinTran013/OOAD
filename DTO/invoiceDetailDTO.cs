using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class invoiceDetailDTO
    {
        private string _invoiceID;
        private string _service;
        private string _unitQuantity;
        private string _unitPrice;

        public invoiceDetailDTO()
        {
            this._invoiceID = null;
            this._service = null;
            this._unitQuantity = null;
            this._unitPrice = null;
        }

        public invoiceDetailDTO(
            string invoiceID,
            string service,
            string unitQuantity,
            string unitPrice
        )
        {
            this._invoiceID = invoiceID;
            this._service = service;
            this._unitQuantity = unitQuantity;
            this._unitPrice = unitPrice;
        }

        public string invoiceID { get => _invoiceID; set => _invoiceID = value; }
        public string service { get => _service; set => _service = value; }
        public string unitQuantity { get => _unitQuantity; set => _unitQuantity = value; }
        public string unitPrice { get => _unitPrice; set => unitPrice = value; }
    }
}
