using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Context;
using Common.DTO;
using System;
using Persistence.Models;
using Common.Enums;
using Mapster;

namespace CavavinAPI.Controllers
{
    [ApiController]
    [Route("api/vins")]
    public class VinController : Controller
    {
        VinService _vinService;
        public VinController()
        {
            _vinService = new VinService();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var retour = await _vinService.RecupererListeVin();
            //Appeler service Vin avec methode RecupererListeVin()
            return Ok(retour);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var retour = await _vinService.SelectVinByID(id);
            //Appeler service Vin avec methode RecupererListeVin()
            return Ok(retour);
        }

        [HttpPut]
        public async Task<IActionResult> Modify(Guid id, VinDTO vinDTO)
        {

            if (id != vinDTO.Id)
            {
                return BadRequest();
            }

            var retour = await _vinService.ModifierVin(id, vinDTO);

            if (retour == (int)EnumErreurs.EtagereNonTrouvee)
            {
                return StatusCode(500);

            }
            if (retour == (int)EnumErreurs.ErreurSauvegardeEnBDD)
            {
                return StatusCode(500);
            }

            return Ok(retour + " lignes modifiées.");

        }
        [HttpPost]
        public async Task<IActionResult> PostVin(VinDTO vinDTO)             //IActionResult est une reponse Http de notre WebApi (ActionResult a sa place uniquement ici)
        {

            var retour = await _vinService.CreerVin(vinDTO);

            return CreatedAtAction("Get", new { id = vinDTO.Id }, vinDTO);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteVin(Guid id)
        {
            var retour = await _vinService.SupprimerVin(id);

            return Ok(retour);
        }
    }
}
