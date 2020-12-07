using System.Collections.Generic;
using System.Linq;
using Item.Items;
using Player;

namespace Item {
    public class CraftManager {
        private PlayerController _player;
        private List<CraftRecipe> _recipes = new List<CraftRecipe>();
        
        public CraftManager(PlayerController player) {
            _player = player;
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemPlank(10) },
                new ItemPlank(22), 
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemDiamondWand(1), 
                RecipeType.None
            ));
            
        }

        public void AddRecipe(CraftRecipe recipe) {
            _recipes.Add(recipe);
        }
        
        public CraftRecipe[] GetRecipes() {
            return _recipes
                .Where(recipe => recipe.Type == RecipeType.None)
                .ToArray();
        }

        public void Craft(CraftRecipe recipe) {
            if (!Available(recipe))
                return;
            
            foreach (var itemBase in recipe.Recipe) {
                _player.Inventory.RemoveItem(itemBase);
            }

            _player.Inventory.AddItem((ItemBase) recipe.Result.Clone());
        }

        public bool Available(CraftRecipe recipe) {
            return recipe.Recipe
                .All(req => _player.Inventory.GetCount(req.ItemId) >= req.Count);
        }

        public event AvailRecipeChangeEventHandler OnAvailRecipeChange;

        public delegate void AvailRecipeChangeEventHandler(CraftManager sender);
    }
}