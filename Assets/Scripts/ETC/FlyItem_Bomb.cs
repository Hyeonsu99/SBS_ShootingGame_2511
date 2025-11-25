using UnityEngine;

public class FlyItem_Bomb : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreManagerRef.IncreaseBomb();
    }
}
