﻿using AutoMapper;
using ChartAccountAPI.Models.ChartAccount;
using ChartAccountBusiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ChartAccountAPI.Models;

namespace ChartAccountAPI.Controllers
{
    [Route("api/[controller]")]

    public class ChartAccountController : Controller
    {
        private readonly IChartAccountBusiness _chartAccountBusiness;
        private readonly IMapper _mapper;
        public ChartAccountController(IChartAccountBusiness chartAccountBusiness, IMapper mapper)
        {
            _chartAccountBusiness = chartAccountBusiness;
            _mapper = mapper;


            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
        }




        [HttpGet("List")]
        [AllowAnonymous]
        public IActionResult List(string name)
        {

            var entity = _chartAccountBusiness.Filter(name);

            var model = _mapper.Map<List<ChartAccountDomain.ChartAccount>, List<ChartAccountListModel>>(entity);


            return Json(model);
        }

        [HttpPut("Insert")]
        [AllowAnonymous]
        public IActionResult Insert([FromBody] ChartAccountInsertModel model)
        {
            var resultModel = new OperationResultModel();

            try
            {
                var entity = _mapper.Map<ChartAccountInsertModel, ChartAccountDomain.ChartAccount>(model);

                resultModel = _mapper.Map<OperationResultModel>(_chartAccountBusiness.Insert(entity));
            }
            catch (Exception ex)
            {
                resultModel.Success = false;
                resultModel.Errors.Add(ex.Message);
            }

            if (resultModel.Success)
                return Json(resultModel);
            else
                return BadRequest(resultModel);
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetNextCode(string currentCode)
        {
            return Json(new { NextCode = _chartAccountBusiness.GetNextCode(currentCode) } );
        }


        [HttpDelete("Delete")]
        [AllowAnonymous]
        public IActionResult Delete(string code)
        {
            var resultModel = new OperationResultModel();

            try
            {
                resultModel = _mapper.Map<OperationResultModel>(_chartAccountBusiness.Delete(code));
            }
            catch (Exception ex)
            {
                resultModel.Success = false;
                resultModel.Errors.Add(ex.Message);
            }

            if (resultModel.Success)
                return Json(resultModel);
            else
                return BadRequest(resultModel);
        }

    }
}
