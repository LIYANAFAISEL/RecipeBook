global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Recipes.Services.Recipe
{
    
    public class RecipeService : IRecipeService
    {
        private static List<RecipeBook> recipeList = new List<RecipeBook> {
            new RecipeBook(),
            new RecipeBook {RecipeId = 1, RecipeName = "Chicken Biriyani"}
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RecipeService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetRecipeResponseDto>>> AddRecipe(AddRecipeRequestDto newRecipe)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeResponseDto>>();
            var rList = _mapper.Map<RecipeBook>(newRecipe);
            rList.RecipeName = newRecipe.RecipeName;
            rList.RecipeIngred = newRecipe.RecipeIngred;
            rList.RecipeInstruction = newRecipe.RecipeInstruction;
            rList.RecipeAuthor = newRecipe.RecipeAuthor;
            rList.ImageData = newRecipe.ImageData;
            // rList.RecipeId = recipeList.Max(c => c.RecipeId) + 1;
            _context.RecipeBooks.Add(rList);
            await _context.SaveChangesAsync();
            serviceResponse.Data = 
                await _context.RecipeBooks.Select(c => _mapper.Map<GetRecipeResponseDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeResponseDto>>> GetAllRecipes()
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeResponseDto>>();
            var dbRecipe = await _context.RecipeBooks.ToListAsync();
            serviceResponse.Data = dbRecipe.Select(c => _mapper.Map<GetRecipeResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRecipeResponseDto>> GetRecipeById(int id)
        {
            var serviceResponse = new ServiceResponse<GetRecipeResponseDto>();
            var dbRecipe = await _context.RecipeBooks.FirstOrDefaultAsync(c => c.RecipeId == id);
            serviceResponse.Data = _mapper.Map<GetRecipeResponseDto>(dbRecipe);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRecipeResponseDto>> UpdateRecipe(UpdateRecipeDto updatedRecipe)
        {
            var serviceResponse = new ServiceResponse<GetRecipeResponseDto>();
            try {
                var rList = await _context.RecipeBooks.FirstOrDefaultAsync(c => c.RecipeId == updatedRecipe.RecipeId);
                if(rList is null)
                    throw new Exception($"Recipe with Id '{updatedRecipe.RecipeId}' not found.");
                // _mapper.Map(updatedRecipe,rList);
                rList.RecipeName = updatedRecipe.RecipeName;
                rList.RecipeIngred = updatedRecipe.RecipeIngred;
                rList.RecipeInstruction = updatedRecipe.RecipeInstruction;
                rList.RecipeAuthor = updatedRecipe.RecipeAuthor;
                rList.ImageData = updatedRecipe.ImageData;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetRecipeResponseDto>(rList);
            }
            catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRecipeResponseDto>>> DeleteRecipe(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeResponseDto>>();
            try {
                var rList = await _context.RecipeBooks.FirstOrDefaultAsync(c => c.RecipeId == id);
                if(rList is null)
                    throw new Exception($"Recipe with Id '{id}' not found.");
                _context.RecipeBooks.Remove(rList);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.RecipeBooks.Select(c => _mapper.Map<GetRecipeResponseDto>(c)).ToListAsync();
            }
            catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}