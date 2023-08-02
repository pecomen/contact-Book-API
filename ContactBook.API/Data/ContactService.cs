namespace ContactBook.Data
{
    public class ContactService : IContactService
    {

        private readonly ContactContext _context;

        public ContactService(ContactContext context)
        {
            _context = context;
        }

        public bool AddContact(Contact contact)
        {
           _context.Add(contact);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteContact(Contact contact)
        {
            _context.Remove(contact);
            _context.SaveChanges();
            return true;
           
        }

        public List<Contact> GetAllContacts()
        {
            return _context.Contacts.ToList();  
        }

        public Contact GetContact(int id)
        {
            var result = _context.Contacts.FirstOrDefault(c => c.Id == id);

            if (result == null)
                return new Contact();
            return result;
        }

        public bool UpdateContact(Contact contact)
        {


            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return true;
        }
    }
}
