using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enemy;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class WaveDataAll
{
    public WaveDataOne[] data;
}

[System.Serializable]
public class WaveDataOne
{
    public WaveData[] data;
}

[System.Serializable]
public class WaveData
{
    public int normal;
    public int highspeed;
    public int highdamage;
}

public class EnemyManager : MonoBehaviour
{
    public WaveDataAll waveData;
    public int day;
    public int num;

    public GameObject normalEnemy;
    public GameObject highspeedEnemy;
    public GameObject highdamageEnemy;

    private List<EnemyBase> _enemies = new List<EnemyBase>();
    private PlayerController _player;

    public void LoadData()
    {
        StreamReader sr = new StreamReader(Path.Combine(Application.streamingAssetsPath, "Data", "enemy.json"));
        string rawData = sr.ReadToEnd();

        WaveDataAll data = (WaveDataAll)JsonUtility.FromJson(rawData, typeof(WaveDataAll));
        waveData = data;

        Debug.Log(JsonUtility.ToJson(data));

        day = 1;
        num = 1;

        normalEnemy = Resources.Load<GameObject>("Prefabs/Enemys/Normal");
        highspeedEnemy = Resources.Load<GameObject>("Prefabs/Enemys/HighSpeed");
        highdamageEnemy = Resources.Load<GameObject>("Prefabs/Enemys/HighDamage");
        _player = FindObjectOfType<PlayerController>();
    }

    public int GetCurrentNightData()
    {
        try
        {
            int d = (day >= waveData.data.Length) ? waveData.data.Length - 1 : day - 1;

            Debug.Log(d);
            Debug.Log(JsonUtility.ToJson(waveData.data[d]));
            return waveData.data[d].data.Length;
        } catch
        {
            Debug.Log(waveData.data[waveData.data.Length - 1].data);
            return waveData.data[waveData.data.Length - 1].data.Length;
        }
    }

    public void SpawnWave(int day = -1, int num = -1)
    {
        int intDay = day != -1 ? day : this.day;
        int intNum = num != -1 ? num : this.num;

        intDay -= 1;
        intNum -= 1;

        if (intDay >= waveData.data.Length)
        {
            intDay = waveData.data.Length - 1;
            intNum = 0;
        } else if (intNum >= waveData.data[intDay].data.Length)
            intNum = 0;

        WaveData current = waveData.data[intDay].data[intNum];
        Debug.Log("Spawning wave..." + (intDay + 1) + " / " + (intNum + 1) + " / " + JsonUtility.ToJson(current));

        for (int i = 0; i < current.normal; i++)
            SpawnEnemy(normalEnemy);
        for (int i = 0; i < current.highspeed; i++)
            SpawnEnemy(highspeedEnemy);
        for (int i = 0; i < current.highdamage; i++)
            SpawnEnemy(highdamageEnemy);

        if (intNum == waveData.data[intDay].data.Length - 1)
        {
            this.day += 1;
            this.num = 1;
        }
        else
            this.num += 1;
    }

    public void SpawnEnemy(GameObject enemy) {
        var enemySpawned = Instantiate(enemy);
        if (_player) {
            enemySpawned.transform.position = _player.transform.position + Quaternion.Euler(
                0, Random.Range(0f, 360f), 0
            ) * Vector3.forward * Random.Range(10f, 20f);
        }
        var enemyController = enemySpawned.GetComponent<EnemyBase>();
        
        _enemies.Add(enemyController);
    }

    public void KillEnemy(EnemyBase enemy) {
        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void AttackInRange(Transform damageSource, float yawRange, float radius, float damage) {
        var attackedEnemies = _enemies
            .Where(enemyBase => {
                var displacement = (enemyBase.transform.position - damageSource.position);
                if (displacement.magnitude > radius)
                    return false;

                var rotY = damageSource.rotation.eulerAngles.y * Mathf.Deg2Rad;
                var direction = new Vector3(Mathf.Sin(rotY), 0, Mathf.Cos(rotY));
                var displacementRot = 
                    Mathf.Acos(Vector3.Dot(direction, displacement.normalized))
                    * Mathf.Rad2Deg;
                
                return Mathf.Abs(displacementRot) < yawRange / 2;
            });

        foreach (var attackedEnemy in attackedEnemies) {
            attackedEnemy.Hurt(damage, damageSource.position);
        }
    }

    public void AttackInRange(Transform damageSource, float radius, float damage) {
        AttackInRange(damageSource, 360, radius, damage);
    }

    public void Awake()
    {
        LoadData();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SpawnWave();
    }
}
