using MVCForum.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class TopicPendingViewModel
    {
        public PagedList<Topic> Topics { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
    }
}