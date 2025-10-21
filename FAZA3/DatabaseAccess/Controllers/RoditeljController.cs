using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoditeljController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSveRoditelje")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiSveRoditelje()
        {
            (bool isError, List<RoditeljPregled>? roditelji, var error) =
                await DataProvider.GetAllRoditeljiAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(roditelji);
        }

        [HttpGet]
        [Route("VratiRoditelja/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiRoditelja(int id)
        {
            (bool isError, RoditeljPregled? roditelj, var error) = await DataProvider.GetRoditeljAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(roditelj);
        }

        [HttpPost]
        [Route("DodajRoditelja")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DodajRoditelja([FromBody] RoditeljPregled roditelj)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddRoditeljAsync(roditelj);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Roditelj je uspešno dodat.");
        }

        [HttpPut]
        [Route("AzurirajRoditelja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajRoditelja([FromBody] RoditeljPregled roditelj)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateRoditeljAsync(roditelj);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Roditelj je uspešno ažuriran.");
        }

        [HttpDelete]
        [Route("ObrisiRoditelja/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiRoditelja(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteRoditeljAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Roditelj je uspešno obrisan.");
        }
    }
}