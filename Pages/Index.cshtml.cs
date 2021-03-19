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
        public IndexModel()
        {
        }

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

            var response = new Dictionary<string, int>();


            return new JsonResult(new {success = true, result = response});
        }
    }
}