using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LokacijaController : ControllerBase
    {
        [HttpGet]
        [Route("VratiAktivnostiNaLokaciji/{nazivLokacije}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiAktivnostiNaLokaciji(string nazivLokacije)
        {
            (bool isError, var aktivnosti, var error) = await DataProvider.GetAktivnostiNaLokacijiAsync(nazivLokacije);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            if (aktivnosti == null || aktivnosti.Count == 0)
                return NotFound($"Na lokaciji '{nazivLokacije}' nema registrovanih aktivnosti.");

            return Ok(aktivnosti);
        }

        [HttpGet]
        [Route("VratiSveLokacije")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiSveLokacije()
        {
            (bool isError, var lokacije, var error) = await DataProvider.GetAllLokacijeAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(lokacije);
        }

        [HttpGet]
        [Route("VratiLokaciju/{naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiLokaciju(string naziv)
        {
            (bool isError, var lokacija, var error) = await DataProvider.GetLokacijaAsync(naziv);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            if (lokacija == null)
                return NotFound($"Lokacija sa nazivom '{naziv}' nije pronađena.");

            return Ok(lokacija);
        }

        [HttpPost]
        [Route("DodajLokaciju")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DodajLokaciju([FromBody] LokacijaPregled lokacija)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddLokacijaAsync(lokacija);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return StatusCode(201, "Lokacija je uspešno sačuvana.");
        }

        [HttpPut]
        [Route("AzurirajLokaciju")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajLokaciju([FromBody] LokacijaPregled lokacija)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateLokacijaAsync(lokacija);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok("Lokacija je uspešno ažurirana.");
        }

        [HttpDelete]
        [Route("ObrisiLokaciju/{naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiLokaciju(string naziv)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteLokacijaAsync(naziv);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok("Lokacija je uspešno obrisana.");
        }
    }
}
