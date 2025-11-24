using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Drop_Gem : MonoBehaviour, IPickup
{
    public static Action OnPickupGem;

    private Rigidbody2D rig;

    private bool isSetTarget = false;
    private GameObject target;
    private float pickupTime;

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 0.2f;
            col.isTrigger = true;
        }

        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 1.0f;
            Vector2 initVelocity = Vector2.zero;
            initVelocity.x = Random.Range(-0.5f, 0.5f);
            initVelocity.y = Random.Range(-3f, 5f);

            rig.AddForce(initVelocity, ForceMode2D.Impulse);
        }
    }

    // 최초로 플레이어 습득 범위에 닿았을 때
    public void OnPickup(GameObject picker)
    {
        rig.gravityScale = 0f;
        rig.linearVelocity = Vector2.zero;  // 현재 속도 0으로 초기화
        isSetTarget = true;
        target = picker;
        pickupTime = 0f;
    }

    private void Update()
    {
        if(isSetTarget && target.activeSelf)
        {
            pickupTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.transform.position, pickupTime / 1f);

            if(pickupTime > 1f)
            {
                //OnPickupGem.Invoke(); // 이벤트 발생
                Destroy(gameObject);  // 자기 자신 파괴
            }
        }
    }


}
