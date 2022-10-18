using Common.DTO;
using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Context;
using Persistence.Models;
using Mapster;

namespace Business.Services
{
    public class VinService
    {
        private VinDAL _dal;
        //private string _connectionString;
        DbContextOptionsBuilder<VinContext> _optionsBuilder;
        public VinService(/*IConfiguration configuration*/)
        {
            _dal = new VinDAL();
            //    _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<VinContext>();
            // _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _optionsBuilder.UseInMemoryDatabase("BaseCave");    // Pour vraie base, passer _connexionString en parametres
        }

        public async Task<IEnumerable<VinDTO>> RecupererListeVin()
        {
            IEnumerable<Vin> vinModelResultat = null;           // Le type IEnumerable retourne une liste d'objets Vin

            using (VinContext _context = new VinContext(_optionsBuilder.Options))       //Initialise la connexion a la BDD avec paramètres définis uniquement dans les cotes
            {
                vinModelResultat = await _dal.GetAllVin(_context);
            }
            return vinModelResultat.Adapt<IEnumerable<VinDTO>>();
            
        }

        public async Task<VinDTO> SelectVinByID(Guid id)
        {
            Vin vinModelResultat = null;

            using (VinContext _context = new VinContext(_optionsBuilder.Options))       //Initialise la connexion a la BDD avec paramètres définis uniquement dans les cotes
            {
                vinModelResultat = await _dal.GetVinById(_context, id);
            }

            return vinModelResultat.Adapt<VinDTO>();
        }

        public Task<int> SupprimerVin(Guid id)
        {
            Task<int> codeVinSuppressionResultat = null;

            using(VinContext _context = new VinContext(_optionsBuilder.Options))
            {
                codeVinSuppressionResultat = _dal.DeleteVin(_context, id);
            }
            return codeVinSuppressionResultat;
        }
        public Task<int> ModifierVin(Guid id, VinDTO vinDTO)
        {
            Task<int> codeVinModification = null;

            using (VinContext _context = new VinContext(_optionsBuilder.Options))
            {
                var vin = vinDTO.Adapt<Vin>();                                      //Transformation de vinDTO (Masquant des valeurs au user) en Vin (qui possède tous les champs)
                codeVinModification = _dal.ModifyVin(_context, id, vin);
            }

            return codeVinModification;
        }
        public Task<int> CreerVin(VinDTO vinDTO)
        {
            Task<int> vinCodeRetour = null;

            using (VinContext vinContext = new VinContext(_optionsBuilder.Options))
            {
                var vin = vinDTO.Adapt<Vin>();
                vinCodeRetour = _dal.CreateVin(vinContext, vin);
            }
            return vinCodeRetour;
        }
    }
}
