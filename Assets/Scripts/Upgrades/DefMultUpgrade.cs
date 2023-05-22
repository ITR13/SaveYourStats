using UnityEngine;


[CreateAssetMenu(fileName = "DefenceMult", menuName = "Upgrade/DefenceMult", order = 1)]
public class DefenceMultUpgrade : Upgrade
{
    public float Mult;
    public override void Execute(Stats mutableStats)
    {
        mutableStats.DefenceMultiplier *= Mult;
    }
}
