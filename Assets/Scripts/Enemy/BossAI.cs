using System;
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public enum BossState
{
    BS_MoveToAppear, // 전투 위치로 이동
    BS_Phase01, // 일반 모드
    BS_Phase02  // 광폭화
}

// STATE


public class BossAI : MonoBehaviour, IMovement, IDamaged
{
    [SerializeField] private float bossBattlePositionY = 0.5f;
    private BossState currentState = BossState.BS_MoveToAppear;

    private IWeapon[] weapons;
    private int curWeapons = 0;

    private Vector2 moveDir = Vector2.zero;
    private bool isInit = false;
    private bool isBattle = false;
    private float moveSpeed = 3f;

    // 기본 정보
    private string bossName;
    private int maxHP;

    // 내부 체력
    private int curHP;

    public bool IsDead { get => curHP <= 0; }

    public delegate void BossDiedEvent();
    public event BossDiedEvent OnBossDied;


    public void InitBoss(string name, int newMaxHP, IWeapon[] newWeapons)
    {
        bossName = name;
        maxHP = newMaxHP;
        weapons = newWeapons;

        SetEnable(true); // isInit = true;
        ChangeState(BossState.BS_MoveToAppear); // AI의 시작점
    }

    private void Update()
    {
        if (isInit)
        {
            Move(moveDir, Time.deltaTime);
        }
    }

    public void Move(Vector2 direction, float delta)
    {
        transform.Translate(direction * (moveSpeed * delta));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if(!IsDead && isBattle)
        {
            curHP -= damage;
            if(curHP <= 0)
            {
                OnDied();
            }
            else
            {
                OnDamaged();
            }
        }
    }

    private void OnDamaged() // 이벤트 - 체력이 50% 미만일 때, 페이즈2로 전환시켜주는 이벤트
    {
        if (currentState == BossState.BS_Phase01 && (float)curHP / maxHP < 0.5)
        {
            ChangeState(BossState.BS_Phase02); // << 첫번째 상태 전환 이벤트
        }
    }

    private void OnDied()
    {
        OnBossDied?.Invoke(); // 스폰 매니저가 다음 Wave로 넘어가도록 처리 로직

        Destroy(gameObject);
    }
    private void ChangeState(BossState newState)
    {
        StopCoroutine(currentState.ToString());

        currentState = newState;

        StartCoroutine(currentState.ToString());
    }

    IEnumerator BS_MoveToAppear()
    {

        moveDir = Vector2.down;

        while(true)
        {
            if(transform.position.y <= bossBattlePositionY)
            {
                moveDir = Vector2.zero; // 이동을 멈춤
                curHP = maxHP;
                isBattle = true;
                ChangeState(BossState.BS_Phase01);
            }
            yield return null;
        }
    }

    IEnumerator BS_Phase01()
    {
        curWeapons = 0;
        weapons[curWeapons].SetEnbale(true);

        while(true)
        {
            weapons[curWeapons].SetFire();
            // 애니메이션 실행
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator BS_Phase02()
    {
        weapons[curWeapons].SetEnbale(false);
        curWeapons = 1;
        weapons[curWeapons].SetEnbale(true);

        moveDir = Vector2.right;

        while(true)
        {
            weapons[curWeapons].SetFire();
            if (transform.position.x <= -2.5f)
            {
                transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
                moveDir = Vector2.right;
            }
            else if (transform.position.x >= 2.5f)
            {
                transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
                moveDir = Vector2.left;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
