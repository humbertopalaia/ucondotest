using AutoMapper;
using ChartAccountAPI.Models.ChartAccount;
using ChartAccountBusiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ChartAccountAPI.Controllers
{
    [Route("api/[controller]")]

    public class ChartAccountController : Controller
    {
        private readonly IChartAccountBusiness _chartAccountBusiness;
        private readonly IMapper _mapper;
        public ChartAccountController(IChartAccountBusiness chartAccountBusiness, IMapper mapper) {
            _chartAccountBusiness = chartAccountBusiness;
            _mapper = mapper;


            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
        }

        [HttpPost("List")]
        [AllowAnonymous]

        public IActionResult List()
        {

            var entity = _chartAccountBusiness.GetAll();

            var model = _mapper.Map<List<ChartAccountDomain.ChartAccount>,List<ChartAccountModel>>(entity);

            return Json(model);
        }
    }
}
