using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemCountText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bombText;

    private void OnEnable()
    {
        ScoreManager.OnChangeGameScore += ChangeScoreText;
        ScoreManager.OnChangeGemCount += ChangeGemCountText;
        ScoreManager.OnChangePlayerBomb += ChangeBombText;
    }

    private void OnDisable()
    {
        ScoreManager.OnChangeGameScore -= ChangeScoreText;
        ScoreManager.OnChangeGemCount -= ChangeGemCountText;
        ScoreManager.OnChangePlayerBomb -= ChangeBombText;
    }


    private void ChangeGemCountText(int score)
    {
        gemCountText.text = $"{score}";
    }

    private void ChangeScoreText(int score)
    {
        scoreText.text = $"{score}";
    }

    private void ChangeBombText(int score)
    {
        bombText.text = $"{score}";
    }
}
