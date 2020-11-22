using System;
using System.Collections;
using System.Collections.Generic;
using Block;
using Entity;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldManager : MonoBehaviour
{
    public int seed = -1;
    public float perlinThreshold = 0.5f;
    public Vector2Int maxThreshold;

    public GameObject landObject;

    private Dictionary<Tuple<int, int>, BlockBase> blocks;
    public EntityManager entityManager;

    public void GenerateWorld()
    {
        this.entityManager = new EntityManager();
        
        if (seed != -1)
            Random.InitState(seed);

        float width = landObject.transform.lossyScale.x;
        float height = landObject.transform.lossyScale.y;

        for (float i = 0; i < maxThreshold.x; i++)
        {
            for (float j = 0; j < maxThreshold.y; j++)
            {
                if (!IsWater((int) i, (int) j)) {
                    Instantiate(landObject, new Vector3(i * width, 0, j * height), Quaternion.identity);
                }
            }
        }
    }

    public bool IsWater(int x, int y) {
        return Mathf.PerlinNoise((float) x / 10, (float) y / 10) <= perlinThreshold;
    }
    
    public BlockBase GetBlock(int x, int y) {
        var contains = blocks.TryGetValue(new Tuple<int, int>(x, y), out var block);
        return contains ? block : null;
    }

    public bool IsAir(int x, int y) {
        if (IsWater(x, y))
            return false;
        
        var location = new Tuple<int, int>(x, y);
        
        if (blocks.ContainsKey(location))
            return false;

        return true;
    }
    
    public bool SetBlock(BlockBase block) {
        var x = block.getX();
        var y = block.getY();

        if (!IsAir(x, y))
            return false;

        var location = new Tuple<int, int>(x, y);
        blocks.Add(location, block);
        return true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateWorld();
    }
}
