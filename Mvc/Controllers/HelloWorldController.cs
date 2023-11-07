/* ------------------------------------------------------------------------------
<auto-generated>
    This file was generated by Sitefinity CLI v1.1.0.31
</auto-generated>
------------------------------------------------------------------------------ */

using ADA.Mvc.Models;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.RelatedData;

namespace ADA.Mvc.Controllers
{
	[ControllerToolboxItem(Name = "HelloWorld", Title = "Hello World Lino", SectionName = "ADA")]
	public class HelloWorldController : Controller, IPersonalizable
	{
		// GET: HelloWorld
		public ActionResult Index()
		{
			var model = new HelloWorldModel();
			model.Message = this.Message;
			model.MyDate = this.MyDate;
			model.Number = this.Number;	
			model.Flag = this.Flag;
			model.Enum = this.Enum;		
			return View(model);
		}
		public ActionResult Joe()
		{

			return View();
		}


        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

		public string Message { get; set; }
        public bool Flag { get; set; }
        public Enumeration Enum { get; set; }
		[Browsable(false)]
		public int Number { get; set; }
        public DateTime MyDate { get; set; }

		[Content(Type=KnownContentTypes.Pages)]
		public MixedContentContext Pages { get; set; }

        [Content(Type = KnownContentTypes.Images)]
        public MixedContentContext Images { get; set; }

        [Content(Type = KnownContentTypes.News)]
        public MixedContentContext MyNews { get; set; }
    }
}