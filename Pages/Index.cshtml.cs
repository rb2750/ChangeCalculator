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
        //A dictionary between a coin/note type and its value in pounds.
        private readonly Dictionary<string, decimal> coinTypes = new()
        {
            {"£50", 50},
            {"£20", 20},
            {"£10", 10},
            {"£5", 5},
            {"£2", 2},
            {"£1", 1},
            {"50p", 0.5m},
            {"20p", 0.2m},
            {"10p", 0.1m},
            {"5p", 0.05m},
            {"2p", 0.02m},
            {"1p", 0.01m}
        };

        /**
         * Calculate the change given the balance and price.
         * Returns JSON in the form:
         * {"success":true,"result":{"coin type":quantity}}
         */
        public ActionResult OnPostCalculateChange(decimal yourBalance, decimal productPrice)
        {
            if (yourBalance < 0)
            {
                return new JsonResult(new {success = false, message = "The entered balance must be not be below 0."});
            }

            if (productPrice < 0)
            {
                return new JsonResult(new {success = false, message = "The entered product price must be not be below 0."});
            }

            decimal change = yourBalance - productPrice;

            if (change < 0)
            {
                return new JsonResult(new {success = false, message = "You can't afford this!"});
            }

            var response = CalculateChange(change);

            return new JsonResult(new {success = true, result = response});
        }

        /**
         * This function calculates the change that should be given.
         */
        private Dictionary<string, int> CalculateChange(decimal change)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            var coins = coinTypes.OrderByDescending(pair => pair.Value); //Confirm that the dictionary is definitely in order, largest to smallest.

            //While there is still change to be given.
            while (change > 0)
            {
                //Find the largest coin type that can be given at this point
                KeyValuePair<string, decimal> coinPair = coins.First(pair => pair.Value <= change);

                //If it's already been added, increment it. Otherwise, add it.
                if (result.ContainsKey(coinPair.Key))
                {
                    result[coinPair.Key]++;
                }
                else
                {
                    result.Add(coinPair.Key, 1);
                }

                //Deduct the now given change.
                change -= coinPair.Value;
            }

            return result;
        }
    }
}