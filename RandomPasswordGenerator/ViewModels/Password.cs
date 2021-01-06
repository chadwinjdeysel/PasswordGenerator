using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPasswordGenerator.ViewModels
{
    public class Password
    {
        [Display(Name = "Length of your Password")]
        [Range(8,16)]
        public int PasswordLength { get; set; }
        public string PasswordGenerated { get; set; } = "";

        // toggle options for password
        public bool UpperCase { get; set; }
        public bool Numbers { get; set; }
        public bool SpecialChar { get; set; }

    }
}
