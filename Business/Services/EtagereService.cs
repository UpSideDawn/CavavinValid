using Common.DTO;
using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Context;
using Persistence.Models;
using Mapster;
using System.Collections.Generic;

namespace Business.Services
{

    public class EtagereService
    {
        // private readonly IConfiguration _configuration;
        private EtagereDAL _dal;

        //private string _connectionString;
        DbContextOptionsBuilder<VinContext> _optionsBuilder;
        public EtagereService(/*IConfiguration configuration*/)
        {
            _dal = new EtagereDAL();
            //    _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<VinContext>();
            // _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _optionsBuilder.UseInMemoryDatabase("BaseCave");
        }



        public async Task<IEnumerable<EtagereDTO>> RecupererListeEtagere()
        {
            IEnumerable<Etagere> etagereModelResultat = null;               //Definit une variable de type Task<ActionResult> (Meme type que le retour de GetAllEtagere)

            //EtagereDAL etagereDAL = new EtagereDAL();
            using (VinContext _context = new VinContext(_optionsBuilder.Options))               //Initialise une variable valable uniquement entre les cotes
            {


                etagereModelResultat = await _dal.GetAllEtagere(_context);
            }

            return etagereModelResultat.Adapt<IEnumerable<EtagereDTO>>();
            //etagereDAL.GetAllEtagere();
            //return etagereModelResultat.Select(x => x.Adapt<EtagereDTO>());
        }

        public Task<int> SupprimerEtagere(Guid id)
        {
            Task<int> CodeRetourSupprEtagere = null;

            using (VinContext _context = new VinContext(_optionsBuilder.Options))               //Initialise une variable valable uniquement entre les cotes
            {

                CodeRetourSupprEtagere = _dal.DeleteEtagere(_context, id);

            }

            return CodeRetourSupprEtagere;
        }

        public Task<int> ModifierEtagere(Guid id, EtagereDTO etagereDTO)
        {
            Task<int> CodeRetourResultat = null;

            using (VinContext _context = new VinContext(_optionsBuilder.Options))               //Initialise une variable valable uniquement entre les cotes
            {
                var etagere = etagereDTO.Adapt<Etagere>();
                CodeRetourResultat = _dal.ModifyEtagere(_context, id, etagere);

            }
            return CodeRetourResultat;
        }
        public async Task<EtagereDTO> SelectEtagereById(Guid id)
        {
            ActionResult<Etagere> etagereModelResultat = null;

            using (VinContext _context = new VinContext(_optionsBuilder.Options))               //Initialise une variable valable uniquement entre les cotes
            {

                etagereModelResultat = await _dal.GetEtagereById(_context, id);

            }
            return etagereModelResultat.Adapt<EtagereDTO>();
        }

        public Task<int> CreerEtagere(EtagereDTO etagereDTO)
        {
            Task<int> etagereCodeRetour = null;

            using (VinContext vinContext = new VinContext(_optionsBuilder.Options))
            {
                var etagere = etagereDTO.Adapt<Etagere>();
                etagereCodeRetour = _dal.CreateEtagere(vinContext, etagere);
            }
            return etagereCodeRetour;
        }
    }
}
