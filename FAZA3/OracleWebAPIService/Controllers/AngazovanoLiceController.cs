using static Deciji_Letnji_Program.DTOs;
using static Deciji_Letnji_Program.DataProvider;
using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AngazovanoLiceController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSvaAngazovanaLica")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiSvaAngazovanaLica()
        {
            (bool isError, List<AngazovanoLicePregled>? lica, var error) =
                await DataProvider.GetAllAngazovanaLicaAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(lica);
        }

        [HttpGet]
        [Route("VratiAngazovanoLice/{jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiAngazovanoLice(string jmbg)
        {
            (bool isError, AngazovanoLicePregled? lice, var error) = await DataProvider.GetAngazovanoLiceAsync(jmbg);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(lice);
        }

        [HttpPost]
        [Route("DodajAngazovanoLice")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DodajAngazovanoLice([FromBody] AngazovanoLicePregled lice)
        {
            var result = await DataProvider.AddAngazovanoLiceAsync(lice);

            if (!result.IsSuccess)
                return StatusCode(result.Error?.StatusCode ?? 400, result.Error?.Message);

            return StatusCode(201, "Angažovano lice je uspešno sačuvano.");
        }

        [HttpPut]
        [Route("AzurirajAngazovanoLice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajAngazovanoLice([FromBody] AngazovanoLicePregled lice)
        {
            var result = await DataProvider.UpdateAngazovanoLiceAsync(lice);

            if (!result.IsSuccess)
                return StatusCode(result.Error?.StatusCode ?? 400, result.Error?.Message);

            return Ok("Angažovano lice je uspešno ažurirano.");
        }

        [HttpDelete]
        [Route("ObrisiAngazovanoLice/{jmbg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiAngazovanoLice(string jmbg)
        {
            var result = await DataProvider.DeleteAngazovanoLiceAsync(jmbg);

            if (!result.IsSuccess)
                return StatusCode(result.Error?.StatusCode ?? 400, result.Error?.Message);

            return Ok($"Angažovano lice sa JMBG-om {jmbg} je uspešno obrisano.");
        }
    }
}
