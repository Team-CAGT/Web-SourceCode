using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Domain.Interfaces.Services
{
    public partial interface IHospitalFeeService
    {
        BangiaDichvu GetBangiaDichvubyType(string type);
        List<BangiaDichvu> GetBangiaDichvu();
        void UpdateBangiaDichvu(BangiaDichvu entity);
        BangiaDichvu GetBangiaDichvubyId(Guid id);
    }
 
}
