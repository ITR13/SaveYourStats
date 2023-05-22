using UnityEngine;


[CreateAssetMenu(fileName = "DamageMult", menuName = "Upgrade/DamageMult", order = 1)]
public class DamageMultUpgrade : Upgrade
{
    public float Mult;
    public override void Execute(Stats mutableStats)
    {
        mutableStats.DamageMultiplier *= Mult;
    }
}
