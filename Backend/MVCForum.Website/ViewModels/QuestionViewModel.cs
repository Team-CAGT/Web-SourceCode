using MVCForum.Domain.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCForum.Website.ViewModels
{
    public class QuestionViewModel
    {
        public string FullName { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public Question ToEntity()
        {
            var question = new Question();
            question.Name = FullName;
            question.Age = Age;
            question.Phone = Phone;
            question.Address = Address;
            question.Email = Email;
            question.ContentQuestion = Content;
            question.CreateDate = DateTime.Now;
            question.LastUpdate = DateTime.Now;
            question.Status = "NEW";
            return question;
        }
    }
    public class ContactViewModel
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public ContactForm ToEntity()
        {
            var contact = new ContactForm();
            contact.Name = FullName;
            contact.Phone = Phone;
            contact.Email = Email;
            contact.Content = Message;
            contact.CreateDate = DateTime.Now;
            contact.Status = "NEW";
            return contact;
        }
    }
    public class AppointmentViewModel
    {
        public string FullName { get; set; }
        public bool IsCompany { get; set; }
      
        public string CompanyName { get; set; }
        public int Amount { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Appointment ToEntity()
        {
            var app = new Appointment();
            app.Name = FullName;
            app.Phone = Phone;
            app.Email = Email;
            app.Address = Address;
            app.CreateDate = DateTime.Now;
            app.Status = "NEW";
            app.Amount = Amount;
            return app;
        }
    }
}