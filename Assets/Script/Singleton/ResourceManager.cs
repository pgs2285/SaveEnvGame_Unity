using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public int Health { get; private set; } = 50;
    public int Money { get; private set; } = 50;
    public int Environment { get; private set; } = 50;
    public int Cleanliness { get; private set; } = 50;
    public int Hunger { get; private set; } = 50;
    public void UpdateHealth(int amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, 100);
    }

    public void UpdateMoney(int amount)
    {
        Money = Mathf.Clamp(Money + amount, 0, 100);
    }

    public void UpdateEnvironment(int amount)
    {
        Environment = Mathf.Clamp(Environment + amount, 0, 100);
    }

    public void UpdateCleanliness(int amount)
    {
        Cleanliness = Mathf.Clamp(Cleanliness + amount, 0, 100);
    }

    public void UpdateHunger(int amount)
    {
        Hunger = Mathf.Clamp(Hunger + amount, 0, 100);
    }

//    public void Start()
//    {
//        AudioSource audio = GetComponent<AudioSource>();
//        audio.Play();
//    }
}
