using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class couponBUS
    {
        static couponDAL coupon = new couponDAL();

        public static bool AddNewCoupon(couponDTO newCoupon)
        {
            return coupon.InsertNewCouponRecord(newCoupon);
        }
        public static double CheckCouponWithCode(string str)
        {
            return coupon.CheckCouponWithCode(str);
        }
    }
}
