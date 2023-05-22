using UnityEngine;


[CreateAssetMenu(fileName = "Difficulty", menuName = "Upgrade/Difficulty", order = 1)]
public class DifficultyUpgrade : Upgrade
{
    public override void Execute(Stats mutableStats)
    {
        mutableStats.Difficulty--;
    }
}
