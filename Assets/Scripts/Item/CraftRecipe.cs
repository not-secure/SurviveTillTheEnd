using System.Collections.Generic;

namespace Item {
    public readonly struct CraftItem {
        public CraftItem(int itemId, int count) {
            ItemId = itemId;
            Count = count;
        }
        
        public int ItemId { get; }
        public int Count { get; }
    }
    
    public readonly struct CraftRecipe {
        public CraftRecipe(List<CraftItem> recipe, CraftItem result) {
            Recipe = recipe;
            Result = result;
        }

        public List<CraftItem> Recipe { get; }
        public CraftItem Result { get; }
    }
}