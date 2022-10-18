using Persistence.Context;
using Persistence.Models;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Data.DAL
{
    public class VinDAL
    {
        public async Task<IEnumerable<Vin>> GetAllVin(VinContext context)
        {
            return await context.Vins.ToListAsync();
        }
        public async Task<Vin> GetVinById(VinContext context, Guid id)
        {
            var vin = await context.Vins.FindAsync(id);

            if (vin == null)
            {
                return new Vin();                 //Plusieurs return = bonne pratique. Ici renvoie un objet vide en cas d'etagere null
            }

            return vin;
        }
        public async Task<int> ModifyVin(VinContext context, Guid id, Vin vin)
        {


            context.Entry(vin).State = EntityState.Modified;

            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VinExists(context, id))
                {
                    return (int)EnumErreursVin.VinNonTrouve;
                }
                else
                {
                    throw;
                }

                // return 0;
            }
        }
        public async Task<int> CreateVin(VinContext context, Vin vin)
        {

             vin.DateCreation = DateTime.Now;

            context.Vins.Add(vin);

            //await _context.SaveChangesAsync();
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return (int)EnumErreursVin.ErreurSauvegardeEnBDD;
                // return 0;
            }
        }
        public async Task<int> DeleteVin(VinContext context, Guid id)
        {
            var vin = await context.Vins.FindAsync(id);


            if (vin == null)
            {
                return (int)EnumErreursVin.VinNonTrouve;
            }
            else
            {
                context.Vins.Remove(vin);
                return await context.SaveChangesAsync();
            }

        }
        private bool VinExists(VinContext context, Guid id)
        {
            return context.Vins.Any(e => e.Id == id);
        }
    }
}
