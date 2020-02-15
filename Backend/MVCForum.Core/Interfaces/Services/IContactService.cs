using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCForum.Domain.Interfaces.Services
{
    public interface IContactService
    {
        /// <summary>
        /// get all Contact enabled
        /// </summary>
        /// <returns></returns>
        IList<ContactForm> GetallContact(int pageIndex, int pageSize);

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contact"></param>
        void Delete(ContactForm contact);
        /// <summary>
        /// get list SearchContact by keysearch
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<ContactForm> SearchContact(string search, int pageIndex, int pageSize);
        PagedList<ContactForm> GetAllContactPost(int pageIndex, int pageSize);
        ContactForm Get(Guid id);
        List<ContactForm> GetAllContacts();

        /// <summary>
        /// All Contact
        /// </summary>
        /// <returns></returns>
        IEnumerable<ContactForm> GetAll();

        ContactForm Add(ContactForm newContact);
        /// <summary>
        /// Update ContactForm
        /// </summary>
        /// <param name="updateContact"></param>
        void Update(ContactForm updateContact);
        /// <summary>
        /// Sài ké
        /// </summary>
        /// <param name="newQuestion"></param>
        /// <returns></returns>
        Question AddQuestion(Question newQuestion);
        void UpdateQuestion(Question updateQuestion);
        void DeleteQuestion(Question newQuestion);
        PagedList<Question> SearchQuestions(string search, int pageIndex, int pageSize);
        PagedList<Question> GetAllQuestionPost(int pageIndex, int pageSize);
        List<Question> GellAllNewQuestions();
        Appointment AddAppointment(Appointment appointment);
        Question GetQuestion(Guid id);
        PagedList<Appointment> SearchAppointment(string search, int pageIndex, int pageSize);
        PagedList<Appointment> GetAllAppointmentPost(int pageIndex, int pageSize);
        PagedList<SoyteInfo> GetAllSoytePost(int pageIndex, int pageSize);
        SoyteInfo AddSoyteInfo(SoyteInfo newSoyteInfo);
        void UpdateSoyteInfon(SoyteInfo updateSoyteInfo);
        SoyteInfo GetInfoSoyte(Guid id);
        List<SoyteInfo> GetListInfoFromSoyte();
        List<SoyteInfo> GetListInfoFromSoytebyKey(string key);
        List<Question> GetAllQuestionEditToday();
        PagedList<Question> GetAllQuestionAdminPost(int pageIndex, int pageSize);
        PagedList<Question> GetQuestions(Guid id);
    }
}
