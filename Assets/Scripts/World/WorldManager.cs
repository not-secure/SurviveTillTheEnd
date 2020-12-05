using Entity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class WorldManager : MonoBehaviour
    {
        #region dummy
        // Things that used in other classes
        // Need to be removed later
        public int Width = 1;
        public int Height = 1;
        public EntityManager EntityManager;
        public bool IsAir(int x, int y) { return (x + y) * 0 == 0; }
        #endregion

        public int seed = -1;
        public Chunk root;
        public Dictionary<Vector2Int, Chunk> chunkMap;

        public Vector2Int playerPosition;
        public float maxDistanceFromPlayer;

        public void GenerateNeighborChunk(Chunk chunk)
        {
            var distanceVector = playerPosition - chunk.Position;
            var distance = distanceVector.magnitude;
            if (distance > maxDistanceFromPlayer)
                return;

            if (chunkMap.ContainsKey(chunk.Position))
                return;

            chunk.Generate();
            chunkMap[chunk.Position] = chunk;

            var dir = new Vector2Int[4] { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(-1, 0), new Vector2Int(1, 0) };
            for (var i = 0; i < 4; i++)
                GenerateNeighborChunk(new Chunk(chunk.Position + dir[i], chunk.type));
        }

        public void Start()
        {
            chunkMap = new Dictionary<Vector2Int, Chunk>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                root = new Chunk(new Vector2Int(0, 0), 1);
                GenerateNeighborChunk(root);
            }
        }
    }
}
