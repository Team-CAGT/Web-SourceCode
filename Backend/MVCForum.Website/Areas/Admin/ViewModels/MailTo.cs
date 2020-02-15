using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class MailTo
    {
        public Guid Id { get; set; }
        public  string Email { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public  string Content { get; set; }
    }
}