using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    private Vector2 moveDir = Vector2.down;
    private float moveSpeed = 3f;
    private bool isInit = false;

    private int curHP;
    private int maxHP = 10;

    public bool IsDead { get => curHP <= 0; }

    public delegate void MonsterDiedEvent(Enemy enemyInfo);
    public static event MonsterDiedEvent OnMonsterDied;

    public static Action<Enemy> OnMonsterDiedAction;

    private void Awake()
    {
        SetEnable(true);
    }

    private void Update()
    {
        if (isInit && !IsDead)
            Move(Time.deltaTime, moveDir);
    }

    public void Move(float delta, Vector2 direction)
    {
        transform.Translate(direction * (moveSpeed * delta));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;

        if(newEnable)
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
            {
                OnDied();
            }
            else
                OnHit();
        }
    }

    private void OnDied()
    {
        //OnMonsterDied.Invoke(this);
        Destroy(gameObject);
    }

    private void OnHit()
    {
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
