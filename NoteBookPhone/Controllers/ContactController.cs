using BL.AppServices;
using BL.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NoteBookPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        ContactAppService _ContactAppService;

        public ContactController(ContactAppService contactAppService)
        {
            this._ContactAppService = contactAppService;
        }
       
        [HttpGet("/GetAllContant")]
        public IActionResult GetAllContaces()
        {
            return Ok(_ContactAppService.GetAllContact());
        }
        
        [HttpGet("/SearchContactByName/{name}")]
        public IActionResult GetContactByName(string name)
        {
            return Ok(_ContactAppService.SearchContact(name));
        }

       
        [HttpPost("/NewContact")]
        public IActionResult Create(ContactViewModel contactViewModel)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                    _ContactAppService.SaveNewContact(contactViewModel);
                    return Created("AddNewContact", contactViewModel);
               

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    
        [HttpPut("/UpdateContact/{id}")]
        public IActionResult Edit(int id, ContactViewModel contactViewModel)
        {

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _ContactAppService.UpdateContact(contactViewModel);
                return Ok(contactViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
        [HttpDelete("/DeleteContact/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _ContactAppService.DeleteContact(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
