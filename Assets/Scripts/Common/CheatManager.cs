using System.Collections.Generic;
using Item;
using Item.Items;
using Player;
using UnityEngine;

namespace Common {
    public class CheatManager: MonoBehaviour {
        public void Update() {
            if (Input.GetKey(KeyCode.PageDown)) {
                AddAllRecipes();
            }
        }

        private bool _recipeAdded = false;
        public void AddAllRecipes() {
            if (_recipeAdded) return;
            _recipeAdded = true;
            
            var c = Object.FindObjectOfType<PlayerController>().Craft;
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemCoin(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemSilverKey(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemGear(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemHammer(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemLetter(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemMagicalPowder(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemSilver(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemRuby(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemEmerald(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemDiamond(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemStaminaAde(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemCider(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemDkPepper(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemCola(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemApple(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemWoodenSword(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemSilverSword1(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemSilverSword2(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemSilverSword3(1),
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() {   },
                new ItemDiamondWand(1), 
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() {   },
                new ItemRubyWand(1), 
                RecipeType.None
            ));
            
            c.AddRecipe(new CraftRecipe(
                new List<ItemBase>() { },
                new ItemEmeraldWand(1),
                RecipeType.None
            ));
        }
    }
}