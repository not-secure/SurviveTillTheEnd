using System;
using System.Collections;
using Player;
using UnityEngine;
using World;

namespace Common {
    public class GameManager : MonoBehaviour
    {
        public enum DayStatus { Day, Night };

        public GameObject daylightObject;
        public GameObject playerObject;
        public GameObject worldManagerObject;

        [NonSerialized] public PlayerController Player;
        [NonSerialized] public WorldManager World;

        public int time;
        public int changePeriod = 100;
        public DayStatus dayStatus;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                StartGame();

            daylightObject.transform.rotation = Quaternion.RotateTowards(daylightObject.transform.rotation, Quaternion.Euler(180 + ((float)time / changePeriod * -360), 0, 0), Time.deltaTime);
        }

        public void StartGame()
        {
            time = 0;
            dayStatus = DayStatus.Day;
            daylightObject.transform.Rotate(new Vector3(180, 0, 0));

            Player = playerObject.GetComponent<PlayerController>();
            World = worldManagerObject.GetComponent<WorldManager>();

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
}
