using Block;
using UnityEngine;

namespace World {
    [System.Serializable]
    public class Chunk {
        public Vector2Int Position;
        public int type;
        public GameObject gameObject;

        private int _variant;
        private ChunkController _chunkController;
    
        public Chunk(Vector2Int position, int type) {
            this.Position = position;
            this.type = type;

            _variant = Random.Range(1, 7);
        }

        public void Generate() {
            if (gameObject) return;
        
            gameObject = Object.Instantiate(
                Resources.Load<GameObject>("Chunks/" + type.ToString() + "_type/" + _variant + "_num")
            );

            _chunkController = gameObject.GetComponent<ChunkController>();
            gameObject.transform.position = new Vector3(
                Position.x * WorldManager.ChunkSize.x, 
                0, 
                Position.y * WorldManager.ChunkSize.y
            );
        }

        public void Unload() {
            _chunkController = null;
        
            Object.Destroy(gameObject);
            gameObject = null;
        }

        public BlockController[] ActiveBlocks() {
            if (!_chunkController)
                return new BlockController[] {};
        
            return _chunkController.AvailableBlock;
        }
    }
}