using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemCountText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bombText;

    [SerializeField] Image[] playerHeart;

    private void OnEnable()
    {
        ScoreManager.OnChangeGameScore += ChangeScoreText;
        ScoreManager.OnChangeGemCount += ChangeGemCountText;
        ScoreManager.OnChangePlayerBomb += ChangeBombText;
        ScoreManager.OnChangePlayerHP += ChangeHPImage;
    }

    private void OnDisable()
    {
        ScoreManager.OnChangeGameScore -= ChangeScoreText;
        ScoreManager.OnChangeGemCount -= ChangeGemCountText;
        ScoreManager.OnChangePlayerBomb -= ChangeBombText;
        ScoreManager.OnChangePlayerHP -= ChangeHPImage;
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

    private void ChangeHPImage(int curHP)
    {
        for(int i = 0; i < 5; ++i)
        {
            if(i < curHP)
            {
                playerHeart[i].enabled = true;
            }
            else
            {
                playerHeart[i].enabled = false;
            }
        }
    }
}
