using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UcestvujeController : ControllerBase
    {
        [HttpGet]
        [Route("VratiSvaUcesca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiSvaUcesca()
        {
            (bool isError, List<UcestvujePregled>? ucesca, var error) = await DataProvider.GetAllUcescaAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(ucesca);
        }
        [HttpGet]
        [Route("VratiUcesce/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VratiUcesce(int id)
        {
            (bool isError, UcestvujePregled? ucesce, var error) = await DataProvider.GetUcesceAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 500, error?.Message);

            return Ok(ucesce);
        }
        [HttpPut]
        [Route("IzmeniUcesce")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IzmeniUcesce([FromBody] UcestvujePregled ucesce)
        {
            var result = await DataProvider.UpdateUcesceAsync(ucesce);

            if (!result.IsSuccess)
                return StatusCode(result.Error?.StatusCode ?? 500, result.Error?.Message);

            return Ok(true);
        }
        [HttpDelete]
        [Route("ObrisiUcesce/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiUcesce(int id)
        {
            var result = await DataProvider.DeleteUcesceAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.Error?.StatusCode ?? 500, result.Error?.Message);

            return Ok(true);
        }
    }
}
