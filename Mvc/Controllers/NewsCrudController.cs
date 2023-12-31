/* ------------------------------------------------------------------------------
<auto-generated>
    This file was generated by Sitefinity CLI v1.1.0.31
</auto-generated>
------------------------------------------------------------------------------ */

using ADA.Mvc.Models;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.GenericContent.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Workflow;
using System;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;

namespace ADA.Mvc.Controllers
{
	[ControllerToolboxItem(Name = "NewsCrud", Title = "News Crud", SectionName = "ADA")]
	public class NewsCrudController : Controller, IPersonalizable
	{
		NewsManager newsManager = NewsManager.GetManager();
		// GET: NewsCrud
		public ActionResult Index()
		{
			var news = newsManager.GetNewsItems().Where(n => n.Status == ContentLifecycleStatus.Live);
			var model = new NewsCrudModel(news);
			return View("Index", model);
		}

        public ActionResult CreatePressRelease()
        {
            NewsItem item = newsManager.CreateNewsItem();
            item.Title = "News Item 1";
            item.Content = "<h2>Test content here</h2>";
            item.DateCreated = DateTime.UtcNow;
            item.PublicationDate = DateTime.UtcNow;
            item.LastModified = DateTime.UtcNow;
            item.UrlName = Regex.Replace(item.Title, @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            newsManager.SaveChanges();

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(NewsItem).FullName);
            WorkflowManager.MessageWorkflow(item.Id, typeof(NewsItem), null, "Publish", false, bag);

            return View("CreatePressRelease");
        }

        public ActionResult Update()
        {
            var master = newsManager.GetNewsItems().FirstOrDefault(n=>n.Title.Equals("News Item 1") && n.Status == ContentLifecycleStatus.Master);
            var temp = newsManager.Lifecycle.CheckOut(master) as NewsItem;
            temp.Title = "News Item 1 Updated";
            temp.Summary = "Lino was here";

            master = newsManager.Lifecycle.CheckIn(temp) as NewsItem;
            newsManager.SaveChanges();

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(NewsItem).FullName);
            WorkflowManager.MessageWorkflow(master.Id, typeof(NewsItem), null, "Publish", false, bag);


            return View("Update");
        }

        public ActionResult CoolStuff()
        {
            string templateTitle = "Mike";
            string placeholderCaption = "grid-6+6";
            int placeholderIndex = 0;
            PageTemplate template;

            PageManager pm = PageManager.GetManager();
            var node = pm.GetPageNodes().Where(n => n.UrlName == "mike-page").FirstOrDefault();
            var pagedata = node.GetPageData();

            var page = pm.EditPage(pagedata.Id);

            if (!string.IsNullOrEmpty(placeholderCaption))
            {
                MvcControllerProxy widget = new MvcControllerProxy();
                widget.ControllerName = typeof(NewsCrudController).FullName;


                //Dictionary<string, object> propsValues = new Dictionary<string, object>();
                //propsValues.Add("Message", "My Message!");
                //// Settings values is a Dictionary<string, object>
                //foreach (var key in propsValues.Keys)
                //{
                //    widget.Settings.Values[key] = propsValues[key];
                //}

                template = pm.GetTemplates().Where(t => t.Title == templateTitle).FirstOrDefault();
                string placeholderId = template.Controls.Where(c => c.Caption.Contains(placeholderCaption)).FirstOrDefault().PlaceHolders[placeholderIndex];
                var widgetControl = pm.CreateControl<PageDraftControl>(widget, placeholderId);
                widgetControl.Caption = "Hello World";
                pm.SetControlDefaultPermissions(widgetControl);
                page.Controls.Add(widgetControl);
                pm.PublishPageDraft(page);
                pm.SaveChanges();
            }
                return View("CoolStuff");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

	}
}