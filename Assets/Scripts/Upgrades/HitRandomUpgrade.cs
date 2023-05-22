using UnityEngine;


[CreateAssetMenu(fileName = "HitRandom", menuName = "Upgrade/HitRandom", order = 1)]
public class HitRandomUpgrade : Upgrade
{
    public override void Execute(Stats mutableStats)
    {
        mutableStats.HitRandomCount++;
    }
}
