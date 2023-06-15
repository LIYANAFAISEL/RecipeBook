using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Dtos.Recipe
{
    public class UpdateRecipeDto
    {
        public int RecipeId { get; set; } 
        public string RecipeName { get; set;} = "";
        public string RecipeIngred { get; set; } = "";
        public string RecipeInstruction { get; set; } = "";
        public string RecipeAuthor { get; set; } = "";
        public string ImageData { get; set; } = "";    
        public bool isDeleted { get; set; } = false;
    }
}