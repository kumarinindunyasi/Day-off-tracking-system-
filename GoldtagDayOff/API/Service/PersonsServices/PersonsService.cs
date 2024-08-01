using API.Entities;
using API.Utilities;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using API.Models;

namespace API.Service.PersonsServices
{
    public class PersonsService : IPersonsService
    {
        private readonly AppDbContext _dbContext;

        public PersonsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AuthResultModel> Authenticate(string email, string password)
        {
            Logger.Log(message: $"Dogrulama islemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);

            try
            {
                var user = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);

                if (user != null)
                {
                    return new AuthResultModel()
                    {
                        Succes = true,
                        Message = "Islem Basarili.",
                        Id = user.Id,
                        RoleId = (int)user.Role
                    };
                }
                else
                {
                    return new AuthResultModel() { Succes = false, Message = "Islem Basarisiz!." };
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Authenticate => {ex}", LogFileType.Person, LogLevelType.Error);
                return new AuthResultModel() { Succes = false, Message = "Bir Hata Olustu." };
            }
        }


        public async Task<string> CreatePerson(PersonModel model)
        {
            Logger.Log(message: "Kişi ekleme işlemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);

            try
            {
                if (!checkEmail(model.Email))
                    return "Geçersiz e-posta formatı";

                Person personModel = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    TelNo = model.TelNo,
                    Unvan = model.Unvan,
                    Email = model.Email,
                    Password = model.Password,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Role = model.Role
                };

                _dbContext.Persons.Add(personModel);
                await _dbContext.SaveChangesAsync();

                return "Kişi oluşturuldu";
            }
            catch (Exception ex)
            {
                Logger.Log($"CreatePerson => {ex}", LogFileType.Person, LogLevelType.Error);
                return "Kişi oluşturulurken bir hata oluştu: " + ex.Message;
            }
        }

        private bool checkEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (!email.Contains("@"))
                return false;

            if (email.EndsWith(".com") || email.EndsWith(".org") || email.EndsWith(".edu") || email.EndsWith(".ca") || email.EndsWith(".uk"))
                return true;

            return false;
        }


        public async Task<string> DeletePerson(int Id)
        {
            Logger.Log(message: $"Kisi silme islemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);

            try
            {
                var person = await _dbContext.Set<Person>().FindAsync(Id);
                if (person != null)
                {
                    _dbContext.Remove(person);
                    await _dbContext.SaveChangesAsync();
                    return "Kişi başarıyla silindi";
                }
                return "Kişi Bulunamadı";
            }
            catch (Exception ex)
            {
                Logger.Log($"DeletePerson => {ex}", LogFileType.Person, LogLevelType.Error);
                return "Kişi silinirken bir hata oluştu: " + ex.Message;
            }
        }


        public async Task<List<Person>> GetAll()
        {
            Logger.Log(message: $"Kisileri listeleme islemi yaılıyor.", LogFileType.Izin, LogLevelType.Info);

            try
            {
                return await _dbContext.Set<Person>().AsNoTracking().ToListAsync();

            }

            catch (Exception ex)
            {
                Logger.Log($"GetAll => {ex}", LogFileType.Person, LogLevelType.Error);
                throw new Exception("Kişileri listeleme işleminde bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<Person?> GetById(int id)
        {
            Logger.Log(message: $"Tek bir kisi listeleme islemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);

            try
            {
                return await _dbContext.Persons.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.Log($"GetById => {ex}", LogFileType.Person, LogLevelType.Error);
                throw new Exception("Kişiyi ID'ye göre getirme işleminde bir hata oluştu: " + ex.Message);
            }

        }

        public async Task<string> UpdatePerson(Person person)
        {
            Logger.Log(message: $"Kisi guncelleme islemi yaılıyor.", LogFileType.Izin, LogLevelType.Info);

            try
            {
                /*Person personModel = new()
                {
                    IsActive= person.IsActive,
                    TelNo= person.TelNo,
                    FirstName= person.FirstName,
                    LastName= person.LastName,
                    Email= person.Email,
                    Role= person.Role,
                    Password= person.Password,

                }*/
                var personNew = await _dbContext.Persons.FindAsync(person.Id);
                if (personNew != null)
                {
                    personNew.IsActive = person.IsActive;
                    personNew.TelNo = person.TelNo;
                    personNew.FirstName = person.FirstName;
                    personNew.LastName = person.LastName;
                    personNew.Email = person.Email;
                    personNew.Role = person.Role;
                    personNew.Password = person.Password;
                    personNew.Unvan = person.Unvan;

                    await _dbContext.SaveChangesAsync();
                    return "Kişi bilgileri başarıyla güncellendi";
                }
                return "İlgili kişi bulunamadı";
            }
            catch (Exception ex)
            {
                Logger.Log($"UpdatePerson => {ex}", LogFileType.Person, LogLevelType.Error);
                return "İlgili kişi güncellenirken hata oluştu.";

            }

        }
    }
}
