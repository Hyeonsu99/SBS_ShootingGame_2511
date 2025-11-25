using UnityEngine;

public class ScoreManager : MonoBehaviour, IManager
{
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeGameScore;
    public static event ScoreChange OnChangeGemCount;
    public static event ScoreChange OnChangePlayerHP;
    public static event ScoreChange OnChangePlayerBomb;

    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDied;

    private int gameScore;
    private int curHP;
    private int maxHP;
    private int gemCount;
    private int bombCount;

    public int Score => gameScore;
    public int CurHP => curHP;
    public int MaxHP => maxHP;
    public int GemCount => gemCount;
    public int BombCount => bombCount;
    private int SetGameScore
    {
        set
        {
            gameScore = value;
            OnChangeGameScore?.Invoke(gameScore);
        }
    }
    private int SetGemCount
    {
        set
        {
            gemCount = value;
            OnChangeGemCount?.Invoke(gemCount);
        }
    }
    private int SetBombCount
    {
        set
        {
            bombCount = value;
            OnChangePlayerBomb?.Invoke(bombCount);
        }
    }
    private int SetCurHP
    {
        set
        {
            curHP = value;
            OnChangePlayerHP?.Invoke(curHP);
        }
    }

    public void GameInitialize()
    {
        SetGameScore = 0;
        SetCurHP = maxHP = StaticValues.InitHP;
        SetGemCount = 0;
        SetBombCount = StaticValues.InitBomb;
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
        
    }

    public void GameTick(float delta)
    {
        
    }

    private void OnEnable()
    {
        Enemy.OnMonsterDied += HandleMonsterDied;
        Drop_Gem.OnPickupGem += HandleGemPickup;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
        Drop_Gem.OnPickupGem -= HandleGemPickup;
    }

    private void HandleMonsterDied(Enemy enemyInfo)
    {
        SetGameScore = Score + 5;
        Debug.Log($"점수 확인 : {Score}");
    }

    private void HandleGemPickup()
    {
        SetGameScore = Score + 3;
        SetGemCount = GemCount + 1;
        Debug.Log($"점수 확인 : {Score}");
        Debug.Log($"보석 확인 : {GemCount}");
    }

    private void HandleChangeHP(bool isIncreased)
    {
        if(isIncreased)
        {
            IncreaseHP();
        }
        else
        {
            DecreaseHP();   
        }
    }

    public void IncreaseHP()
    {
        curHP++;
        if (curHP >= maxHP)
            curHP = maxHP;
        OnChangePlayerHP?.Invoke(curHP);
    }

    public void DecreaseHP()
    {
        curHP--;
        if (curHP <= 0)
        {
            curHP = 0;
            OnPlayerDied?.Invoke();
        }
        OnChangePlayerHP?.Invoke(curHP);
    }

    public void IncreaseBomb()
    {
        bombCount++;
        if (bombCount >= 99)
            bombCount = 99;
        OnChangePlayerBomb?.Invoke(bombCount);
    }

    public void DecreaseBomb()
    {
        bombCount--;
        if (bombCount <= 0)
            bombCount = 0;
        OnChangePlayerBomb?.Invoke(bombCount);
    }
}
