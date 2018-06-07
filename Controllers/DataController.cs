using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabetWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabetWeb.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class DataController : Controller
    {
		private IDiabetWebData _diabetWebData;

		public DataController(IDiabetWebData data)
		{
			_diabetWebData = data;
		}

		[Route("Foods")]
		public JsonResult Foods()
        {
			return new JsonResult(_diabetWebData.GetAllFoodItems().ToList());
        }

		[Route("Members")]
		public JsonResult Members()
		{
			return new JsonResult(_diabetWebData.GetAllMemberItems().ToList());
		}


		[Route("")]
		[Route("Meals")]
		public JsonResult Meals()
		{
			return new JsonResult(_diabetWebData.GetAllMealItems().ToList());
		}
	}
}
