using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private GameObject[] flyItemPrefabs;



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

        if(Random.Range(0, 999) < 500)
        {
            int randomItem = Random.Range(0, 3);

            go = Instantiate(flyItemPrefabs[randomItem], enemyInfo.transform.position, Quaternion.identity);
        }
    }
}
