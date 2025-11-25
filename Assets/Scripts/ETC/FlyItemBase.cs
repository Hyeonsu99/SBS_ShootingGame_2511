using System.Collections;
using UnityEngine;

// 추상 클래스 - 순수 가상 함수를 가지고 있는 클래스
// 스스로 객체를 만들어 낼 수 없다. ==> 파생 클래스의 부모 역할
// 다형성을 구현하기 위한 요소

// 추상 클래스와 인터페이스 : 상속 계층 구조가 명확하여 단일 상속 구조를 유지할 수 있을떄 -> 추상 클래스
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class FlyItemBase : MonoBehaviour, IMovement, IPickup
{
    public abstract void ApplyEffect(GameObject target); // 파생클래스에서 구현의 의무가 있는 추상 메서드

    private bool isInit = false;
    private float flySpeed = 0.7f;
    private Vector2 flyDirection = Vector2.zero;
    private Vector3 flyTargetPos = Vector3.zero;

    private CircleCollider2D col;

    private CircleCollider2D Col
    {
        get
        {
            if (col == null)
                TryGetComponent<CircleCollider2D>(out col);
            return col;
        }
    }

    private ScoreManager scoreManager;
    public ScoreManager ScoreManagerRef
    {
        get
        {
            if(scoreManager == null)  
                scoreManager = FindAnyObjectByType<ScoreManager>();
            return scoreManager;
        }
    }

    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0f;
        }
        Col.isTrigger = true;
        Col.radius = 0.25f;

        SetEnable(true);
    }

    private void Update()
    {
        if (isInit)
            Move(flyDirection, Time.deltaTime);
    }

    public void Move(Vector2 direction, float delta)
    {
        transform.Translate(direction * (flySpeed * delta));
    }

    public void OnPickup(GameObject picker)
    {
        ApplyEffect(picker);
        Destroy(gameObject);
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
        if (isInit)
            StartCoroutine(ChangeFlyDirection());
    }

    IEnumerator ChangeFlyDirection()
    {
        while(true)
        {
            flyTargetPos.x = Random.Range(-2f, 2f);
            flyTargetPos.y = Random.Range(-2f, 2f);
            flyTargetPos.z = 0f;

            flyDirection = (flyTargetPos - transform.position).normalized;

            yield return new WaitForSeconds(4f);
        }
    }
}
