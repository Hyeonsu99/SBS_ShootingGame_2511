using UnityEditor;
using UnityEngine;

public abstract class BossWeaponBase
{
    protected GameObject owner;
}

public class BossWeapon01 : BossWeaponBase, IWeapon
{
    public void SetEnbale(bool newEnable)
    {
        
    }

    public void SetFire()
    {
        Vector3 firePos = owner.transform.position;
        int numProjectiles = 5;
        float spreadAngle = 15f;

        for(int i = 0; i < numProjectiles; ++i)
        {
            float angle = spreadAngle * (i - (numProjectiles - 1) / 2);
            Vector2 fireDirection = Quaternion.Euler(0f, 0f, angle) * Vector2.down;

            ProjectileManager.instance.FireProjectile(Projectile.Type.boss01, firePos, fireDirection, owner, 1, 6f);
        }

    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}

public class BossWeapon02 : BossWeaponBase, IWeapon
{
    public void SetEnbale(bool newEnable)
    {
       
    }

    public void SetFire()
    {
        Debug.Log("두 번쨰 보스의 무기!");
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}

public class BossWeapon03 : BossWeaponBase, IWeapon
{
    public void SetEnbale(bool newEnable)
    {

    }

    public void SetFire()
    {
        Debug.Log("세 번쨰 보스의 무기!");
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}



