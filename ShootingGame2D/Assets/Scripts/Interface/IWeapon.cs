using UnityEngine;

public interface IWeapon
{
    void SetOwner(GameObject owner);
    void SetEnable(bool newEnable);
    void SetFire();
}
