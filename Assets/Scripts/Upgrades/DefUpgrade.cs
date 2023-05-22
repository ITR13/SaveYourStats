using UnityEngine;


[CreateAssetMenu(fileName = "Defence", menuName = "Upgrade/Defence", order = 1)]
public class DefUpgrade : Upgrade
{
    public int Defence; 
    public override void Execute(Stats mutableStats)
    {
        mutableStats.BaseDefence += Defence;
    }
}
