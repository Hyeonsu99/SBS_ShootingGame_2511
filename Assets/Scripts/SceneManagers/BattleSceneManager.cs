using System.Collections;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private IManager playerManager;

    private void Awake()
    {
        GameObject obj;

        obj = GameObject.Find("Player");

        if(obj != null)
            playerManager = obj.GetComponent<IManager>();

        StartCoroutine(Gamestart());
    }

    bool isPlaying = false;

    IEnumerator Gamestart()
    {
        yield return null;

        playerManager?.GameInitialize();

        for(int i = 5; i >= 0; i--)
        {
            Debug.Log($"게임 시작 준비중.... {i}");
            yield return new WaitForSeconds(1f);
        }

        isPlaying = true;
        playerManager.GameStart();
        
        yield return new WaitForSeconds(2f);
    }

    private void Update()
    {
        if(isPlaying)
        {
            playerManager?.GameTick(Time.deltaTime);
        }
    }
}
