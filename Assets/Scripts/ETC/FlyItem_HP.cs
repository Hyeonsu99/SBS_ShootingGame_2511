using UnityEngine;

public class FlyItem_HP : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreManagerRef.IncreaseHP();
    }
}
