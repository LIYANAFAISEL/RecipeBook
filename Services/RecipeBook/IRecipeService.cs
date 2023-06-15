using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Services.Recipe
{
    public interface IRecipeService
    {
        Task<ServiceResponse<List<GetRecipeResponseDto>>> GetAllRecipes();
        Task<ServiceResponse<GetRecipeResponseDto>> GetRecipeById(int id);
        Task<ServiceResponse<List<GetRecipeResponseDto>>> AddRecipe(AddRecipeRequestDto newRecipe);
        Task<ServiceResponse<GetRecipeResponseDto>> UpdateRecipe(UpdateRecipeDto updatedRecipe);
        Task<ServiceResponse<List<GetRecipeResponseDto>>> DeleteRecipe(int id);

    }
}
