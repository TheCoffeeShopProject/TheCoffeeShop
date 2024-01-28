﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheCoffeeCatBusinessObject;
using TheCoffeeCatBusinessObject.DTO;
using TheCoffeeCatBusinessObject.ViewModels;
using TheCoffeeCatService.IServices;
using TheCoffeeCatService.Services;

namespace TheCoffeeCatStore.Controllers.NewFolder
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinkServices _services;
        private readonly IMapper _mapper;
        public DrinksController(IDrinkServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<Drink> GetDrinks()
        {
            // Get Data from database - domain model - object
            var drink = _services.GetDrinks();
            //Map domain model to DTO
            var drinkDTO = new List<DrinkDTO>();
            foreach (var drinks in drink)
            {
                drinkDTO.Add(new DrinkDTO()
                {
                    DrinkName = drinks.DrinkName,
                    Image = drinks.Image,
                    Status = drinks.Status,
                    UnitPrice = drinks.UnitPrice,
                });
            }
            // Return DTO
            return Ok(drinkDTO);
        }
        //[HttpGet]
        //[Route("{id:Guid}")]
        //public ActionResult<Drink> GetDrinkById(Guid id)
        //{

        //    var drink = _services.GetDrinkById(id);
        //    if(drink == null)
        //    {
        //        return NotFound();
        //    }
        //    var drinkDTO = new DrinkDTO
        //    {
        //        DrinkName = drink.DrinkName,
        //        Image = drink.Image,
        //        Status = drink.Status,
        //        UnitPrice = drink.UnitPrice,
        //    };
        //    return Ok(drinkDTO);
        // }
        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<Drink> GetDrinkById(Guid id)
        {

            try
            {
                var drinks = _services.GetDrinkById(id);
                var response = _mapper.Map<DrinkDTO>(drinks);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search")]
        public ActionResult<Drink> GetDrinkByName(string searchvalue)
        {

            try
            {
                var drinks = _services.GetDrinkByName(searchvalue);
                var response = _mapper.Map<DrinkDTO>(drinks);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public ActionResult AddDrink(DrinkDTO drinkDTO)
        {
            try
            {
                var respose = _mapper.Map<Drink>(drinkDTO);
                _services.AddDrink(respose);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut]
        public ActionResult UpdateDrink(Guid id, DrinkDTO drinkDTO)
        {
            try
            {
                var response = _services.GetDrinkById(id);

                if (response == null)
                {
                    return NotFound();
                }
                var find = _mapper.Map<Drink>(drinkDTO);
                _services.UpdateDrink(find);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
