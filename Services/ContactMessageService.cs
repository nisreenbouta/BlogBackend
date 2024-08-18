using MongoDB.Driver;

namespace BlogApi.Services
{
    public class ContactMessageService
    {
        private readonly IMongoCollection<ContactMessage> _contactMessage;

        //This constructor  is used to create an instance of the BlogService class with the necessary database connection and collection.
        public ContactMessageService(IMongoDatabase database) 
        {
            _contactMessage = database.GetCollection<ContactMessage>("ContactMessage");
        }

        //get all ContactMessages
       public async Task<List<ContactMessage>> GetAllContactMessagesAsync()
        {
            return await _contactMessage.Find(contactMessage => true).ToListAsync();
        }

        //get ContactMessage by id
        public async Task<ContactMessage> GetContactMessageByIdAsync(string id) {
            return await _contactMessage.Find<ContactMessage>(contactMessage => contactMessage.Id == id).FirstOrDefaultAsync();
        }

        //create ContactMessage
        public async Task<ContactMessage> CreateContactMessageAsync(ContactMessage contactMessage)
        {
            await _contactMessage.InsertOneAsync(contactMessage);
            return contactMessage;
        }
        
        //delete ContactMessage
        public async Task RemoveContactMessageAsync(string id) {
             await _contactMessage.DeleteOneAsync(contactMessage => contactMessage.Id == id);
        }
    }
}
