using Microsoft.AspNetCore.Mvc;

namespace DiabetWeb.Controllers
{
    //about
    [Route("[controller]")]
	public class AboutController
    {
		[Route("")]
		[Route("Phone")]
		public string Phone()
        {
            return "917-968-XXXX";
        }

		[Route("Address")]
		public string Address()
        {
            return "Brooklyn, NY 11215";
        }
    }
}
