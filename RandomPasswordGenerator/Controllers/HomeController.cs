using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomPasswordGenerator.ViewModels;

namespace RandomPasswordGenerator.Controllers
{
    public class HomeController : Controller
    {
        // lowercase characters
        internal static readonly char[] lower =
           "qwertyuiopasdfghjklzxcvbnm".ToCharArray();

        // uppercase characters
        internal static readonly char[] upper =
            "QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();

        // number characters
        internal static readonly char[] numbers =
            "1234567890".ToCharArray();

        // special characters
        internal static readonly char[] special =
            "@%+\\/'!#$^?:,(){}[]~-_.".ToCharArray();

        [Route("Home")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Password();

            return View(model);
        }

        [Route("Home")]
        [HttpPost]
        public IActionResult Index(Password model) 
        {
            // list to store which options (character sets) was chosen by the user 
            List<int> options = new List<int>();

            // character array to be used for creating the string
            StringBuilder sb = new StringBuilder();

            // adds lowercase characters by default
            options.Add(0);
            sb.Append(lower);

            // add uppercase characters
            if (model.UpperCase)
                sb.Append(upper);

            // add uppercase characters
            if (model.Numbers)
                sb.Append(numbers);

            // add uppercase characters
            if (model.SpecialChar)
                sb.Append(special);

            model.PasswordGenerated = GetUniqueKey(model.PasswordLength, sb.ToString().ToCharArray());
            return View(model);
        }

        /// <summary>
        /// Generates the unique key using RNGCryptoServiceProvider
        /// </summary>
        /// <param name="size">The size of the characters included in the string</param>
        /// <param name="chars">The characters used to generate the string</param>
        /// <returns></returns>
        public static string GetUniqueKey(int size, char[] chars)
        {
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(size);

            // generates unique string
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                long idx = rnd % chars.Length;
                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }   
}
