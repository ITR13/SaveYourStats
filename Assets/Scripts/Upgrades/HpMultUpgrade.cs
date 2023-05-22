using UnityEngine;


[CreateAssetMenu(fileName = "HealthMult", menuName = "Upgrade/HealthMult", order = 1)]
public class HealthMultUpgrade : Upgrade
{
    public float Mult;
    public override void Execute(Stats mutableStats)
    {
        mutableStats.HealthMultiplier *= Mult;
    }
}
