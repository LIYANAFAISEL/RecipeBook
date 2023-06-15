using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RecipeBook,GetRecipeResponseDto>();
            
            CreateMap<AddRecipeRequestDto,RecipeBook>();
        
            CreateMap<UpdateRecipeDto, RecipeBook>();
        }
    }
}