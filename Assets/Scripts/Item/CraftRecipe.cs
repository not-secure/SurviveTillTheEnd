using System.Collections.Generic;

namespace Item {
    public enum RecipeType {
        None,
        Table
    };
    
    public readonly struct CraftRecipe {
        public CraftRecipe(List<ItemBase> recipe, ItemBase result, RecipeType type) {
            Recipe = recipe;
            Result = result;
            Type = type;
        }

        public List<ItemBase> Recipe { get; }
        public ItemBase Result { get; }
        public RecipeType Type { get; }
    }
}