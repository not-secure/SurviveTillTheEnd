using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chunk
{
    public Vector2Int Position;
    public Vector2Int Size = new Vector2Int(30, 30);
    public int type;
    public GameObject gameObject;

    private int _variant;
    
    public Chunk(Vector2Int position, int type) {
        this.Position = position;
        this.type = type;

        _variant = Random.Range(1, 5);
    }

    public void Generate() {
        gameObject = Object.Instantiate(
            Resources.Load<GameObject>("Chunks/" + type.ToString() + "_type/" + _variant + "_num")
        );
        
        gameObject.transform.position = new Vector3(Position.x * Size.x, 0, Position.y * Size.y);
    }
}