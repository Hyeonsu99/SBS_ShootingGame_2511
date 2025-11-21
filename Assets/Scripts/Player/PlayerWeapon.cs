using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Projectile.Type projectileType;
    [SerializeField] private Transform fireTrans;

    [Header("발사 관련 Data")]
    [SerializeField] private int numOfProjectiles = 5; // 한 번에 발사되는 투사체의 숫자.
    [SerializeField] private float spreadAngle = 5; // 투사체가 여러 개 발사될 때 , 발사 간의 간격.
    [SerializeField] private float fireRate = 0.3f; // 투사체 발사와 발사 사이 간격
    
    private float nextFireTime;
    private bool isFiring = false;

    // 연산용 변수
    float startAngle;
    float angle;
    Quaternion fireRotation;
    GameObject go;
    Projectile projectileComp;


    public void SetEnable(bool newEnable)
    {
        isFiring = newEnable;

        if(newEnable)
        {
            nextFireTime = 0f;
        }
    }

    public void SetFire()
    {
        if (Time.time < nextFireTime)
            return;

        if (!isFiring)
            return;

        // 발사 조건 충족
        nextFireTime += Time.time + fireRate;

        startAngle = -spreadAngle * (numOfProjectiles - 1) / 2f;
        
        for(int i = 0; i < numOfProjectiles; ++i)
        {
            angle = startAngle + spreadAngle * i;

            fireRotation = fireTrans.rotation * Quaternion.Euler(0f, 0f, angle);

            Vector2 fireDir = fireRotation * Vector2.up;

            ProjectileManager.Instance.FireProjectile(projectileType, fireTrans.position, fireDir, gameObject, 1, 10f);
        }
    }

    public void SetOwner(GameObject owner)
    {
        
    }
}
