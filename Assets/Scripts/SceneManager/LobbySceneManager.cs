using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MenuType
{
    Menu_Enchant = 1,
    Menu_Item = 2,
    Menu_Skill = 3,
    Menu_Card = 4,
    Menu_Option = 5,
}


public class LobbySceneManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject go = GameObject.Find("GameStartButton");
        if (go != null)
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                PlayerPrefs.SetString("NextSceneName", "BattleScene");
                SceneManager.LoadScene("LoadingScene");
            });
    }

    private int activeMenu = 0;

    private int ActiveMenu
    {
        set
        {
            if (value < 1 || value > 5)
            {
                activeMenu = 0;
            }
            else
            {
                activeMenu = value;
            }
        }
    }

    [SerializeField] private List<GameObject> popUpList;
    private float hidePosY = -2000f;

    public void OnClickButton(int i)
    {
        if(activeMenu == i) // 현재 열린 팝업과 같은 버튼을 다시 눌렀다.
        {
            ActiveMenu = 0;
            LeanTween.moveLocalY(popUpList[i], hidePosY, 0.5f); // 닫기
        }
        else
        {
            if(activeMenu != 0)
            {
                LeanTween.moveLocalY(popUpList[activeMenu], hidePosY, 0.5f); // 닫기
            }
            activeMenu = i;
            LeanTween.moveLocalY(popUpList[activeMenu], 0f, 0.5f); // 열기
        }
    }
}
