using UnityEngine;


[CreateAssetMenu(fileName = "Damage", menuName = "Upgrade/Damage", order = 1)]
public class DamageUpgrade : Upgrade
{
    public int Damage;
    public override void Execute(Stats mutableStats)
    {
        mutableStats.BaseDamage += Damage;
    }
}
