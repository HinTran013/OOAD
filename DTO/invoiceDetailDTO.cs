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

        public invoiceDetailDTO()
        {
            this._service = null;
            this._unitQuantity = -1;
        }

        public invoiceDetailDTO(
            string service,
            int unitQuantity
        )
        {
            this._service = service;
            this._unitQuantity = unitQuantity;
        }

        public string service { get => _service; set => _service = value; }
        public int unitQuantity { get => _unitQuantity; set => _unitQuantity = value; }
    }
}
