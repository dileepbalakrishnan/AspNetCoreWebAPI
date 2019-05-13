using System.Collections.Generic;
using AutoMapper;
using CitiInfo.API.Models;
using CitiInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitiInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var result =
                Mapper.Map<IEnumerable<CitiWithoutPointsOfInterestDto>>(_cityInfoRepository.GetCities());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfIneterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfIneterest);
            if (city == null)
                return NotFound();
            if (includePointsOfIneterest)
                return Ok(Mapper.Map<CitiDto>(city));
            return Ok(Mapper.Map<CitiWithoutPointsOfInterestDto>(city));
        }
    }
}