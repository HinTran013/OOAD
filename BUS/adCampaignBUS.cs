using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class adCampaignBUS
    {
        static adCampaignDAL adCampaign = new adCampaignDAL();

        public static bool AddNewAdCampaign(adCampaignDTO newAdCampaign)
        {
            return adCampaign.InsertNewAdCampaignRecord(newAdCampaign);
        }
        public static long CountAllCampaigns()
        {
            return adCampaign.CountAllCampaigns();
        }

    }
}
