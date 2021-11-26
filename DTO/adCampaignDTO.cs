using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class adCampaignDTO
    {
        private string _campaignName;
        private string _dateStart;
        private string _dateEnd;
        private string _type;
        private string _staffID;

        public adCampaignDTO()
        {
            this._campaignName = null;
            this._dateStart = null;
            this._dateEnd = null;
            this._type = null;
            this._staffID = null;
        }

        public adCampaignDTO(
            string campaignName,
            string dateStart,
            string dateEnd,
            string type,
            string staffID
        )
        {
            this._campaignName = campaignName;
            this._dateStart = dateStart;
            this._dateEnd = dateEnd;
            this._type = type;
            this._staffID = staffID;
        }

        public string campaignName { get => _campaignName; set => _campaignName = value; }
        public string dateStart { get => _dateStart; set => _dateStart = value; }
        public string dateEnd { get => _dateEnd; set => _dateEnd = value; }
        public string type { get => _type; set => _type = value; }
        public string staffID { get => _staffID; set => _staffID = value; }
    }
}
