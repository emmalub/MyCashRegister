using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Products
{
    public interface IProductRepository
    {
        List<Product> GetAll();
    }
}
