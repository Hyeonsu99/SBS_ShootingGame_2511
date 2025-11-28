using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    Vector2 moveDir = Vector2.down;
    float moveSpeed = 3f;
    bool isInit = false;

    private int curHP;
    [SerializeField] private int maxHP = 10;

    public bool IsDead { get => curHP <= 0; }

    public delegate void MonsterDiedEvent(Enemy enemyInfo);
    public static event MonsterDiedEvent OnMonsterDied;

    public void Move(Vector2 direction, float delta)
    {
        transform.Translate(direction * delta * moveSpeed);
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
        if (newEnable)
        {
            curHP = maxHP;
            if (TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
                col.isTrigger = true;
           
        }
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if(!IsDead)
        {
            curHP -= damage;
            if (curHP <= 0)
                OnDied();
            else
                OnHitted();
        }
    }

    private void OnDied()
    {
        OnMonsterDied?.Invoke(this);
        SoundManager.instance.PlaySFX(SfxType.SFX_EnemyDie);
        Destroy(gameObject);        // 풀링 생략(주말에 보강하기)
    }

    private void OnHitted()
    {
        StartCoroutine(ChangeColor());
        // 색 변경은 애니메이션으로 할 예정
    }

    IEnumerator ChangeColor()
    {
        var waitTime = new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.red;

        yield return waitTime;

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Update()
    {
        if (isInit && !IsDead)
        {
            Move(moveDir, Time.deltaTime);
        }
    }

    
}
