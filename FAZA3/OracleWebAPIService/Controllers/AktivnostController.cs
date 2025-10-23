using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AktivnostController : ControllerBase
    {
        [HttpGet]
        [Route("SveAktivnosti")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SveAktivnosti()
        {
            (bool isError, List<AktivnostPregled>? aktivnosti, var error) = await DataProvider.GetAllAktivnostiAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(aktivnosti);
        }

        [HttpGet]
        [Route("Aktivnost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Aktivnost(int id)
        {
            (bool isError, AktivnostPregled? aktivnost, var error) = await DataProvider.GetAktivnostAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(aktivnost);
        }

        [HttpPost]
        [Route("DodajAktivnost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DodajAktivnost([FromBody] AktivnostPregled aktivnost)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddAktivnostAsync(aktivnost);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Aktivnost je uspešno dodata.");
        }

        [HttpPut]
        [Route("AzurirajAktivnost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajAktivnost([FromBody] AktivnostPregled aktivnost)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateAktivnostAsync(aktivnost);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Aktivnost je uspešno ažurirana.");
        }

        [HttpDelete]
        [Route("ObrisiAktivnost/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiAktivnost(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteAktivnostAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Aktivnost je uspešno obrisana.");
        }


    }
}
