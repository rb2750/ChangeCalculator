using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ChangeCalculator.Pages
{
    public class IndexModel : PageModel
    {
        //A dictionary between a coin/note type and its value in pence.
        Dictionary<string, int> coinTypes = new()
        {
            {"£50", 5000},
            {"£20", 5000},
            {"£10", 5000},
            {"£5", 5000},
            {"£2", 5000},
            {"£1", 5000},
            {"50p", 5000},
            {"10p", 5000},
            {"5p", 5000},
            {"2p", 5000},
            {"1p", 5000}
        };

        /**
         * Calculate the change given the balance and price.
         * Returns JSON in the form:
         * {"success":true,"result":{"coin type":quantity}}
         */
        public ActionResult OnPostCalculateChange(float yourBalance, float productPrice)
        {
            if (yourBalance < 0)
            {
                return new JsonResult(new {success = false, message = "The entered balance must be not be below 0."});
            }

            if (productPrice < 0)
            {
                return new JsonResult(new {success = false, message = "The entered product price must be not be below 0."});
            }

            var response = CalculateChange(yourBalance,productPrice);
            
            return new JsonResult(new {success = true, result = response});
        }

        /**
         * This function calculates the change that should be given.
         */
        private Dictionary<string, int> CalculateChange(float balance, float productPrice)
        {
            
        }
    }
}