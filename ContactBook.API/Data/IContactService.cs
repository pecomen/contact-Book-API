using ContactBook.Data;
using ContactBook.DTO;

public interface IContactService
    {
        public List<Contact> GetAllContacts();

        public Contact GetContact(int id);

       public bool AddContact(Contact contact );

      public bool UpdateContact(Contact contact);

       public bool DeleteContact(Contact contact);

}

