using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chunk
{
    public Vector2Int position;
    public Vector2Int size;
    public int type;
    public GameObject gameObject;

    public Chunk(Vector2Int position, int type)
    {
        this.position = position;
        this.type = type;
        size = new Vector2Int(1, 1);

        int randInt = Random.Range(1, 5);
        gameObject = Object.Instantiate(Resources.Load<GameObject>("Chunks/" + type.ToString() + "_type/" + randInt + "_num"));
        gameObject.transform.position = new Vector3(position.x * 5, 0, position.y * 5);
    }
}