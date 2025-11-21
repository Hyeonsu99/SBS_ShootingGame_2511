using UnityEngine;

// 지정된 방향으로 지정된 속도로 지속 이동
// 발사시켜준 owner 주체와 다른 팀의 대상과 닿았을 때 데미지
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IMovement
{
    public enum Type
    {
        player01,
        player02,
        player03,
        boss01,
        boss02,
        boss03
    }

    // DI : 의존성 주입 패턴
    // 객체가 사용을 필요로 하는 정보를 외부에서 주입해주는 패턴

    private float moveSpeed = 10f;
    private int damage;
    private Vector2 moveDir;
    private GameObject owner;
    private string ownerTag;

    private bool isInit = false;
    private Type type;

    private void Awake()
    {
        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 0.1f;
            col.isTrigger = true;
        }
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
            body.gravityScale = 0;
    }

    // 외부 데이터 주입
    public void Initialize(Type newType, Vector2 newDir, GameObject newOwner, int newDmg, float newSpeed)
    {
        type = newType;
        moveDir = newDir;
        owner = newOwner;
        ownerTag = owner.tag;
        damage = newDmg;
        moveSpeed = newSpeed;
        SetEnable(true);
    }
    private void Update()
    {
        if (!isInit)
            return;
        Move(moveDir, Time.deltaTime);          // 잘못된 구조
    }
    public void Move(Vector2 direction, float delta)
    {
        transform.Translate(direction * moveSpeed * delta);
    }
    public void SetEnable(bool newEnable)
    {
        // 조건 체크가 필요하기도 함
        isInit = newEnable;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // return 을 먼저 띄우는 스타일 : early return
        if (!isInit)
            return;
        if (collision.gameObject == owner)      // 자폭 금지
            return;
        if (collision.CompareTag(ownerTag))     // 아군 오사 금지
            return;

        if(collision.CompareTag("DestroyArea"))
        {
            ProjectileManager.instance.ReturnProjectileToPool(this, type);
            return;
        }
        
        if(collision.TryGetComponent<IDamaged>(out IDamaged component))
        {
            component.TakeDamage(owner, damage);
            ProjectileManager.instance.ReturnProjectileToPool(this, type);
            return;
        }
    }
}
