using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MyCashRegister.Managers
{
    public class InputValidator
    {
        private static InputValidator _instance;
        public InputValidator()
        {
        }

        public static InputValidator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputValidator();
                }
                return _instance;
            }
        }
        public bool NonEmptyString(string input, out string validName)
        {
            validName = input;
            return !string.IsNullOrWhiteSpace(input);
        }
        public bool ValidateDecimal(string input, out decimal result)
        {
            input = input.Replace('.', ',');

            return decimal.TryParse(input, out result);
        }
        public bool ValidateInt(string input, out int result)
        {
            return int.TryParse(input, out result);
        }
        public static void InvalidInputMessage()
        {
            Console.WriteLine("Ogiltig inmatning. Tryck på en valfri tangent för att fortsätta.");
            Console.ReadLine();
        }
    }
}

