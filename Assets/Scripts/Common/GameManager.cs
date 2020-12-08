﻿using System;
using System.Collections;
using Player;
using UnityEngine;
using World;

namespace Common {
    public class GameManager : MonoBehaviour
    {
        public enum DayStatus { Day, Night };

        public GameObject playerObject;
        public GameObject worldManagerObject;
        public GameObject enemyManagerObject;

        [NonSerialized] public PlayerController Player;
        [NonSerialized] public WorldManager World;
        [NonSerialized] public EnemyManager Enemies;

        public float time;
        public int changePeriod = 100;
        public DayStatus dayStatus;
        public Light directionalLight;
        public LightingPreset lightPreset;

        private int _dayCounter = 0;

        public void OnEnable() {
            Player = playerObject.GetComponent<PlayerController>();
            World = worldManagerObject.GetComponent<WorldManager>();
            Enemies = enemyManagerObject.GetComponent<EnemyManager>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                StartGame();

            time += Time.deltaTime;

            float time24h = time / changePeriod;

            RenderSettings.ambientLight = lightPreset.AmbientColor.Evaluate(time24h);
            RenderSettings.fogColor = lightPreset.FogColor.Evaluate(time24h);

            if (directionalLight != null)
            {
                directionalLight.color = lightPreset.DirectionalColor.Evaluate(time24h);
                directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((time24h * 360f) - 90f, 170f, 0));
            }
        }

        public void StartGame()
        {
            time = 0;
            dayStatus = DayStatus.Day;

            if (RenderSettings.sun != null && directionalLight == null)
                directionalLight = RenderSettings.sun;

            StartCoroutine(UpdateGame());
            Debug.Log("Starting game...");
        }

        public void ChangeDayStatus()
        {
            if (dayStatus == DayStatus.Day) {
                dayStatus = DayStatus.Night;
                return;
            }
            
            dayStatus = DayStatus.Day;
            _dayCounter++;
        }

        public int DayCounter => _dayCounter;

        public IEnumerator UpdateGame()
        {
            while (true)
            {
                if ((int)time % changePeriod == 0)
                    ChangeDayStatus();

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
