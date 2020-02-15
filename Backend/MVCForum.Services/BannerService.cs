using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.Interfaces;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Services.Data.Context;
using MVCForum.Domain.DomainModel.Enums;

namespace MVCForum.Services
{
    public class BannerService : IBannerService
    {
        private readonly MVCForumContext _context;
        public BannerService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }

        public Banner Add(Banner newBanner)
        {
            return _context.Banner.Add(newBanner);
        }

        public void Delete(Banner banner)
        {
            banner.Status = "D";
            _context.Entry(banner).State = System.Data.Entity.EntityState.Modified;
        }

        public Banner Get(Guid id)
        {
            return _context.Banner.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Banner> GetAll()
        {
            return _context.Banner.OrderByDescending(x => x.Name).ToList();
        }

        public IList<Banner> GetallBanners()
        {
            return _context.Banner.OrderByDescending(x => x.Name).ToList();
        }

        public Banner GetBanner(string name)
        {
            return _context.Banner.FirstOrDefault(x => x.Name.Contains(name));
        }

        public void Update(Banner updateBanner)
        {
            _context.Entry(updateBanner).State = System.Data.Entity.EntityState.Modified;
        }

        public List<Banner> GetActiveBanners()
        {
            var now = DateTime.Now;
            return _context.Banner.Where(x => x.StartDate <= now && x.EndDate >= now && x.Status == "A").OrderBy(x=>x.Position).ToList();
        }
    }
}
