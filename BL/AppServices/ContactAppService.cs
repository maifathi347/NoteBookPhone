using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.Bases;
using BL.Dto;
using Dll;

namespace BL.AppServices
{
    public class ContactAppService : AppServiceBase
    {
        public ContactAppService(Interfaces.IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
      
        #region CURD

        public List<ContactViewModel> GetAllContact()
        {

            return Mapper.Map<List<ContactViewModel>>(TheUnitOfWork.contact.GetAllContact());
        }
        public ContactViewModel SearchContact(string name)
        {
            return Mapper.Map<ContactViewModel>(TheUnitOfWork.contact.GetContactByName(name));
        }
        public bool SaveNewContact(ContactViewModel contactViewModel)
        {
            if (contactViewModel == null)

                throw new ArgumentNullException();

            bool result = false;
            var category = Mapper.Map<Contact>(contactViewModel);
            if (TheUnitOfWork.contact.Insert(category))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateContact(ContactViewModel contactViewModel)
        {
            var contact= Mapper.Map<Contact>(contactViewModel);
            TheUnitOfWork.contact.Update(contact);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteContact(int id)
        {
            bool result = false;

            TheUnitOfWork.contact.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

      
        #endregion



    }
}
