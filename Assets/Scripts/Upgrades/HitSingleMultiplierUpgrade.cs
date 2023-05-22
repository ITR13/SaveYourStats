using UnityEngine;


[CreateAssetMenu(fileName = "HitSingleMultiplier", menuName = "Upgrade/HitSingleMultiplier", order = 1)]
public class HitSingleMultiplier : Upgrade
{
    public override void Execute(Stats mutableStats)
    {
        mutableStats.HitSingleMultiplier += 0.5f;
    }
}
