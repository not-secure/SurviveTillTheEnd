using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Item {
    public class DroprateTable {
        public SortedList<float, ItemBase> Table = new SortedList<float, ItemBase>();

        public void AddDrop(ItemBase item, float rate) {
            Table.Add(Max() + rate, item);
        }

        public ItemBase GetDrop() {
            var random = Random.Range(0, Max());

            ItemBase output = null;
            Table
                .Aggregate(0f, (prev, curr) => {
                    if (prev < random && random < curr.Key)
                        output = curr.Value;

                    return curr.Key;
                });

            return (ItemBase) output?.Clone();
        }

        public float Max() {
            return Table.Count == 0 ? 0f : Table.Keys.Max();
        }
    }
}