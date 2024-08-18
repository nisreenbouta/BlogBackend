using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly SubscriberService _subscriber;

        public SubscriberController(SubscriberService subscriberService)
        {
            _subscriber = subscriberService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Subscriber>>> GetAllSubscribersAsync()
        {
            return await _subscriber.GetAllSubscribersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetSubscriberByIdAsync(string id)
        {
            var subscriber = await _subscriber.GetSubscriberByIdAsync(id);

            if (subscriber == null)
            {
                return NotFound();
            }

            return subscriber;
        }

        [HttpPost]
        public async Task<ActionResult<Subscriber>> CreateSubscriberAsync(Subscriber subscriber)
        {
            await _subscriber.CreateSubscriberAsync(subscriber);
            return CreatedAtRoute(nameof(GetSubscriberByIdAsync), new { id = subscriber.Id }, subscriber);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubscriberStatusAsync(string id, bool isActive)
        {
            var subscriber = await _subscriber.GetSubscriberByIdAsync(id);

            if (subscriber == null)
            {
                return NotFound();
            }

            await _subscriber.UpdateSubscriberStatusAsync(id, isActive);

            return NoContent();
        }


    }
}
