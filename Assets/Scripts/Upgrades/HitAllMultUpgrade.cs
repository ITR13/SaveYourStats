using UnityEngine;


[CreateAssetMenu(fileName = "HitAllMultiplier", menuName = "Upgrade/HitAllMultiplier", order = 1)]
public class HitAllMultiplierUpgrade : Upgrade
{
    public float Mult; 
    public override void Execute(Stats mutableStats)
    {
        mutableStats.HitAllMultiplier += Mult;
    }
}
