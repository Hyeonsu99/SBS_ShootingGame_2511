using UnityEngine;

public class Explosion : MonoBehaviour
{
    // 애니메이션 실행 함수입니다. Trigger로 작동시킵니다
    public void Set()
    {
        GetComponent<Animator>().SetTrigger("Explosion");
    }
    
    
    // 애니메이션 이벤트 함수입니다.
    // 애니메이션이 끝나면 풀에 오브젝트를 다시 넣음요.
    public void explosion()
    {
        EffectManager.instance.ReturnObjectToPool(gameObject);
    }
}
