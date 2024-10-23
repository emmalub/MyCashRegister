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
        private InputValidator()
        { }

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
        public static string NonEmptyString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Inmatningen kan inte vara tom.");
            }
            return input;
        }


        public bool ValidateDecimal(string input, out decimal result)
        {
            return decimal.TryParse(input, out result);
        }
        public bool ValidateInt(string input, out int result)
        {
            return int.TryParse(input, out result);
        }
        public bool ValidateDate(string input, out DateOnly result)
        {
            return DateOnly.TryParse(input, out result);
        }
        public string InvalidInputMessage()
        {
            return "Ogiltig inmatning, försök igen.";
        }
    }
}

