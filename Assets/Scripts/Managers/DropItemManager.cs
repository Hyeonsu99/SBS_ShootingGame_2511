using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;



    private GameObject go;

    private void OnEnable()
    {
        Enemy.OnMonsterDied += ItemDrop;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= ItemDrop;
    }

    private void ItemDrop(Enemy enemyInfo)
    {


        for(int i = 0; i < 7; ++i)
        {
            Instantiate(gemPrefab, enemyInfo.transform.position, Quaternion.identity);
        }
    }
}
