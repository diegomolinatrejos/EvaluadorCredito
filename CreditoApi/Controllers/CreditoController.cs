using CreditoLogic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using CreditoDto;
using Microsoft.AspNetCore.Cors;

namespace CreditoAPI.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class CreditoController : ControllerBase
    {
        [HttpPost]
        public Salida GetCredito(Entrada entrada)
        {
            CreditoAdmin creditoAdmin = new CreditoAdmin();
            return creditoAdmin.CalculoCredito(entrada);
        }
    }

}

