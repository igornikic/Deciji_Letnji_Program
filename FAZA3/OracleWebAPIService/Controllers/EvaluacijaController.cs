using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Deciji_Letnji_Program.DataProvider;
using static Deciji_Letnji_Program.DTOs;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EvaluacijaController : ControllerBase
    {
        [HttpGet]
        [Route("SveEvaluacije")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //=====================================================================
        //OVDE SAM PRIMETIO DA POL CUDNO VRACA U OBLIKU /U00000
        //=====================================================================
        public async Task<IActionResult> SveEvaluacije()
        {
            (bool isError, List<EvaluacijaPregled>? evaluacije, var error) = await DataProvider.GetAllEvaluacijeAsync();

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(evaluacije);
        }

        [HttpGet]
        [Route("Evaluacija/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Evaluacija(int id)
        {
            (bool isError, EvaluacijaPregled? evaluacija, var error) = await DataProvider.GetEvaluacijaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok(evaluacija);
        }

        [HttpPost]
        [Route("DodajEvaluaciju")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DodajEvaluaciju([FromBody] EvaluacijaPregled evaluacija)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddEvaluacijaAsync(evaluacija);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return StatusCode(201, "Evaluacija je uspešno dodata.");
        }

        [HttpPut]
        [Route("AzurirajEvaluaciju")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AzurirajEvaluaciju([FromBody] EvaluacijaPregled evaluacija)
        {
            (bool isError, bool ok, var error) = await DataProvider.UpdateEvaluacijaAsync(evaluacija);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Evaluacija je uspešno ažurirana.");
        }

        [HttpDelete]
        [Route("ObrisiEvaluaciju/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObrisiEvaluaciju(int id)
        {
            (bool isError, bool ok, var error) = await DataProvider.DeleteEvaluacijaAsync(id);

            if (isError)
                return StatusCode(error?.StatusCode ?? 400, error?.Message);

            return Ok("Evaluacija je uspešno obrisana.");
        }

    }
}
