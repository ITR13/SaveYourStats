using UnityEngine;


[CreateAssetMenu(fileName = "Mana", menuName = "Upgrade/Mana", order = 1)]
public class MpUpgrade : Upgrade
{
    public int Mana; 
    public override void Execute(Stats mutableStats)
    {
        mutableStats.BaseMana += Mana;
    }
}
