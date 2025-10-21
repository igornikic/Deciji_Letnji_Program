using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrijavaController : ControllerBase
    {
        [HttpGet]
        [Route("RoditeljiZaAktivnost/{aktivnostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RoditeljiZaAktivnost(int aktivnostId)
        {
            (bool isError, List<RoditeljPregled>? roditelji, var error) =
                await DataProvider.GetRoditeljiZaAktivnostAsync(aktivnostId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(roditelji);
        }

        [HttpGet]
        [Route("DecaZaRoditeljaIAktivnost/{roditeljId}/{aktivnostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DecaZaRoditeljaIAktivnost(int roditeljId, int aktivnostId)
        {
            (bool isError, List<DetePregled>? deca, var error) =
                await DataProvider.GetDecaZaRoditeljaIAktivnostAsync(roditeljId, aktivnostId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(deca);
        }

        [HttpGet]
        [Route("MozePrijava/{aktivnostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MozePrijava(int aktivnostId)
        {
            (bool isError, bool moze, var error) = await DataProvider.MozePrijavaAsync(aktivnostId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(moze);
        }

        [HttpGet]
        [Route("SvePrijave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SvePrijave()
        {
            (bool isError, List<PrijavaPregled>? prijave, var error) = await DataProvider.GetAllPrijaveAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(prijave);
        }

        [HttpGet]
        [Route("Prijava/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Prijava(int id)
        {
            (bool isError, PrijavaPregled? prijava, var error) = await DataProvider.GetPrijavaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(prijava);
        }

        [HttpPost]
        [Route("DodajPrijavu")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DodajPrijavu(int aktivnostId, int roditeljId, int deteId, DateTime datum)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddPrijavaAsync(aktivnostId, roditeljId, deteId, datum);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Prijava je uspešno dodata.");
        }

        [HttpPut]
        [Route("AzurirajPrijavu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AzurirajPrijavu([FromBody] PrijavaPregled prijava)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdatePrijavaAsync(prijava);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Prijava je uspešno ažurirana.");
        }

        [HttpDelete]
        [Route("ObrisiPrijavu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiPrijavu(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeletePrijavaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Prijava je uspešno obrisana.");
        }

        [HttpGet]
        [Route("BrojPrijava/{aktivnostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BrojPrijava(int aktivnostId)
        {
            (bool isError, int broj, var error) = await DataProvider.GetBrojPrijavaZaAktivnostAsync(aktivnostId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(broj);
        }
    }
}