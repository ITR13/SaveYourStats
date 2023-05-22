using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public Sprite Icon, Image;
    public string Text, Description;
    
    public Upgrade[] Dependencies;

    public abstract void Execute(Stats mutableStats);
}
