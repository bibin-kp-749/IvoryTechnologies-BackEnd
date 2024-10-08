﻿using MachinTest_Backend.Model;
using MachinTest_Backend.Model.DTO;
using MachinTest_Backend.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace MachinTest_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        //Contructor function for Implementing the dependency Injection
        public LocationController(ILocationService locationService)
        {
            this._locationService = locationService;
        }

        //Action Methods for Getting All Locations

        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var data = await _locationService.GetLoactions();
                //Checking the data is exist
                if (data.Count() < 1)
                    return NotFound();
                return Ok(data);
            }catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //Action Methods for Add new Locations

        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationDto details)
        {
            try
            {
                if (details == null) //Validating the input data
                    return BadRequest();
                var data =await _locationService.AddLoaction(details);
                //Checking is Added or not
                if (!data)
                    return StatusCode(500, "Failed to add location to the database.");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        //Action Methods for Update Location

        [HttpPut]
        public async Task<IActionResult> UpdateLocation(LocationDto details)
        {
            try
            {
                if (details == null) //Validating the input data
                    return BadRequest();
                var data = await _locationService.UpdateLoaction(details);
                //Checking is Updated or not
                if (!data)
                    return StatusCode(500, "Failed to update location to the database.");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        //Action Methods to delete an exising location using name

        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(string Phone)
        {
            try
            {
                if (Phone.IsNullOrEmpty()) //Check the input data
                    return BadRequest();
                var data = await _locationService.DeletLoaction(Phone);
                //Validating dleted or not
                if (!data)
                    return StatusCode(500, "Failed to delete location from the database.");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
