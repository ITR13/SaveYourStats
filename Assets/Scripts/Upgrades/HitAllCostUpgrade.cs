using UnityEngine;


[CreateAssetMenu(fileName = "HitAllCost", menuName = "Upgrade/HitAllCost", order = 1)]
public class HitAllCostUpgrade : Upgrade
{
    public override void Execute(Stats mutableStats)
    {
        mutableStats.HitAllCost /= 2;
    }
}
