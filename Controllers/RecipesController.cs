using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recipes.Services.Recipe;

namespace Recipes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
        
    public class RecipesController : ControllerBase
    {
        private static List<RecipeBook> recipeList = new List<RecipeBook> {
            new RecipeBook()
        };

        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeResponseDto>>>> GetAllRecipes()
        {
            return Ok(await _recipeService.GetAllRecipes());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRecipeResponseDto>>> GetRecipeById(int id)
        {
            return Ok(await _recipeService.GetRecipeById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetRecipeResponseDto>>>> AddRecipe(AddRecipeRequestDto newRecipe)
        {
            return Ok(await _recipeService.AddRecipe(newRecipe));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<UpdateRecipeDto>>> UpdateRecipe(UpdateRecipeDto updatedRecipe)
        {
            var response = await _recipeService.UpdateRecipe(updatedRecipe);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRecipeResponseDto>>> DeleteRecipe(int id)
        {
            var response = await _recipeService.DeleteRecipe(id);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}