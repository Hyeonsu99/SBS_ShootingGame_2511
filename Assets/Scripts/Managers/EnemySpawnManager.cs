using System;
using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour, IManager
{
    [SerializeField] private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnPrefabs;

    public static Action OnSpawnFinish; // 일반 몬스터 스폰이 종료됨을 알림

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 7;

    public static Action OnBossDead;

    public void GameInitialize()
    {
        spawnLevel = 0;
        spawnCount = 7;
        spawnDelta = 1f;

    }

    public void GameOver()
    {
    }

    public void GamePause()
    {
    }

    public void GameResume()
    {
    }

    public void GameStart()
    {
        StartCoroutine(StartWave());
    }

    public void GameTick(float delta)
    {
    }
    private GameObject go;
    IEnumerator StartWave()
    {
        while (spawnCount > 0)
        {
            for (int i = 0; i < spawnTrans.Length; ++i)
            {
                go = Instantiate(spawnPrefabs[spawnLevel], spawnTrans[i].position, Quaternion.identity);
                if (go.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.SetEnable(true);
                }
            }
            spawnCount--;
            yield return new WaitForSeconds(spawnDelta);
        }
        OnSpawnFinish?.Invoke();

        // 보스 생성

        spawnLevel++;
        if(spawnLevel >= 3)
        {
            spawnLevel = 0;
        }
        spawnCount = 7;
    }
}
