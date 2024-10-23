using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Campaigns
{
    public class FixedAmountDiscount : IDiscountType
    {
        private decimal _amount;

        public FixedAmountDiscount(decimal amount)
        {
            _amount = amount;
        }
        public decimal ApplyDiscount(decimal originalPrice)
        {
            return originalPrice - _amount;
        }
    }
}
