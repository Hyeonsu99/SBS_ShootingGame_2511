using UnityEngine;


// 지정된 방향으로 지정된 속도로 지속적으로 이동하는 기능
// 발사시켜준 owner 주체와 다른 팀의 대상과 닿았을 때, 상대방에게 데미지 전달

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IMovement
{
    // 선택 사항
    // 전역 공간에서 쓸 수도 있음
    public enum projectileType
    {
        player01,
        player02,
        player03,
        boss01,
        boss02,
        boss03
    }

    // 스크립터블 오브젝트 
    // 데이터 매니저

    // DI 의존성 주입
    private float moveSpeed = 10f;
    private int damage;
    private Vector2 moveDir;
    private GameObject owner;
    private string ownerTag;

    private bool isInit = false;
    private projectileType type;

    // 절차지향적 프로그래밍
    // 객체지향적 프로그래밍
    // DI 의존성 주입

    // Data-Driven Programming

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 0.1f;
            col.isTrigger = true;
        }
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0f;
        }
    }
    public void InitProjectile(projectileType newtype,
                               Vector2 newDir,
                               GameObject newOwner,
                               int newDamage,
                               float newSpeed)
    {
        type = newtype;
        moveDir = newDir;
        owner = newOwner;
        ownerTag = owner.name;
        damage = newDamage;
        moveSpeed = newSpeed;
        SetEnable(true);
    }

    private void Update()
    {
        if(isInit)
            Move(Time.deltaTime, moveDir); // 잘못된 예제...
    }
    public void Move(float delta, Vector2 direction)
    {
        transform.Translate(direction * (moveSpeed * delta));
    }

    public void SetEnable(bool newEnable)
    {
        //조건 체크 필요한 경우도 있어서.
        isInit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == owner)
            return; // 얼리 리턴
        if (collision.CompareTag(ownerTag)) // 보스가 쏜 투사체를 몬스터들이 맞으면 안되니까
            return;

        if(collision.CompareTag("DestroyArea"))
        {
            ProjectileManager.instance.ReturnProjectileToPool(this, type);
            return;
        }

        //나머지 상황
        if (collision.TryGetComponent<IDamaged>(out IDamaged component))
        {
            component.TakeDamaged(owner, damage);
            ProjectileManager.instance.ReturnProjectileToPool(this, type);
            return;
        }
    }
}
