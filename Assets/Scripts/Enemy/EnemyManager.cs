using System.IO;
using UnityEngine;

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
    }

    public void SpawnWave(int day = -1, int num = -1)
    {
        int intDay = day != -1 ? day : this.day;
        int intNum = num != -1 ? num : this.num;

        intDay -= 1;
        intNum -= 1;

        if (intDay >= waveData.data.Length)
        {
            intDay = 0;
            intNum = 0;
        } else if (intNum >= waveData.data[intDay].data.Length)
            intNum = 0;

        WaveData current = waveData.data[intDay].data[intNum];
        Debug.Log("Spawning wave..." + (intDay + 1) + " / " + (intNum + 1) + " / " + JsonUtility.ToJson(current));

        for (int i = 0; i < current.normal; i++)
            Instantiate(normalEnemy);
        for (int i = 0; i < current.highspeed; i++)
            Instantiate(highspeedEnemy);
        for (int i = 0; i < current.highdamage; i++)
            Instantiate(highdamageEnemy);

        if (intNum == waveData.data[intDay].data.Length - 1)
        {
            this.day += 1;
            this.num = 1;
        }
        else
            this.num += 1;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadData();

        if (Input.GetKeyDown(KeyCode.P))
            SpawnWave();
    }
}
