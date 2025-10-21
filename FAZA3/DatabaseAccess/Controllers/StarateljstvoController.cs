using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarateljstvoController : ControllerBase
    {
        [HttpPost]
        [Route("DodajStarateljstvo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DodajStarateljstvo(int deteId, int roditeljId)
        {
            (bool isError, bool ok, var error) = await DataProvider.DodajStarateljstvoAsync(deteId, roditeljId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Starateljstvo je uspešno dodato.");
        }

        [HttpGet]
        [Route("DostupnaDeca/{roditeljId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DostupnaDecaZaStarateljstvo(int roditeljId)
        {
            (bool isError, List<DetePregled>? deca, var error) =
                await DataProvider.GetDecaZaDodavanjeStarateljstvaAsync(roditeljId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(deca);
        }
    }
}