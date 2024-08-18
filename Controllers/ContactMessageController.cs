using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessageController : ControllerBase
    {
        private readonly ContactMessageService _contactMessageService;

        public ContactMessageController(ContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactMessage>>> GetAllContactMessagesAsync()
        {
            return await _contactMessageService.GetAllContactMessagesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactMessage>> GetContactMessageByIdAsync(string id)
        {
            var contactMessage = await _contactMessageService.GetContactMessageByIdAsync(id);

            if (contactMessage == null)
            {
                return NotFound();
            }

            return contactMessage;
        }

        [HttpPost]
        public async Task<ActionResult<ContactMessage>> CreateContactMessageAsync(ContactMessage contactMessage)
        {
            await _contactMessageService.CreateContactMessageAsync(contactMessage);
            return CreatedAtRoute(nameof(GetContactMessageByIdAsync), new { id = contactMessage.Id }, contactMessage);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactMessageAsync(string id)
        {
            var contactMessage = await _contactMessageService.GetContactMessageByIdAsync(id);

            if (contactMessage == null)
            {
                return NotFound();
            }

            await _contactMessageService.RemoveContactMessageAsync(id);

            return NoContent();
        }
    }
}
