using System.Collections;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private IManager playerManager;
    private IManager scrollManager;
    private IManager spawnManager;
    private bool isPlaying = false;

    private void Awake()
    {
        GameObject obj;
        obj = GameObject.Find("Player");

        if (obj != null )
            playerManager = obj.GetComponent<IManager>();

        obj = GameObject.Find("ScrollManager");

        if (obj != null)
            scrollManager = obj.GetComponent<IManager>();

        obj = GameObject.Find("SpawnManager");

        if (obj != null)
            spawnManager = obj.GetComponent<IManager>();
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return null;
        playerManager?.GameInit();
        scrollManager?.GameInit();
        spawnManager?.GameInit();

        for(int i = 5; i >= 0; --i)
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
        if(isPlaying)
        {
            playerManager?.GameTick(Time.deltaTime);
            scrollManager?.GameTick(Time.deltaTime);
            spawnManager?.GameTick(Time.deltaTime);
        }
    }
}

