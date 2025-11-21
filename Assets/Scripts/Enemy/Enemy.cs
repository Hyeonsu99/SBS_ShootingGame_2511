using System;
using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    private Vector2 moveDir;
    private float moveSpeed = 3f;
    private bool isInit = false;

    private int curHP;
    private int maxHP = 10;

    public bool isDead { get => curHP <= 0; }

    public delegate void MonsterDiedEvent(Enemy enemyInfo);
    public static event MonsterDiedEvent OnMonsterDied;

    //public static Action<Enemy> OnMonsterDiedAct; 같은 의미임

    private void Update()
    {
        if (isInit && !isDead)
            Move(Time.deltaTime, moveDir);
    }

    public void Move(float delta, Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * delta);
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
        if (newEnable)
        {
            curHP = maxHP;
            if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
                col.isTrigger = true;
        }
    }

    public void TakeDamaged(GameObject attacker, int damage)
    {
        if(!isDead )
        {
            curHP -= damage;
            if (curHP <= 0)
                OnDied();
            else
                OnHit();

        }
    }

    private void OnDied()
    {
        OnMonsterDied?.Invoke(this);
        Destroy(gameObject); // << 풀링 생략.
        // 이펙트 재생
        // 아이템 드랍
        // 점수 증가  <- 이러면 안됨
    }

    private void OnHit()
    {
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
