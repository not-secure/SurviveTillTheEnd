using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Common;

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

        public PlayerController Player { get; private set; }
        private Dictionary<Vector2Int, Chunk> _chunkMap;
        private readonly Vector2Int[] _directions = new Vector2Int[4] {
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0)
        };

        public Image gameOverImage;
        public TextMeshProUGUI gameOverTMP1, gameOverTMP2;

        private bool isGameOver = false;

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

        public void Awake()
        {
            gameOverImage.color = new Color(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, 0f);
            gameOverTMP1.color = new Color(gameOverTMP1.color.r, gameOverTMP1.color.g, gameOverTMP1.color.b, 0f);
            gameOverTMP2.color = new Color(gameOverTMP2.color.r, gameOverTMP2.color.g, gameOverTMP2.color.b, 0f);
        }

        public void Start() {
            _chunkMap = new Dictionary<Vector2Int, Chunk>(); 
            Player = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<PlayerController>();
            
            GenerateNeighborChunks(PlayerChunk);
        }

        private Vector2Int _lastPosition;
        public void Update() {
            if (isGameOver)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                else if (Input.GetKeyDown(KeyCode.Escape))
                    Application.Quit();

                return;
            }

            var position = PlayerPosition;
            if (_lastPosition != position) {
                // GenerateNeighborChunks(PlayerChunk);
                _lastPosition = position;
            }
        }
        
        public Vector2Int PlayerPosition {
            get {
                var p = Player.transform.position;
                return new Vector2Int(
                    Mathf.FloorToInt(p.x / ChunkSize.x), 
                    Mathf.FloorToInt(p.z / ChunkSize.y)
                );
            }
        }

        // TODO if chunkType is not fixed?
        public Chunk PlayerChunk => GetChunk(PlayerPosition, 1);

        public void GameOver()
        {
            if (isGameOver) return;

            GameManager manager = FindObjectOfType<GameManager>();
            int day = (int)(manager.time / manager.changePeriod);
            int hrs = (int)(manager.time % manager.changePeriod / manager.changePeriod * 24);

            gameOverTMP2.text = "You survived for " + day + " day" + (day > 1 ? "s " : " ") + hrs + (hrs > 1 ? " hours" : " hour") + ".\n\n" + gameOverTMP2.text;

            Debug.Log("GameOver");
            StartCoroutine(GameOverOverlay());
            isGameOver = true;
        }

        private IEnumerator GameOverOverlay()
        {
            Time.timeScale = 0f;

            while (gameOverImage.color.a < 1f)
            {
                gameOverImage.color = new Color(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, gameOverImage.color.a + 0.005f);
                yield return new WaitForSecondsRealtime(0.01f);
            }

            Time.timeScale = 1f;

            yield return new WaitForSecondsRealtime(0.5f);

            while (gameOverTMP1.color.a < 1f)
            {
                gameOverTMP1.color = new Color(gameOverTMP1.color.r, gameOverTMP1.color.g, gameOverTMP1.color.b, gameOverTMP1.color.a + 0.005f);
                yield return new WaitForSecondsRealtime(0.01f);
            }

            yield return new WaitForSecondsRealtime(0.5f);

            while (gameOverTMP2.color.a < 1f)
            {
                gameOverTMP2.color = new Color(gameOverTMP2.color.r, gameOverTMP2.color.g, gameOverTMP2.color.b, gameOverTMP2.color.a + 0.005f);
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
    }
}
