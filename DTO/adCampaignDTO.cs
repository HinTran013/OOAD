using MongoDB.Bson;
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
        private string _staffUsername;
        private string _description;
        private ObjectId? _objectId;

        public adCampaignDTO()
        {
            this._campaignName = null;
            this._dateStart = null;
            this._dateEnd = null;
            this._type = null;
            this._staffUsername = null;
            this._objectId = null;
            this._description = null;
        }

        public adCampaignDTO(
            string campaignName,
            string dateStart,
            string dateEnd,
            string type,
            string staffUsername,
            string description = null,
            ObjectId? objectId = null
        )
        {
            this._campaignName = campaignName;
            this._dateStart = dateStart;
            this._dateEnd = dateEnd;
            this._type = type;
            this._staffUsername = staffUsername;
            this._description = description;
            this._objectId = objectId;
        }

        public string description { get => _description; set => _description = value; }
        public ObjectId? objectId { get => _objectId; set => _objectId = value; }
        public string campaignName { get => _campaignName; set => _campaignName = value; }
        public string dateStart { get => _dateStart; set => _dateStart = value; }
        public string dateEnd { get => _dateEnd; set => _dateEnd = value; }
        public string type { get => _type; set => _type = value; }
        public string staffUsername { get => _staffUsername; set => _staffUsername = value; }
    }
}
