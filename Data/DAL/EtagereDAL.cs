using Persistence.Context;
using Persistence.Models;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


            ///Exemple de DataLayer non monolithique, Structure en layer

namespace Data.DAL
{

    public class EtagereDAL
    {
      //  private readonly VinContext _context;
        /*public EtagereDAL(VinContext context)       //Initalisation de context
        {
            _context = context;
        }*/
        public async Task<IEnumerable<Etagere>> GetAllEtagere(VinContext context)
        {
            return await context.Etageres.Include(etagere => etagere.Vins).ToListAsync();
        }
        public async Task<Etagere> GetEtagereById(VinContext context ,Guid id)
        {
            var etagere = await context.Etageres.Include(etagere => etagere.Vins).FirstOrDefaultAsync(etagere => etagere.Id == id);

           /* if (etagere == null)
            {
                return new Etagere();                 //Plusieurs return = bonne pratique. Ici renvoie un objet vide en cas d'etagere null
            }
           */
            return etagere;
        }
        public async Task<int> ModifyEtagere(VinContext context, Guid id, Etagere etagere)
        {


            context.Entry(etagere).State = EntityState.Modified;

            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtagereExists(context, id))
                {
                    return (int)EnumErreurs.EtagereNonTrouvee;
                }
                else
                {
                    throw;
                }

                // return 0;
            }



            /*   try
               {
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!EtagereExists(id))
                   {
                       return new Etagere();
                   }
                   else
                   {
                       throw;
                   }
               }
            */
        }
        public async Task<int> CreateEtagere(VinContext context, Etagere etagere)
        {

            // chien.CreatedDate = DateTime.Now;
            etagere.DateCreation = DateTime.Now;

            context.Etageres.Add(etagere);

            //await _context.SaveChangesAsync();
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return (int)EnumErreurs.ErreurSauvegardeEnBDD;
                // return 0;
            }
        }
        public async Task<int> DeleteEtagere(VinContext context, Guid id)
        {
            var etagere = await context.Etageres.FindAsync(id);


            if (etagere == null)
            {
                return (int)EnumErreurs.EtagereNonTrouvee;
            }
            else
            {
                context.Etageres.Remove(etagere);
                return await context.SaveChangesAsync();
            }

        }
        private bool EtagereExists(VinContext context, Guid id)
        {
            return context.Etageres.Any(e => e.Id == id);
        }


    }
}
