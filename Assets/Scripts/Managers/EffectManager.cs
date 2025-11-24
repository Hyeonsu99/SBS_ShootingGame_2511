using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectManager : Singleton<EffectManager>   
{
    // 폭발하는 프리팹입니다. 애니메이션을 가지고 있어요
    [SerializeField] private GameObject explosionPrefab;
    // 폭발하는 애니메이션을 가진 프리팹을 담는 큐입니다.
    Queue<GameObject> explosion;

    private void OnEnable()
    {
        // 적이 죽을 때 이벤트를 구독합니다.
        Enemy.OnMonsterDied += SetExplosion;
    }

    private void OnDisable()
    {
        // 게임이 꺼지면 이벤트를 구독 취소합니다.
        Enemy.OnMonsterDied -= SetExplosion;
    }

    private void Start()
    {
        // 큐를 초기화 해줍니다.
        explosion = new Queue<GameObject>();
        // 일단 풀에 폭발 오브젝트를 10개 추가합니다.
        Allocate();
    }

    private void Allocate()
    {
        GameObject go;
        for(int i = 0; i < 10; ++i)
        {
            // 오브젝트 생성
            go = Instantiate(explosionPrefab);
            // 큐에 삽입
            explosion.Enqueue(go);
            // 오브젝트를 비활성화해서 눈에 안보이게
            go.SetActive(false);
        }
    }

    private void SetExplosion(Enemy enemyInfo)
    {
        // 풀에서 오브젝트를 하나 꺼내옵니다.
        GameObject go = GetExplosionFromPool();

        // 그 오브젝트를 잘 가져왔다면
        if(go != null)
        {
            // 죽은 적의 위치로 오브젝트를 이동시키고
            go.transform.position = enemyInfo.transform.position;
            // 화면에 나타내줍니다.
            go.SetActive(true);

            if (go.TryGetComponent<Explosion>(out Explosion ex))
            {
                // 폭발하는 오브젝트에 있는 애니메이션 실행 함수를 실행합니다.
                ex.Set();
            }
        }
    }

    // 오브젝트를 큐에서 가져옵니다
    private GameObject GetExplosionFromPool()
    {
        // 만약 큐에 오브젝트가 없다면 10개를 추가로 생성합니다
        if(explosion.Count < 1)
            Allocate();

        // 큐 맨위에 있는 오브젝트를 꺼냅니다
        return explosion.Dequeue();
    }

    // 풀에 오브젝트를 되돌립니다
    public void ReturnObjectToPool(GameObject obj)
    {
        // 오브젝트를 비활성화하고
        obj.SetActive(false);
        // 큐에 다시 넣습니다.
        explosion.Enqueue(obj); 
    }
}
