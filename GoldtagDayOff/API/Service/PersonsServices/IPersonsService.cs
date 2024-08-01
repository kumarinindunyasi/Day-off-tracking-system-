using System.Collections.Generic;
using API.Entities;
using API.Models;

namespace API.Service.PersonsServices
{
    public interface IPersonsService
    {
        Task<AuthResultModel> Authenticate(string email, string password);
        Task<string> CreatePerson(PersonModel model);
        Task<string> DeletePerson(int Id);
        Task<List<Person>> GetAll();
        Task<Person> GetById(int Id);
        Task<string> UpdatePerson(Person person);
    }
}
