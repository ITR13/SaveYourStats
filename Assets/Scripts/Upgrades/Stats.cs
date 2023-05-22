using UnityEngine;

public class Stats
{
    public int Damage => Mathf.FloorToInt(BaseDamage * DamageMultiplier);
    public int MaxHp => Mathf.FloorToInt(BaseHealth * HealthMultiplier);
    public int MaxMp => Mathf.FloorToInt(BaseMana * ManaMultiplier);
    public int Defence => Mathf.FloorToInt(BaseDefence * DefenceMultiplier);

    public int BaseDamage = 1;
    public float DamageMultiplier = 1;

    public int BaseHealth = 1;
    public float HealthMultiplier = 1;

    public int BaseMana = 1;
    public float ManaMultiplier = 1;

    public int BaseDefence = 0;
    public float DefenceMultiplier = 1;

    public int EnemyCount = 5;
    public int Difficulty = 5;

    public float HitAllMultiplier = 0.5f;
    public int HitAllCost = 5*2*2*2*2;

    public float HitRandomMultiplier = 1;
    public int HitRandomCount = 2;

    public float HitSingleMultiplier = 1.5f;
}