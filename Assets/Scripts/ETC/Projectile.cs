using UnityEngine;

// 지정된 방향, 속도로 지속적으로 이동하는 기능
// 발사시켜준 owner(주체)와 다른 팀의 대상이 닿았을 때, 상대방에게 데미지 전달


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour, IMovement
{
    public enum Type
    {
        player01, player02, player03,
        boss1, boss2, boss3
    }

    // 일반적으로 구조체를 만들어서 사용
    // 스크립터블 오브젝트 활용
    // 데이터 매니저


    // 절차지향적 프로그래밍
    // 객체지향적 프로그래밍
    // DI 의존성 주입
    // Data-Driven Programming
    private float moveSpeed = 10f;
    private int damage;
    private Vector2 moveDir;
    private GameObject owner;
    private string ownerTag;

    private bool isInit = false;
    private Type type;

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 0.1f;
            col.isTrigger = true;

            
        }

        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.gravityScale = 0f;
        }
    }

    // 데이터를 구조체로 받아오는 것이 실무에 더 가까움
    public void InitProjectile(Type newType, 
                               Vector2 newDir,
                               GameObject newOwner,
                               int newDamage,
                               float newSpeed)
    {
        type = newType;
        moveDir = newDir;
        owner = newOwner;
        ownerTag = owner.tag;
        damage = newDamage;
        moveSpeed = newSpeed;
        SetEnable(true);
    }

    private void Update()
    {
        if (isInit)
            Move(Time.deltaTime, moveDir);
    }

    public void Move(float delta, Vector2 direction)
    {
        transform.Translate(direction * (moveSpeed * delta));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner)
            return;                         // 얼리 리턴 : 예외 사항을 최상단에서 처리
        if (collision.CompareTag(ownerTag)) // 보스가 쏜 투사체를 몬스터들이 맞으면 안되니까...
            return;

        if(collision.CompareTag("DestroyArea"))
        {

        }

        // 나머지 상황
        if(collision.TryGetComponent<IDamaged>(out IDamaged component))
        {
            component.TakeDamage(owner, damage);

            return;
        }
    }
}
