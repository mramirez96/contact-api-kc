using Domain;
using Domain.Request;
using Infraestructure.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Handling contacts
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            var added = await _contactService.Create(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = added.Id }, added);
        }

        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        /// <response code="200">Returns the updated item</response>
        /// <response code="404">Item to update not found</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> UpdateContact(Contact contact)
        {
            var updated = await _contactService.Update(contact);
            return Ok(updated);
        }

        /// <summary>
        /// Get contact by id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the found item</response>
        /// <response code="404">Item not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> GetContactById([FromRoute] int id)
        {
            return Ok(await _contactService.Get(id));
        }

        /// <summary>
        /// Get contact by email
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the found item</response>
        /// <response code="404">Item not found</response>
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> GetContactByEmail([FromRoute] string email)
        {
            return Ok(await _contactService.Get(email));
        }

        /// <summary>
        /// Get contact by phone number
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the found item</response>
        /// <response code="404">Item not found</response>
        [HttpGet("phone/{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> GetContactByPhone([FromRoute] string phoneNumber)
        {
            return Ok(await _contactService.GetByPhone(phoneNumber));
        }

        /// <summary>
        /// Get contacts by city or coutry
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the found items</response>
        /// <response code="404">Items not found</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Contact> SearchContacts([FromQuery] SearchContactRequest request)
        {
            return Ok(_contactService.Search(request));
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Item deleted</response>
        /// <response code="404">Item not found</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact>> Delete([FromRoute] int id)
        {
            await _contactService.Delete(id);
            return NoContent();
        }

    }
}
