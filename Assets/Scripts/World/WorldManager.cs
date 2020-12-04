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
            Vector2 distanceVector = playerPosition - chunk.position;
            float distance = Mathf.Sqrt(Mathf.Pow(distanceVector.x, 2) + Mathf.Pow(distanceVector.y, 2));
            if (distance > maxDistanceFromPlayer)
                return;

            if (chunkMap.ContainsKey(chunk.position))
                return;

            chunkMap[chunk.position] = chunk;

            Vector2Int[] dir = new Vector2Int[4] { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(-1, 0), new Vector2Int(1, 0) };
            for (int i = 0; i < 4; i++)
                GenerateNeighborChunk(new Chunk(chunk.position + dir[i], chunk.type));
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
