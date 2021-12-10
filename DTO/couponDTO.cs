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
        private string _couponTitle;
        private string _startDate;
        private string _endDate;
        private float _couponPercent;
        private string _couponDesc;

        public couponDTO()
        {
            this._couponCode = null;
            this._endDate = null;
            this._couponPercent = 0;
            this._couponDesc = null;
            this._startDate = null;
            this._couponTitle = null;
        }

        public couponDTO(
            string couponCode,
            string couponTitle,
            string startDate,
            string endDate,
            float couponPercent,
            string couponDesc
        )
        {
            this._couponTitle = couponTitle;
            this._couponCode = couponCode;
            this._startDate = startDate;
            this._endDate = endDate;
            this._couponPercent = couponPercent;
            this._couponDesc = couponDesc;
        }
        public string couponTitle { get => _couponTitle; set => _couponTitle = value; }
        public string couponCode { get => _couponCode; set => _couponCode = value; }
        public string startDate { get => _startDate; set => _startDate = value; }
        public string endDate { get => _endDate; set => _endDate = value; }
        public float couponPercent { get => _couponPercent; set => _couponPercent = value; }
        public string couponDesc { get => _couponDesc; set => couponDesc = value; }
    }
}
