using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashRegister.Managers
{
    public interface IFileManager<T>
    {
        void SaveToFile(string filePath, List<T> items);
        List<T> LoadFromFile(string filePath);
    }
}
