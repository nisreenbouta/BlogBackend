using MongoDB.Driver;

namespace BlogApi.Services{

    public class SubscriberService{

        private readonly IMongoCollection<Subscriber> _subscriber;

        //This constructor  is used to create an instance of the SubscriberService class with the necessary database connection and collection.

        public SubscriberService(IMongoDatabase database){
            _subscriber = database.GetCollection<Subscriber>("Subscriber");
        }

        //get all subscribers
        public async Task<List<Subscriber>> GetAllSubscribersAsync(){
            return await _subscriber.Find(subscriber => true).ToListAsync();
        }
        //get subscriber by id
        public async Task<Subscriber> GetSubscriberByIdAsync(string id) {
            return await _subscriber.Find<Subscriber>(subscriber => subscriber.Id == id).FirstOrDefaultAsync();
        }

        //create subscriber
        public async Task<Subscriber> CreateSubscriberAsync(Subscriber subscriber)
        {
            await _subscriber.InsertOneAsync(subscriber);
            return subscriber;
        }
        
         // Update Subscriber's IsActive status
        public async Task UpdateSubscriberStatusAsync(string id, bool isActive)
        {
            var update = Builders<Subscriber>.Update.Set(subscriber => subscriber.IsActive, isActive);
            await _subscriber.UpdateOneAsync(subscriber => subscriber.Id == id, update);
        }


    }

}