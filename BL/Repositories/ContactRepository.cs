using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using Dll;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class ContactRepository : BaseRepository<Contact>
    {
        private DbContext U_DbContext;

        public ContactRepository(DbContext U_DbContext) : base(U_DbContext)
        {
            this.U_DbContext = U_DbContext;
        }
        #region CRUB

        public List<Contact> GetAllContact()
        {
            return GetAll().ToList();
        }
        public bool InsertContact(Contact contact)
        {
            return Insert(contact);
        }
        public void UpdateContact(Contact contact)
        {
            Update(contact);
        }
        public void DeleteContact(int id)
        {
            Delete(id);
        }
        public Contact GetContactByName(string name)
        {
            return GetWhere(Cont =>  Cont.UserName == name).FirstOrDefault();
           
        }
        #endregion
    }
}
