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
using MVCForum.Domain.DomainModel;
using MVCForum.Utilities;

namespace MVCForum.Services
{
    public class ContactService : IContactService
    {
        private readonly MVCForumContext _context;
        public ContactService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }

        public ContactForm Add(ContactForm newContact)
        {
            return _context.ContactForm.Add(newContact);
        }

        public List<Question> GellAllNewQuestions()
        {
            return _context.Question.Where(x => string.IsNullOrEmpty(x.ReplyContent)).ToList();
        }

        public Appointment AddAppointment(Appointment appointment)
        {
            return _context.Appointment.Add(appointment);
        }

        public Question AddQuestion(Question newQuestion)
        {
           return  _context.Question.Add(newQuestion);
        }

        public void Delete(ContactForm contact)
        {
            _context.ContactForm.Remove(contact);
        }

        public void DeleteQuestion(Question newQuestion)
        {
            _context.Question.Remove(newQuestion);
        }

        public ContactForm Get(Guid id)
        {
           return _context.ContactForm.FirstOrDefault(x=>x.Id == id);
        }

        public IEnumerable<ContactForm> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<ContactForm> GetAllContacts()
        {
            return _context.ContactForm.OrderByDescending(x=>x.CreateDate).ToList();
        }
        public PagedList<Appointment> GetAllAppointmentPost(int pageIndex, int pageSize)
        {
            var query = _context.Appointment;

            var results = query
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Appointment>(results, pageIndex, pageSize, query.Count());
        }

        public IList<ContactForm> GetallContact(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public PagedList<ContactForm> GetAllContactPost(int pageIndex, int pageSize)
        {
            var query = _context.ContactForm;

            var results = query
                .OrderByDescending(x => x.CreateDate).ThenBy(x=>x.Status)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<ContactForm>(results, pageIndex, pageSize, query.Count());
        }

        public PagedList<Question> GetAllQuestionPost(int pageIndex, int pageSize)
        {
            var query = _context.Question.Where(x => x.Status.Equals("UPDATE"));

            var results = query
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Question>(results, pageIndex, pageSize, query.Count());
        }
        public PagedList<Question> GetAllQuestionAdminPost(int pageIndex, int pageSize)
        {
            var query = _context.Question;

            var results = query
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Question>(results, pageIndex, pageSize, query.Count());
        }

        public List<Question> GetAllQuestionEditToday()
        {
            var now = DateUtils.ParseDate(DateTime.Now.ToString("MM/dd/yyyy"));
            var query = _context.Question;

            var results = query.Where(x=>x.LastUpdate >= now && x.Status== "UPDATE").ToList();
            return results;
        }

        public Question GetQuestion(Guid id)
        {
           return _context.Question.FirstOrDefault(x => x.Id == id);
        }
        public PagedList<Question> GetQuestions(Guid id)
        {
            var results =  _context.Question.Where(x => x.Id == id).ToList();
            return new PagedList<Question>(results, 1,1, 1);
        }
        public PagedList<Appointment> SearchAppointment(string search, int pageIndex, int pageSize)
        {

            search = StringUtils.SafePlainText(search);
            var query = _context.Appointment


                   .Where(x => x.Phone.ToUpper().Contains(search.ToUpper()) 
                   || x.Email.ToUpper().Contains(search.ToUpper())
                   || x.Name.ToUpper().Contains(search.ToUpper()));

            var results = query
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Appointment>(results, pageIndex, pageSize, query.Count());
        }

        public PagedList<ContactForm> SearchContact(string search, int pageIndex, int pageSize)
        {

            search = StringUtils.SafePlainText(search);
            var query = _context.ContactForm


                   .Where(x => x.Phone.ToUpper().Contains(search.ToUpper()) || x.Email.ToUpper().Contains(search.ToUpper()));

            var results = query
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<ContactForm>(results, pageIndex, pageSize, query.Count());
        }

        public PagedList<Question> SearchQuestions(string search, int pageIndex, int pageSize)
        {
            search = StringUtils.SafePlainText(search);
            var query = _context.Question


                   .Where(x => x.Phone.ToUpper().Contains(search.ToUpper()) || x.Email.ToUpper().Contains(search.ToUpper()));

            var results = query
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Question>(results, pageIndex, pageSize, query.Count());
        }

        public void Update(ContactForm updateContact)
        {
            _context.Entry(updateContact).State = System.Data.Entity.EntityState.Modified;
        }

        public void UpdateQuestion(Question updateQuestion)
        {
            _context.Entry(updateQuestion).State = System.Data.Entity.EntityState.Modified;
        }
        public PagedList<SoyteInfo> GetAllSoytePost(int pageIndex, int pageSize)
        {
            var query = _context.SoyteInfo;

            var results = query
                .OrderByDescending(x => x.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<SoyteInfo>(results, pageIndex, pageSize, query.Count());
        }

        public SoyteInfo AddSoyteInfo(SoyteInfo newSoyteInfo)
        {
            return _context.SoyteInfo.Add(newSoyteInfo);
        }

        public void UpdateSoyteInfon(SoyteInfo updateSoyteInfo)
        {
            _context.Entry(updateSoyteInfo).State = System.Data.Entity.EntityState.Modified;
        }
        public SoyteInfo GetInfoSoyte(Guid id)
        {
            return _context.SoyteInfo.FirstOrDefault(x => x.Id == id);
        }
        public List<SoyteInfo> GetListInfoFromSoyte()
        {
            var vanban = _context.SoyteInfo.Where(x => x.Type == "VANBAN").Take(2).OrderByDescending(x=>x.NgayApdung);
            var thongbao = _context.SoyteInfo.Where(x => x.Type == "THONGBAO").Take(2).OrderByDescending(x => x.NgayApdung);
            var thumoi = _context.SoyteInfo.Where(x => x.Type == "THUMOI").Take(2).OrderByDescending(x => x.NgayApdung);
            var rs = vanban.Concat(thongbao).Concat(thumoi).ToList();
            return rs;
        }
        public List<SoyteInfo> GetListInfoFromSoytebyKey(string key)
        {
            var rs = _context.SoyteInfo.Where(x => x.Type == key).OrderByDescending(x => x.NgayApdung).ToList();
          
            return rs;
        }
    }
}
