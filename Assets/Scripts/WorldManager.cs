using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public int seed = -1;
    public float perlinThreshold = 0.5f;
    public Vector2Int maxThreshold;

    public GameObject landObject;

    public void GenerateWorld()
    {
        if (seed != -1)
            Random.InitState(seed);

        float width = landObject.transform.lossyScale.x;
        float height = landObject.transform.lossyScale.y;

        for (float i = 0; i < maxThreshold.x; i++)
        {
            for (float j = 0; j < maxThreshold.y; j++)
            {
                var perlin = Mathf.PerlinNoise(i / 10, j / 10);
                if (perlin > perlinThreshold)
                {
                    Instantiate(landObject, new Vector3(i * width, 0, j * height), Quaternion.identity);
                }
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateWorld();
    }
}
