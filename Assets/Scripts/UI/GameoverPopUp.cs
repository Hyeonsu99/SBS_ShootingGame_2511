using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverPopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUPPanel;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject gameScore;
    [SerializeField] private GameObject gemScore;
    [SerializeField] private GameObject buttonObj;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gemText;

    private Coroutine popUpCoroutine;

    private void OnEnable()
    {
        ScoreManager.OnPlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        ScoreManager.OnPlayerDied -= HandlePlayerDied;
    }

    private void HandlePlayerDied()
    {
        LeanTween.scaleX(popUPPanel, 1f, 0.7f).setEaseInBounce();
        LeanTween.scaleX(title, 1f, 0.7f).setEaseInBack().setDelay(1f);
        LeanTween.scaleY(gameScore, 1f, 0.7f).setEaseInCubic().setDelay(1.7f);
        LeanTween.scaleY(gemScore, 1f, 0.7f).setEaseInCubic().setDelay(2.0f);
        LeanTween.scaleY(buttonObj, 1f, 0.7f).setEaseInCubic().setDelay(3.5f);
    }

    private void LoadLobbyScene()
    {
        PlayerPrefs.SetString("NextSceneName", "LobbyScene"); // << Android, IOS, PC 모든 운영체제에서 각각의 저장소 경로가 나누어져 있다. Unity Manual에 적혀 있다... ,
                                                              // 임시 저장소에 저장(클라이언트가 꺼지기 전까지) , 유니티 에디터가 꺼질 때 ROM(각 운영체제에서 설정한 위치에 저장됨
        SceneManager.LoadScene("LoadingScene");
    }
}
