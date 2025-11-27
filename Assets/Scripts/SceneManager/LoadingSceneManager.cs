using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Image loadBar;

    private void Awake()
    {      
        // SSD의 읽기 속도가 500mb/s이고 씬에서 사용하는 메모리가 5GB일 때, SSD가 로딩을 완료하는 대략 10초동안은 모든 로직이 정지한다. (동기 로딩)
        //SceneManager.LoadScene(PlayerPrefs.GetString("NextSceneName"));
        // 한 프레임에서 처리해야 할 작업이 끝나고 남는 유휴시간을 활용하여 로딩을 조금씩 진행한다.
        //SceneManager.LoadSceneAsync(PlayerPrefs.GetString("NextSceneName"));
        // LoadSceneMode.Single : 현재 존재하는 씬만 남기고 모두 메모리에서 지움
        // LoadSceneMode.Additive : 현재 존재하는 씬과 로딩될 씬을 같이 로딩한다.

        StartCoroutine(LoadingSceneRoutine());
    }
    
    private IEnumerator LoadingSceneRoutine()
    {
        yield return null;

        string nextScene = PlayerPrefs.GetString("NextSceneName", "LobbyScene");

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        operation.allowSceneActivation = false; // 로딩 완료 시 즉시 씬 변경하지 않는다.

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadBar != null)
            {
                loadBar.fillAmount = progress;
            }

            Debug.Log($"{progress * 100.0f:F1}");

            if (operation.progress >= 0.9f)
            {
                for(int i = 3; i > 0; --i)
                {
                    yield return new WaitForSeconds(1f);
                }

                operation.allowSceneActivation = true;
            }

        }
    }
}
