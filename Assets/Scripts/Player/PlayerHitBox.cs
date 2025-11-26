using System;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamaged
{
    public static Action<bool> OnPlayerHPIncreased;

    public void TakeDamage(GameObject attacker, int damage)
    {
        Debug.Log($"attacker : {attacker.name} -> Hit : {transform.root.name}, damage : {damage}");

        OnPlayerHPIncreased?.Invoke(false);
    }
}
