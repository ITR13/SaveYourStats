using UnityEngine;


[CreateAssetMenu(fileName = "Count", menuName = "Upgrade/EnemyCount", order = 1)]
public class EnemyCountUpgrade : Upgrade
{
    public override void Execute(Stats mutableStats)
    {
        mutableStats.EnemyCount--;
    }
}
