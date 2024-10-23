using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Managers
{
    public interface IManageable
    {
        void Add();
        void Remove();
        void Edit();
    }
}
