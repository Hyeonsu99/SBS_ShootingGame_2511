using UnityEngine;

public interface IDamaged 
{
    void TakeDamage(GameObject attacker, int damage);       // 대미지 스크립트(공격자와 데미지 추적)
}
