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
                new List<ItemBase>() { new ItemGear(2), new ItemMagicalPowder(1) },
                new ItemSilver(1), 
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemSilverKey(3), new ItemMagicalPowder(1) },
                new ItemSilver(1),
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemSilver(1) },
                new ItemCoin(1),
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemPlank(5), new ItemSilver(3) },
                new ItemHammer(1), 
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemPlank(7) },
                new ItemWoodenSword(1), 
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemPlank(5), new ItemSilver(5) },
                new ItemSilverSword1(1),  
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemSilverSword1(1), new ItemSilver(5) },
                new ItemSilverSword2(1),  
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemSilverSword2(1), new ItemMagicalPowder(7) },
                new ItemSilverSword3(1),  
                RecipeType.None
            ));

            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemPlank(10), new ItemMagicalPowder(20) },
                new ItemWandBase(1), 
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemDiamond(4), new ItemWandBase(1) },
                new ItemDiamondWand(1), 
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() { new ItemRuby(3), new ItemWandBase(1) },
                new ItemRubyWand(1), 
                RecipeType.None
            ));
            
            AddRecipe(new CraftRecipe(
                new List<ItemBase>() {  new ItemEmerald(2), new ItemPlank(4) },
                new ItemEmeraldWand(1), 
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