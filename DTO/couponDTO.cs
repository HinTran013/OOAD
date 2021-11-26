using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class couponDTO
    {
        private string _couponCode;
        private string _endDate;
        private float _saleAmount;
        private string _invoiceID;

        public couponDTO()
        {
            this._couponCode = null;
            this._endDate = null;
            this._saleAmount = 0;
            this._invoiceID = null;
        }

        public couponDTO(
            string couponCode,
            string endDate,
            float saleAmount,
            string invoiceID
        )
        {
            this._couponCode = couponCode;
            this._endDate = endDate;
            this._saleAmount = saleAmount;
            this._invoiceID = invoiceID;
        }

        public string couponCode { get => _couponCode; set => _couponCode = value; }
        public string endDate { get => _endDate; set => _endDate = value; }
        public float saleAmount { get => _saleAmount; set => _saleAmount = value; }
        public string invoiceID { get => _invoiceID; set => invoiceID = value; }
    }
}
