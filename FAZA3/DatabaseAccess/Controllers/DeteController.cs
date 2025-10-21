using static Deciji_Letnji_Program.DTOs;
using static Deciji_Letnji_Program.DataProvider;
using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeteController : ControllerBase
    {
  
        [HttpPost]
        [Route("DodajDete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DodajDete([FromBody] DetePregled dete)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddDeteAsync(dete);
            if (isError) return StatusCode(error?.StatusCode ?? 400, error?.Message);
            return StatusCode(201, "Dete je uspešno sačuvano.");
        }

        [HttpGet]
        [Route("VratiSvuDecu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> VratiSvuDecu()
        {
            (bool isError, List<DetePregled>? deca, var error) = await DataProvider.GetAllDecaAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(deca);
        }

        [HttpGet]
        [Route("VratiDete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiDete(int id)
        {
            (bool isError, DetePregled? dete, var error) = await DataProvider.VratiDeteAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(dete);
        }

        [HttpDelete]
        [Route("ObrisiDete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiDete(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteDeteAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok($"Dete sa ID-em {id} je uspešno obrisano.");
        }

        [HttpGet]
        [Route("VratiTelefoneRoditelja/{roditeljId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiTelefoneRoditelja(int roditeljId)
        {
            (bool isError, List<TelefonRoditeljaPregled>? telefoni, var error) =
                await DataProvider.GetTelefoniRoditeljaAsync(roditeljId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(telefoni);
        }

        [HttpGet]
        [Route("VratiEmailoveRoditelja/{roditeljId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiEmailoveRoditelja(int roditeljId)
        {
            (bool isError, List<EmailRoditeljaPregled>? emailovi, var error) =
                await DataProvider.GetEmailoviRoditeljaAsync(roditeljId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(emailovi);
        }

        [HttpGet]
        [Route("VratiDecuNaAktivnosti/{aktivnostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiDecuNaAktivnosti(int aktivnostId)
        {
            (bool isError, List<DetePregled>? deca, var error) =
                await DataProvider.GetDecaNaAktivnostiAsync(aktivnostId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(deca);
        }

        [HttpPut]
        [Route("AzurirajDete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajDete([FromBody] DetePregled dete)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateDeteAsync(dete);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Dete je uspešno ažurirano.");
        }
    }
}
