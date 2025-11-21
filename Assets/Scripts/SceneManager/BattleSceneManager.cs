using System.Collections;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private IManager playerManager;
    private IManager scrollManager;
    private IManager spawnManager;

    private void Awake()
    {
        GameObject go;
        go = GameObject.Find("Player");
        if (go != null)
            go.TryGetComponent<IManager>(out playerManager);
        go = GameObject.Find("ScrollManager");
        if (go != null) 
            go.TryGetComponent<IManager>(out scrollManager);
        go = GameObject.Find("SpawnManager");
        if (go != null)
            go.TryGetComponent<IManager>(out spawnManager);
        StartCoroutine("GameStart");
    }

    bool isPlaying = false;

    IEnumerator GameStart()
    {
        yield return null;
        playerManager?.GameInitialize();
        scrollManager?.GameInitialize();
        spawnManager?.GameInitialize();

        for (int i = 5; i >= 0; --i)
        {
            Debug.Log($"게임 시작 준비중...{i}");
            yield return new WaitForSeconds(1f);
        }

        isPlaying = true;
        playerManager?.GameStart();
        scrollManager?.GameStart();
        yield return new WaitForSeconds(2f);
        spawnManager?.GameStart();
    }

    private void Update()
    {
        if (isPlaying)
        {
            playerManager?.GameTick(Time.deltaTime);
            scrollManager?.GameTick(Time.deltaTime);
        }
    }
}
