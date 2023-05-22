using UnityEngine;


[CreateAssetMenu(fileName = "HitRandomDmg", menuName = "Upgrade/HitRandomDmg", order = 1)]
public class HitRandomDmgUpgrade : Upgrade
{
    public override void Execute(Stats mutableStats)
    {
        mutableStats.HitRandomMultiplier *= 1.1f;
    }
}
