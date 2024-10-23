using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Campaigns
{
    public interface IDiscountType
    {
        decimal ApplyDiscount(decimal originalPrice);
    }
}
