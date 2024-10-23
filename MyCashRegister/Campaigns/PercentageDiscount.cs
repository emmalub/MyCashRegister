using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Campaigns
{
    public class PercentageDiscount : IDiscountType
    {
        private decimal _percentage;

        public PercentageDiscount(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return originalPrice - originalPrice * _percentage / 100;
        }
    }

}