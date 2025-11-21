using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] private GameObject[] projectilePrefabs;
    private static Queue<Projectile>[] projectiles;
    private int poolSize = 10;

    // stage 1 : p1p2p3 b1
    // stage 2 : p1p2p3 b2
    protected override void DoAwake()
    {
        base.DoAwake();

        projectiles = new Queue<Projectile>[projectilePrefabs.Length];

        for(int i = 0; i < projectiles.Length; ++i)
        {
            projectiles[i] = new Queue<Projectile>();

            Allocate((Projectile.Type)i);
        }        
    }

    private void Allocate(Projectile.Type type)
    {
        GameObject go;

        for(int i = 0; i < poolSize; ++i)
        {
            go = Instantiate(projectilePrefabs[(int)type]);

            if(go.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectiles[(int)type].Enqueue(projectile); //  큐<스크립트 객체 기준>에 추가
            }
            go.SetActive(false);
        }
    }

    public void FireProjectile(Projectile.Type type, Vector3 spawnPos,Vector2 direction,GameObject owner,int damage, float speed)
    {
        Projectile proj = GetProjectileFromPool(type);

        if (proj != null)
        {
            proj.transform.position = spawnPos;
            proj.gameObject.SetActive(true);
            proj.InitProjectile(type, direction, owner, damage, speed);
        }     
    }

    private Projectile GetProjectileFromPool(Projectile.Type type)
    {
        if (projectiles[(int)type].Count <= 1)
        {
            Allocate(type);
        }

        return projectiles[(int)type].Dequeue();
    }

    public void ReturnProjectileToPool(Projectile returnProj, Projectile.Type type)
    {
        returnProj.gameObject.SetActive(false);
        projectiles[(int)type].Enqueue(returnProj);
    }
}
