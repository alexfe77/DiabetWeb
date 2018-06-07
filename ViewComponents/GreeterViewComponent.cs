using DiabetWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabetWeb.ViewComponents
{
    public class GreeterViewComponent : ViewComponent
    {
        private IGreeter _greet;

        public GreeterViewComponent(IGreeter greet)
        {
            _greet = greet;
        }

        public IViewComponentResult Invoke()
        {
            var model = _greet.GetMessageOfTheDay();
            return View("Default", model);
        }
    }
}
