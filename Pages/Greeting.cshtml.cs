using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabetWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiabetWeb.Pages
{
    public class GreetingModel : PageModel
    {
        private IGreeter _greet;

        public string CurrentMessage { get; set; }

        public GreetingModel(IGreeter greet)
        {
            _greet = greet;
        }

        public void OnGet(string name)
        {
            CurrentMessage = $"{name}: {_greet.GetMessageOfTheDay()}";
        }
    }
}