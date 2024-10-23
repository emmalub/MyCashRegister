using MyCashRegister.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Transactions
{
    public class Calculate
    {
        public decimal CalculatePrice(decimal quantity)
        {
            Product product = new Product();
            if (product.PriceType == PriceType.PerKilo)
            {
                return product.Price * quantity;
            }
            else
            {
                return product.Price * (int)quantity;
            }
        }
    }
}
