using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Domain.DomainModel;

namespace MVCForum.Domain.Model
{
    public partial class DataDoctorModel
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Avatar { get; set; }

        public DataDoctorModel()
        {

        }

        public DataDoctorModel(Doctor doctor)
        {
            id = doctor.Id.ToString();
            Name = doctor.Name;
            Position = doctor.PositionName;
            if (!string.IsNullOrEmpty(doctor.Avatar))
            {
                Avatar = doctor.Avatar;
            }
            else
            {
                Avatar = "/Content/images/default-bs.svg";
            }
        }
    }
}
