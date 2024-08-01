using API.Entities;
using API.Utilities;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Service.IzinServices
{
    public class OffService : IOffService
    {
        private readonly AppDbContext _dbContext;

        public OffService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> CreateIzin(OffModel model)
        {
            Logger.Log(message: $"Izin ekleme islemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);
            try
            {
                Off izinModel = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BaslangicTarihi = model.BaslangicTarihi,
                    BitisTarihi = model.BitisTarihi,
                    IzinAdresi = model.IzinAdresi,
                    IzinSebepi = model.IzinSebepi,
                };

                _dbContext.Offs.Add(izinModel);
                await _dbContext.SaveChangesAsync();


                return "Izin oluşturuldu";
            }
            catch (Exception ex)
            {
                Logger.Log($"CreateIzin => {ex}", LogFileType.Izin, LogLevelType.Error);
                return "Izin oluşturulurken bir hata oluştu: " + ex.Message;
            }
        }

        public async Task<Entities.Off?> UpdateApproveIzin(OffApproveModel izin)
        {
            try
            {
                Logger.Log(message: $"Izin onaylama işlemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);

                var izinNew = await _dbContext.Offs.FindAsync(izin.Id);
                if (izinNew is null)
                {
                    return null;
                }

                izinNew.OnayVerildi = izin.OnayVerildi;
                izinNew.OnayTarihi = DateTime.Now;
                _dbContext.Offs.Update(izinNew);
                await _dbContext.SaveChangesAsync();
                return izinNew;
            }
            catch (Exception ex)
            {
                Logger.Log($"UpdateApproveIzin => {ex}", LogFileType.Izin, LogLevelType.Error);
                return null;
            }
        }


        public async Task<string> DeleteIzin(int Id)
        {
            try
            {
                var izin = await _dbContext.Set<Off>().FindAsync(Id);
                if (izin != null)
                {
                    _dbContext.Remove(izin);
                    await _dbContext.SaveChangesAsync();
                    return "İzin silindi";
                }
                Logger.Log(message: $"Izin silme islemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);
                return "İzin Bulunamadı";
            }
            catch (Exception ex)
            {
                Logger.Log($"DeleteIzin => {ex}", LogFileType.Izin, LogLevelType.Error);
                throw new Exception("İzinleri silme işleminde bir hata oluştu: " + ex.Message);
            }

        }


        public async Task<List<Off>> GetAllIzin()
        {
            try
            {
                Logger.Log(message: $"Izin listeleme islemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);

                return await _dbContext.Set<Off>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Log($"GetAllIzin => {ex}", LogFileType.Izin, LogLevelType.Error);
                throw new Exception("İzinleri listeleme işleminde bir hata oluştu: " + ex.Message);
            }
        }

        public async Task<Off> GetByIdIzin(int Id)
        {
            try
            {
                Logger.Log(message: $"Tek bir Izin listeleme islemi yaılıyor.", LogFileType.Izin, LogLevelType.Info);

                return await _dbContext.Set<Off>().FindAsync(Id);
            }
            catch (Exception ex)
            {
                Logger.Log($"GetByIdIzin => {ex}", LogFileType.Izin, LogLevelType.Error);
                throw new Exception("ID'ye göre izni getirme işleminde bir hata oluştu: " + ex.Message);
            }

        }
        public async Task<string> UpdateIzin(Off izin)
        {
            try
            {
                Logger.Log(message: $"Izin guncelleme islemi yapılıyor.", LogFileType.Izin, LogLevelType.Info);

                var izinNew = await _dbContext.Offs.FindAsync(izin.Id);
                if (izinNew != null)
                {
                    izinNew.CreatedDate = DateTime.Now;
                    izinNew.BaslangicTarihi = izin.BaslangicTarihi;
                    izinNew.BitisTarihi = izin.BitisTarihi;
                    izinNew.IzinAdresi = izin.IzinAdresi;
                    izinNew.IzinSebepi = izin.IzinSebepi;
                    izinNew.OnayVerildi = izin.OnayVerildi;

                    await _dbContext.SaveChangesAsync();
                    return "Kişinin izni güncellendi";
                }
                return "İlgili izin bulunamadı";
            }
            catch (Exception ex)
            {
                Logger.Log($"UpdateIzin => {ex}", LogFileType.Izin, LogLevelType.Error);
                return "İlgili izin güncellenirken hata oluştu";

            }

        }
    }
}