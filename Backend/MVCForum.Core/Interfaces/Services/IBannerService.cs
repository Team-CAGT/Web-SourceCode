using MVCForum.Domain.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCForum.Domain.Interfaces.Services
{
    public interface IBannerService
    {
        /// <summary>
        /// get all banner enabled
        /// </summary>
        /// <returns></returns>
        IList<Banner> GetallBanners();

        /// <summary>
        /// Delete a banner
        /// </summary>
        /// <param name="banner"></param>
        void Delete(Banner banner);
        /// <summary>
        /// Get a banner by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Banner GetBanner(string name);

      Banner Get(Guid id);

        /// <summary>
        /// All banner
        /// </summary>
        /// <returns></returns>
        IEnumerable<Banner> GetAll();

        Banner Add(Banner newBanner);
        void Update(Banner updateBanner);
        List<Banner> GetActiveBanners();
    }
}
