using UnityEngine;


[CreateAssetMenu(fileName = "Health", menuName = "Upgrade/Health", order = 1)]
public class HpUpgrade : Upgrade
{
    public int Health;
    public override void Execute(Stats mutableStats)
    {
        mutableStats.BaseHealth += Health;
    }
}
