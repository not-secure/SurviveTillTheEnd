using Item;
using UI.Item;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory {
    public class UICraft : MonoBehaviour {
        public GameObject item;
    
        public void InitializeRecipe(CraftRecipe craft, CraftManager manager) {
            if (!manager.Available(craft)) {
                var img = GetComponent<Image>();
                var color = img.color;
                color.a = 0.3f;

                img.color = color;
            }
            
            var x = -60;
            foreach (var recipeItem in craft.Recipe) {
                AddItem(recipeItem, x);
                x += 40;
            }
            
            AddItem(craft.Result, 60);
        }

        private void AddItem(ItemBase addingItem, int x) {
            var itemObject = Instantiate(item, transform);
            itemObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            
            var pos = itemObject.transform.localPosition;
            pos.x = x;
            pos.y = 0;
            itemObject.transform.localPosition = pos;
            
            var itemController = itemObject.GetComponent<UIItem>();
            itemController.SetItem(addingItem);
        }
    }
}
