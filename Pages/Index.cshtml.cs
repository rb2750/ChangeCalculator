﻿using System;
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

        public ActionResult OnPostCalculateChange()
        {
            return new JsonResult(new {result = "Test"});
        }
    }
}