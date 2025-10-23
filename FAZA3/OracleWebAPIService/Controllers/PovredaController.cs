using DatabaseAccess;
using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PovredaController : ControllerBase
    {
        [HttpGet]
        [Route("SvePovrede")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SvePovrede()
        {
            (bool isError, List<PovredaPregled>? povrede, var error) = await DataProvider.GetAllPovredeAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(povrede);
        }

        [HttpGet]
        [Route("Povreda/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Povreda(int id)
        {
            var (isError, povreda, error) = await DataProvider.GetPovredaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(povreda);
        }

        [HttpPost]
        [Route("DodajPovredu")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DodajPovredu([FromBody] PovredaPregled povreda)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddPovredaAsync(povreda);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Povreda je uspešno dodata.");
        }

        [HttpPut]
        [Route("AzurirajPovredu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajPovredu([FromBody] PovredaPregled povreda)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdatePovredaAsync(povreda);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Povreda je uspešno ažurirana.");
        }

        [HttpDelete]
        [Route("ObrisiPovredu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiPovredu(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeletePovredaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Povreda je uspešno obrisana.");
        }
    }
}