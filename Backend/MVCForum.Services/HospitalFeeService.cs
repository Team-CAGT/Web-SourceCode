using System;
using System.Collections.Generic;
using System.Linq;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.Interfaces;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Services.Data.Context;

namespace MVCForum.Services
{
    public partial class HospitalFeeService : IHospitalFeeService
    {
        private readonly MVCForumContext _context;
        public HospitalFeeService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }

        #region GiaVienPhi
        public BangiaDichvu GetBangiaDichvubyType(string type)
        {
            return _context.BangiaDichvu.FirstOrDefault(x=>x.Type.Equals(type));
        }
        public BangiaDichvu GetBangiaDichvubyId(Guid id)
        {
            return _context.BangiaDichvu.FirstOrDefault(x => x.Id == id);
        }

        #endregion



        #region GiaDichVu
        public List<BangiaDichvu> GetBangiaDichvu()
        {
            return _context.BangiaDichvu.ToList();
        }

        public void UpdateBangiaDichvu(BangiaDichvu entity)
        {
            entity.DateCreated = DateTime.Now;
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
        #endregion



       
    }
}
