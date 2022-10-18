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
    [Route("api/etageres")]
    public class EtagereController : Controller
    {
        EtagereService _etagereService;
        public EtagereController()
        {
            _etagereService = new EtagereService();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var retour = await _etagereService.RecupererListeEtagere();
            //Appeler service Etagere avec methode RecupererListeEtagere()
            return Ok(retour);
    
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var retour = await _etagereService.SelectEtagereById(id);
            //Appeler service Etagere avec methode RecupererListeEtagere()
            return Ok(retour);
        }
        [HttpPut]
        public async Task<IActionResult> PutEtagere(Guid id, EtagereDTO etagereDTO)
        {
            if (id != etagereDTO.Id)
            {
                return BadRequest();
            }

            var retour = await _etagereService.ModifierEtagere(id, etagereDTO);

            if(retour == (int)EnumErreurs.EtagereNonTrouvee)
            {
                return StatusCode(500);
               
            }
            if(retour == (int)EnumErreurs.ErreurSauvegardeEnBDD)
            {
                return StatusCode(500);
            }
            
            return Ok(retour + " lignes modifiées.");
        }
        [HttpPost]
        public async Task<IActionResult> PostEtagere(EtagereDTO etagereDTO)
        {

            var retour = await _etagereService.CreerEtagere(etagereDTO);

            return CreatedAtAction("Get", new { id = etagereDTO.Id }, etagereDTO);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEtagere(Guid id)
        {
            var retour = await _etagereService.SupprimerEtagere(id);

            return Ok(retour);
        }
    }
}
