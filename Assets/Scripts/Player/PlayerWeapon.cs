using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Projectile.projectileType projectileType;
    [SerializeField] private Transform fireTrans;

    [Header("발사 관련 Data")]
    [SerializeField] private float spreadAngle = 5; // 투사체가 여러발 발사 될 때, 발사 간의 사이각
    [SerializeField] private float fireRate = 0.3f; // 투사체 발사와 발사 사이 간격
    [SerializeField] private int numofProjectiles = 5; // 한번에 발사되는 투사체의 숫자

    private float nextFireTime;
    private bool isFiring = false; // 활성화 중인 무기인지.

    //연산용 변수
    float startAngle;
    float angle;
    Quaternion fireRotation;
    GameObject go;
    Projectile projectileComp;

    public void SetEnable(bool newEnable)
    {
        isFiring = newEnable;
        if (newEnable)
            nextFireTime = 0f;
    }

    public void SetFire()
    {
        if (Time.time < nextFireTime)
            return;

        if (!isFiring)
            return;

        //발사조건 충족.
        nextFireTime = Time.time + fireRate;
        startAngle = -spreadAngle * (numofProjectiles - 1) / 2f; // 시작 각도 계산

        for(int i = 0; i < numofProjectiles; ++i)
        {
            angle = startAngle + spreadAngle * i;

            fireRotation = fireTrans.rotation * Quaternion.Euler(0f, 0f, angle);
            Vector2 fireDir = fireRotation * Vector2.up;
            ProjectileManager.instance.FireProjectile(projectileType, 
                                                        fireTrans.position, 
                                                        fireDir, 
                                                        gameObject, 
                                                        1,
                                                        10f);

        }
    }

    public void SetOwner(GameObject owner)
    {

    }
}
