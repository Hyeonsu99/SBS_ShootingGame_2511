using UnityEngine;

public class PlayerWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Projectile.Type projType;
    [SerializeField] private Transform fireTrans;

    [Header("Projectile Data")]
    [SerializeField] private int numOfProjectiles = 5;  // 한번에 발사되는 투사체 수
    [SerializeField] private float spreadAngle = 5;     // 투사체가 여러발 발사될 때 발사 간 사이각
    [SerializeField] private float fireRate = 0.3f;     // 투사체 발사 간격
    [SerializeField] private int fireDmg = 1;           // 투사체 대미지
    [SerializeField] private float fireSpeed = 10f;     // 투사체 속도

    private float nextFireTime;                         // 시간 재는 용도
    private bool isFireing = false;                     // 활성화 여부

    // 연산용 변수
    float startAngle;                                   // 시작 각도
    float angle;                                        // 현재 각도
    Quaternion fireRotation;                            // 발사체 회전


    public void SetEnbale(bool newEnable)
    {
        isFireing = newEnable;
        if (newEnable)
            nextFireTime = 0f;
    }

    public void SetFire()
    {
        if (Time.time < nextFireTime)
            return;
        if (!isFireing)
            return;
        //
        nextFireTime = Time.time + fireRate;
        SoundManager.instance.PlaySFX(SfxType.SFX_PlayerFire);

        startAngle = -spreadAngle * (numOfProjectiles - 1) / 2f;
        for (int i = 0; i < numOfProjectiles; ++i)
        {
            angle = startAngle + (i * spreadAngle);
            fireRotation = fireTrans.rotation * Quaternion.Euler(0f,0f,angle);
            Vector2 fireDir = fireRotation * Vector2.up;
            ProjectileManager.instance.FireProjectile(projType, fireTrans.position, fireDir, gameObject, fireDmg, fireSpeed);
        }
    }

    public void SetOwner(GameObject owner)
    {
    }
}
