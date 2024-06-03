using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceManager : Singleton<ResourceManager>
{
    public int Health { get; private set; } = 50;
    public int Money { get; private set; } = 50;
    public int Environment { get; private set; } = 50;
    public int Cleanliness { get; private set; } = 50;
    public int Hunger { get; private set; } = 50;
    public void UpdateHealth(int amount)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "EnvSurvey")
        {
            Health = Mathf.Clamp(Health + amount, 10, 100);
            return;
        }    
        Health = Mathf.Clamp(Health + amount, 0, 100);
    }

    public void UpdateMoney(int amount)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "EnvSurvey")
        {
            Money = Mathf.Clamp(Money + amount, 10, 100);
            return;
        }
        Money = Mathf.Clamp(Money + amount, 0, 100);
    }

    public void UpdateEnvironment(int amount)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "EnvSurvey")
        {
            Environment = Mathf.Clamp(Environment + amount, 10, 100);
            return;
        }
        Environment = Mathf.Clamp(Environment + amount, 0, 100);
    }

    public void UpdateCleanliness(int amount)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "EnvSurvey")
        {
            Cleanliness = Mathf.Clamp(Cleanliness + amount, 10, 100);
            return;
        }
        Cleanliness = Mathf.Clamp(Cleanliness + amount, 0, 100);
    }

    public void UpdateHunger(int amount) {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "EnvSurvey")
        {
            Hunger = Mathf.Clamp(Hunger + amount, 10, 100);
            return;
        }

        Hunger = Mathf.Clamp(Hunger + amount, 0, 100);
    }

}
