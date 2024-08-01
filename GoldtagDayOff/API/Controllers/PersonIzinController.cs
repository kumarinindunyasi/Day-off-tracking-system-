using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Service.PersonsServices;
using API.Service.IzinServices;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Service.EmailSenderServices;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PersonIzinController: ControllerBase
    {
        private readonly IPersonsService _personsService;
        private readonly IOffService _izinService;
        private readonly IEmailSenderService _emailSender;
       

        public PersonIzinController(IPersonsService personsService, IOffService izinService, IEmailSenderService emailSender)
        {
            _personsService = personsService;
            _izinService = izinService;
            _emailSender = emailSender;
        }
        
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personsService.GetAll();
            return Ok(persons);
        }

        [HttpGet("getbyID{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _personsService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet]
        [Route("getallIzin")]
        public async Task<IActionResult> GetAllIzin()
        {
            var offs = await _izinService.GetAllIzin();
            return Ok(offs);
        }

        [HttpGet("Izin{Id}")]
        public async Task<IActionResult> GetByIdIzin(int Id)
        {
            var person = await _izinService.GetByIdIzin(Id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }


        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(string email, string password)
        {
            return Ok(await _personsService.Authenticate(email, password));
        }

        [HttpPost]
        [Route("createperson")]
        public async Task<IActionResult> CreatePerson([FromBody]PersonModel model)
        {
            var result = await _personsService.CreatePerson(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("createIzin")]
        public async Task<IActionResult> CreateIzin(OffModel model)
        {
            var result = await _izinService.CreateIzin(model);
            var reciever = "karsokenbsr@gmail.com";
            var subject = "Test";
            var message = result;
            await _emailSender.SendEmailAsync(reciever, subject, message);

            return Ok(result);
        }


       [HttpPut("OnayGuncelle/{Id}")]
        public async Task<IActionResult> UpdateApproveIzin([FromBody] OffApproveModel izin)
        {
            var result = await _izinService.UpdateApproveIzin(izin);
            return Ok(result);

        }


        [HttpPut("PersonGuncelle/{Id}")]
        public async Task<IActionResult> UpdatePerson(int Id, [FromBody] Person person)
        {
            Console.WriteLine("Bilge Methodu Çağırdı");
            if (Id != person.Id)
            {
                Console.WriteLine("Bilge");
                return BadRequest();
            }

            var result = await _personsService.UpdatePerson(person);
            return Ok(result);
        }

        [HttpPut("IzinGuncelle/{Id}")]
        public async Task<IActionResult> UpdateIzin(Off izin)
        {

            var result = await _izinService.UpdateIzin(izin);
            return Ok(result);
        }


        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeletePerson(int Id)
        {
            var result = await _personsService.DeletePerson(Id);
            return Ok(result);
        }
        /// <summary>
        /// İzin Sil
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Deleteızin/{Id}")]
        public async Task<IActionResult> DeleteIzin(int Id)
        {
            var result = await _izinService.DeleteIzin(Id);
            return Ok(result);
        }
        



    }
}