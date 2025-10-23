using static Deciji_Letnji_Program.DTOs;
using static Deciji_Letnji_Program.DataProvider;
using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObrokController : ControllerBase
    {
        [HttpGet]
        [Route("VratiObrokeDeteta/{deteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiObrokeDeteta(int deteId)
        {
            (bool isError, List<ObrokPregled>? obroci, var error) = await DataProvider.GetObrociZaDeteAsync(deteId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(obroci);
        }
        [HttpGet]
        [Route("VratiSveObroke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiSveObroke()
        {
            (bool isError, List<ObrokPregled>? obroci, var error) = await DataProvider.GetAllObrociAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(obroci);
        }
        [HttpGet]
        [Route("VratiObrok/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiObrok(int id)
        {
            var (isError, obrok, error) = await DataProvider.GetObrokAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(obrok);
        }
        [HttpPost]
        [Route("DodajObrok")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DodajObrok([FromBody] ObrokPregled obrok)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddObrokAsync(obrok);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Obrok je uspešno dodat.");
        }
        [HttpPut]
        [Route("AzurirajObrok")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AzurirajObrok([FromBody] ObrokPregled obrok)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateObrokAsync(obrok);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Obrok je uspešno ažuriran.");
        }

        [HttpDelete]
        [Route("ObrisiObrok/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiObrok(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteObrokAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok($"Obrok sa ID-em {id} je uspešno obrisan.");
        }
        
        [HttpPost]
        [Route("DodeliObrokDetetu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DodeliObrokDetetu([FromQuery] int deteId, [FromQuery] int obrokId)
        {
            (bool isError, bool ok, var error) = await DataProvider.DodeliObrokDetetuAsync(deteId, obrokId);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok($"Obrok sa ID-em {obrokId} je uspešno dodeljen detetu sa ID-em {deteId}.");
        }

    }
}