using static Deciji_Letnji_Program.DTOs;
using static Deciji_Letnji_Program.DataProvider;
using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelefonRoditeljaController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveTelefoneRoditelja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiSveTelefoneRoditelja()
        {
            (bool isError, List<TelefonRoditeljaPregled>? telefoni, var error) =
                await DataProvider.GetAllTelefoniRoditeljaAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(telefoni);
        }
        [HttpGet]
        [Route("VratiTelefon/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiTelefon(int id)
        {
            var (isError, telefon, error) = await DataProvider.GetTelefonRoditeljaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(telefon);
        }

        [HttpPost]
        [Route("DodajTelefon/{deteId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DodajTelefon([FromBody] TelefonRoditeljaPregled telefon, int deteId)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddTelefonRoditeljaAsync(telefon, deteId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Telefon roditelja je uspešno dodat.");
        }
        [HttpPut]
        [Route("AzurirajTelefon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajTelefon([FromBody] TelefonRoditeljaPregled telefon)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateTelefonRoditeljaAsync(telefon);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Telefon roditelja je uspešno ažuriran.");
        }
        [HttpDelete]
        [Route("ObrisiTelefon/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiTelefon(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteTelefonRoditeljaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Telefon roditelja je uspešno obrisan.");
        }
        [HttpGet]
        [Route("PreuzmiTelefoneZaDete/{deteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PreuzmiTelefoneZaDete(int deteId)
        {
            (bool isError, var telefoni, var error) = await DataProvider.GetTelefoniRoditeljaZaDeteAsync(deteId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(telefoni);
        }
    }
}