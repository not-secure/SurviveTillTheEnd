using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum DayStatus { Day, Night };

    public int time;
    public int changePeriod = 100;
    public DayStatus dayStatus;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            StartGame();
    }

    public void StartGame()
    {
        time = 0;
        dayStatus = DayStatus.Day;

        StartCoroutine(UpdateGame());
        Debug.Log("Starting game...");
    }

    public void ChangeDayStatus()
    {
        if (dayStatus == DayStatus.Day)
            dayStatus = DayStatus.Night;
        else
            dayStatus = DayStatus.Day;
    }

    public IEnumerator UpdateGame()
    {
        while (true)
        {
            time += 1;

            if (time % changePeriod == 0)
                ChangeDayStatus();

            yield return new WaitForSeconds(1f);
        }
    }
}
