using System.Security.Claims;
using BL.AppServices;
using BL.Dto;
using Dll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteBookPhone.HelpClasses;

namespace NoteBookPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private AccountAppService _accountAppService;
        IHttpContextAccessor _httpContextAccessor;
        public AccountController(
           AccountAppService accountAppService,
           IHttpContextAccessor httpContextAccessor)
        {
            this._accountAppService = accountAppService;
            this._httpContextAccessor = httpContextAccessor;
        }
      
        

        [HttpPost("/Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel model)
        {

            return await Register(model);

        }
        private async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_accountAppService.CheckAccountExistsByData(model))
            {
                var result = await _accountAppService.Register(model);

                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = result.Errors.FirstOrDefault().Description });

                ApplicationUsersIdentity identityUser = await _accountAppService.FindByName(model.UserName);
              
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            else
            {
                return BadRequest();
            }
        }
       
        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] loginViewModel model)
        {
            var user = await _accountAppService.FindByName(model.UserNmae);
            if (user != null)
            {
                dynamic token = await _accountAppService.CreateToken(user);

                return Ok(token);
            }
            return Unauthorized();
        }

       



    }
}
