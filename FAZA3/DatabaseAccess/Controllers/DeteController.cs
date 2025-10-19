using static Deciji_Letnji_Program.DTOs;
using static Deciji_Letnji_Program.DataProvider;
using Deciji_Letnji_Program;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;

namespace OracleWebAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeteController : ControllerBase
    {
  
        [HttpPost]
        [Route("DodajDete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DodajDete([FromBody] DetePregled dete)
        {
            (bool isError, bool ok, var error) = await DataProvider.AddDeteAsync(dete);
            if (isError) return StatusCode(error?.StatusCode ?? 400, error?.Message);
            return StatusCode(201, "Dete je uspešno sačuvano.");
        } 
    }
}
