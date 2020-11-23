using System.Collections.Generic;
using Block;
using Entity;
using Entity.Friendly;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World {
    public class WorldManager : MonoBehaviour
    {
        public int seed = -1;
        public float perlinThreshold = 0.5f;
        public Vector2Int MaxThreshold;

        public GameObject landObject;
        public GameObject entityObject;
        public EntityManager EntityManager;

        private Dictionary<Vector2Int, BlockBase> _blocks =
            new Dictionary<Vector2Int, BlockBase>();

        public void GenerateWorld()
        {
            EntityManager = new EntityManager(this, entityObject);
            var entity = EntityManager.SpawnEntity<EntityRabbit>(30, 2, 20);

            if (seed != -1)
                Random.InitState(seed);

            for (float i = 0; i < MaxThreshold.x; i++)
            {
                for (float j = 0; j < MaxThreshold.y; j++)
                {
                    if (!IsWater((int) i, (int) j)) {
                        Instantiate(landObject, new Vector3(i * Width, 0, j * Height), Quaternion.identity);
                    }
                }
            }
        
            entity.PathPlanner.MoveTo(new Vector2Int(0, 0));
        }

        public float Width => landObject.transform.lossyScale.x;
        public float Height => landObject.transform.lossyScale.y;

        public bool IsWater(int x, int y) {
            return Mathf.PerlinNoise((float) x / 10, (float) y / 10) <= perlinThreshold;
        }
    
        public BlockBase GetBlock(int x, int y) {
            var contains = _blocks.TryGetValue(new Vector2Int(x, y), out var block);
            return contains ? block : null;
        }

        public bool IsAir(int x, int y) {
            if (x < 0 || y < 0)
                return false;

            if (x > MaxThreshold.x || y > MaxThreshold.y)
                return false;
        
            if (IsWater(x, y))
                return false;
        
            var location = new Vector2Int(x, y);
        
            if (_blocks.ContainsKey(location))
                return false;

            return true;
        }
    
        public bool SetBlock(BlockBase block) {
            var x = block.X;
            var y = block.Y;

            if (!IsAir(x, y))
                return false;

            var location = new Vector2Int(x, y);
            _blocks.Add(location, block);
            return true;
        }

        public void Start()
        {
            GenerateWorld();
        }
    }
}
