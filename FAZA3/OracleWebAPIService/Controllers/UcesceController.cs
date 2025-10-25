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
    public class UcesceController : ControllerBase
    {
        [HttpGet]
        [Route("VratiAngazovanaLica/{aktivnostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiAngazovanaLica(int aktivnostId)
        {
            try
            {
                List<AngazovanoLicePregled> angazovanaLica =
                    await DataProvider.GetAngazovanaLicaNaAktivnostiAsync(aktivnostId);

                if (angazovanaLica == null || angazovanaLica.Count == 0)
                    return NotFound("Nema angažovanih lica za ovu aktivnost.");

                return Ok(angazovanaLica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Greška prilikom učitavanja angažovanih lica: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("DodajAngazovanoLice")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DodajAngazovanoLice([FromQuery] string jmbg, [FromQuery] int aktivnostId)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddAngazovanoLiceNaAktivnostAsync(jmbg, aktivnostId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Angažovano lice je uspešno dodato na aktivnost.");
        }

        [HttpDelete]
        [Route("UkloniAngazovanoLice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UkloniAngazovanoLice([FromQuery] string jmbg, [FromQuery] int aktivnostId)
        {
            (bool isError, bool ok, var error) = await DataProvider.RemoveAngazovanoLiceFromAktivnostAsync(jmbg, aktivnostId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Angažovano lice je uspešno uklonjeno sa aktivnosti.");
        }
    }
}
