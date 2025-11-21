using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] private GameObject[] projectilePrefab;
    private static Queue<Projectile>[] projectiles;
    private int poolSize = 32;

    protected override void OnAwake()
    {
        projectiles = new Queue<Projectile>[projectilePrefab.Length];
        for (int i = 0; i < projectilePrefab.Length; ++i)
        {
            projectiles[i] = new Queue<Projectile>();
            Allocate((Projectile.Type)i);
        }
    }

    // 스테이지마다 등장시킬 종류가 다를 경우에는 수정이 필요한 코드
    private void Allocate(Projectile.Type type)
    {
        GameObject go;
        for(int i = 0; i < poolSize; ++i)
        {
            go = Instantiate(projectilePrefab[(int)type]);
            if (go.TryGetComponent<Projectile>(out Projectile projectile))
                projectiles[(int)type].Enqueue(projectile); // 큐<스크립트 객체 기준>에 추가
            go.SetActive(false);
        }
    }

    public void FireProjectile(Projectile.Type type, Vector3 spawnPos, Vector2 dir, GameObject owner, int dmg, float speed)
    {
        Projectile proj = GetProjectileFromPool(type);
        if (proj != null)
        {
            proj.transform.position = spawnPos;
            proj.gameObject.SetActive(true);
            proj.Initialize(type, dir, owner, dmg, speed);
        }
    }

    private Projectile GetProjectileFromPool(Projectile.Type type)
    {
        if (projectiles[(int)type].Count < 1)
            Allocate(type);
        return projectiles[(int)type].Dequeue();
    }

    public void ReturnProjectileToPool(Projectile returnProj, Projectile.Type type)
    {
        returnProj.gameObject.SetActive(false);
        projectiles[(int)type].Enqueue(returnProj);
    }
}
