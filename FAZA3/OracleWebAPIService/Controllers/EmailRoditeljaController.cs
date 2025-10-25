using static Deciji_Letnji_Program.DTOs;
using static Deciji_Letnji_Program.DataProvider;
using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailRoditeljaController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveEmailoveRoditelja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiSveEmailoveRoditelja()
        {
            (bool isError, List<EmailRoditeljaPregled>? emailovi, var error) =
                await DataProvider.GetAllEmailoviRoditeljaAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(emailovi);
        }
        [HttpGet]
        [Route("VratiEmailRoditelja/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiEmailRoditelja(int id)
        {
            (bool isError, EmailRoditeljaPregled? email, var error) =
                await DataProvider.GetEmailRoditeljaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(email);
        }
        [HttpPost]
        [Route("DodajEmailRoditelja/{deteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DodajEmailRoditelja(int deteId, [FromBody] EmailRoditeljaPregled email)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddEmailRoditeljaAsync(email, deteId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Email roditelja je uspešno dodat.");
        }
        [HttpPut]
        [Route("AzurirajEmailRoditelja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajEmailRoditelja([FromBody] EmailRoditeljaPregled email)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateEmailRoditeljaAsync(email);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Email roditelja je uspešno ažuriran.");
        }
        [HttpDelete]
        [Route("ObrisiEmailRoditelja/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiEmailRoditelja(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteEmailRoditeljaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Email roditelja je uspešno obrisan.");
        }
        [HttpGet]
        [Route("VratiEmailoveRoditeljaZaDete/{deteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiEmailoveRoditeljaZaDete(int deteId)
        {
            (bool isError, List<EmailRoditeljaPregled>? emailovi, var error) =
                await DataProvider.GetEmailoviRoditeljaZaDeteAsync(deteId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(emailovi);
        }

    }
}
