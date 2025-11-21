using UnityEngine;

public interface IWeapon
{
    void SetOwner(GameObject owner);    // 소유자 세팅
    void SetEnbale(bool newEnable);     // 활성화 제어
    void SetFire();                     // 발사
}
