using System;
using System.Collections.Generic;
using AutoMapper;
using CitiInfo.API.Entities;
using CitiInfo.API.Models;
using CitiInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CitiInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterstController : Controller
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<PointsOfInterstController> _logger;

        public PointsOfInterstController(ILogger<PointsOfInterstController> logger, IEmailService emailService,
            CityInfoContext cityInfoContext, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _emailService = emailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            if (_cityInfoRepository.DoesCityExist(cityId))
                return Ok(Mapper.Map<IEnumerable<PointOfInterestDto>>(
                    _cityInfoRepository.GetPointsOfInterestForCity(cityId)));
            _logger.LogWarning($"The city with id={cityId} was not found.");
            return NotFound();
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointsOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            if (_cityInfoRepository.DoesCityExist(cityId))
            {
                var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
                if (pointOfInterest != null)
                    return Ok(Mapper.Map<PointOfInterestDto>(pointOfInterest));
                _logger.LogWarning($"The point of interest with id={id} was not found.");
                return NotFound();
            }
            _logger.LogWarning($"The city with id={cityId} was not found.");
            return NotFound();
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                _logger.LogWarning($"The city with id={cityId} was not found.");
                return NotFound();
            }
            var newPointOfInterst = Mapper.Map<PointsOfInterest>(pointOfInterest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, newPointOfInterst);
            if (!_cityInfoRepository.Save())
                return StatusCode(500, "Error while trying to save new point of interest");
            return CreatedAtRoute("GetPointsOfInterest", new {cityId = newPointOfInterst.Id, id = newPointOfInterst.Id},
                Mapper.Map<PointOfInterestDto>(newPointOfInterst));
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                _logger.LogWarning($"The city with id={cityId} was not found.");
                return NotFound();
            }
            var existingPointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (existingPointOfInterest == null)
                return NotFound();
            Mapper.Map(pointOfInterest, existingPointOfInterest);
            if (!_cityInfoRepository.Save())
                return StatusCode(500, "Error while trying to save updated point of interest");
            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> pointOfInterestPatchDocument)
        {
            if (pointOfInterestPatchDocument == null)
                return BadRequest();
            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                _logger.LogWarning($"The city with id={cityId} was not found.");
                return NotFound();
            }
            var existingPointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (existingPointOfInterest == null)
                return NotFound();
            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(existingPointOfInterest);
            pointOfInterestPatchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
                ModelState.AddModelError("Description", "Name and Description cannot be same.");
            TryValidateModel(pointOfInterestToPatch);
            Mapper.Map(pointOfInterestToPatch, existingPointOfInterest);
            if (!_cityInfoRepository.Save())
                return StatusCode(500, "Error while trying to save updated point of interest");
            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.DoesCityExist(cityId))
            {
                _logger.LogWarning($"The city with id={cityId} was not found.");
                return NotFound();
            }
            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
                return NotFound();
            _cityInfoRepository.DeletePointOfInterest(pointOfInterest);
            if (!_cityInfoRepository.Save())
                return StatusCode(500, "Error while trying to delete point of interest");
            _emailService.Send("Removal of resource",
                $"Point of interest with id={id} for city with id={cityId} has been removed");
            return NoContent();
        }

        [HttpGet("exception")]
        public IActionResult ThrowAnException()
        {
            try
            {
                throw new Exception("An excption");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error happened while to trying to service your request.", ex);
                _logger.LogWarning("An error happened while to trying to service your request.", ex);
            }
            return StatusCode(500, "Sorry, we could not process your request.");
        }
    }
}