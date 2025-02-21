using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R4._01_TP4.Models.DataManager;
using R4._01_TP4.Models.EntityFramework;
using R4._01_TP4.Models.Repository;

namespace R4._01_TP4.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        //private readonly FilmsRatingDBContext _context;
        //private readonly UtilisateurManager utilisateurManager;
        private readonly IDataRepository<Utilisateur> dataRepository;

        public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return dataRepository.GetAll();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetUtilisateurById")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            var utilisateur = dataRepository.GetByIdAsync(id);

            if (utilisateur.Result == null)
            {
                return NotFound();
            }

            return utilisateur.Result;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.UtilisateurId)
            {
                return BadRequest();
            }

            //_context.Entry(utilisateur).State = EntityState.Modified;
            var userToUpdate = dataRepository.GetByIdAsync(id);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UtilisateurExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

            if(userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                dataRepository.UpdateAsync(userToUpdate.Result.Value, utilisateur);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            //_context.Utilisateurs.Add(utilisateur);
            //await _context.SaveChangesAsync();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await dataRepository.AddAsync(utilisateur);

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.UtilisateurId }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            var utilisateur = dataRepository.GetByIdAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            //_context.Utilisateurs.Remove(utilisateur);
            //await _context.SaveChangesAsync();
            await dataRepository.DeleteAsync(utilisateur.Result.Value);

            return NoContent();
        }

        //private bool UtilisateurExists(int id)
        //{
        //    return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
        //}

        // GET: api/Utilisateurs/toto@gmail.com
        [HttpGet("{email}")]
        [ActionName("GetUtilisateurByEmail")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
        {
            //var utilisateur = _context.Utilisateurs.Where(p => p.Mail == email.ToLower()).FirstOrDefault();
            var utilisateur = await dataRepository.GetByStringAsync(email);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }
    }
}
