using UnityEngine;


[CreateAssetMenu(fileName = "ManaMult", menuName = "Upgrade/ManaMult", order = 1)]
public class MpMultUpgrade : Upgrade
{
    public float Mult; 
    public override void Execute(Stats mutableStats)
    {
        mutableStats.ManaMultiplier *= Mult;
    }
}
