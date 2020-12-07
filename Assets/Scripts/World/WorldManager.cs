using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Player;
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
        public bool IsAir(int x, int y) { return (x + y) * 0 == 0; }
        #endregion

        public int seed = -1;
        public Chunk root;
        public float maxDistanceFromPlayer;
        public static Vector2Int ChunkSize = new Vector2Int(30, 30);

        private PlayerController _player;
        private Dictionary<Vector2Int, Chunk> _chunkMap;
        private readonly Vector2Int[] _directions = new Vector2Int[4] {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0)
        };
        
        public void GenerateNeighborChunks(Chunk chunk) {
            var distanceVector = PlayerPosition - chunk.Position;
            var distance = distanceVector.magnitude;
            if (distance > maxDistanceFromPlayer)
                return;

            if (chunk.gameObject) return;
            chunk.Generate();
            
            foreach (var neighborChunk in GetNeighborChunks(chunk)) {
                GenerateNeighborChunks(neighborChunk);
            }
        }

        public Chunk GetChunk(Vector2Int position, int chunkType) {
            if (_chunkMap.TryGetValue(position, out var resultChunk)) {
                return resultChunk;
            }

            var nextChunk = new Chunk(position, chunkType);
            _chunkMap[position] = nextChunk;
                
            return nextChunk;
        }
        
        public IEnumerable<Chunk> GetNeighborChunks(Chunk c) {
            return _directions.Select(dir => {
                var position = c.Position + dir;
                return GetChunk(position, c.type);
            });
        }

        public void Start() {
            _chunkMap = new Dictionary<Vector2Int, Chunk>(); 
            _player = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<PlayerController>();
            
            GenerateNeighborChunks(PlayerChunk);
        }

        private Vector2Int _lastPosition;
        public void Update() {
            var position = PlayerPosition;
            if (_lastPosition != position) {
                GenerateNeighborChunks(PlayerChunk);
                _lastPosition = position;
            }
        }
        
        public Vector2Int PlayerPosition {
            get {
                var p = _player.transform.position;
                return new Vector2Int(
                    Mathf.FloorToInt(p.x / ChunkSize.x), 
                    Mathf.FloorToInt(p.z / ChunkSize.y)
                );
            }
        }

        // TODO if chunkType is not fixed?
        public Chunk PlayerChunk => GetChunk(PlayerPosition, 1);
    }
}
