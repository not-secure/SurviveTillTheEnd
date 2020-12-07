using System;
using Block;
using UnityEngine;

namespace World {
    public class ChunkController: MonoBehaviour {
        [NonSerialized] public BlockController[] AvailableBlock;
        
        private void OnEnable() {
            AvailableBlock = GetComponentsInChildren<BlockController>();
        }
    }
}