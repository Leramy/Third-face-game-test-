using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    public int health { get; private set; }

    public int money { get; private set; }
    public int maxHealth { get; private set; }

    public string ID = "10023456";
    public void Startup(NetworkService service)
    {
        Debug.Log("Player manager starting...");
        _network = service;

        UpdateData(50, 100);

        if (!PlayerPrefs.HasKey("money") && !PlayerPrefs.HasKey("money"))
        {
            money = 0;
            SafePrefs.Create("money", 0);
        }
        else
        {
            if (!SafePrefs.IsEdited("money"))
                money = PlayerPrefs.GetInt("money");
            else
            {
                Debug.Log("CHEATED!!!");
                money = 0;
                SafePrefs.Create("money", money);
                BasicUI.cheat = true;
            }
        }
        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int value)
    {
        health += value;
        if (health > maxHealth)
            health = maxHealth;
        else if (health < 0)
            health = 0;

        if (health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    public void UpdateData(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }
    public void ChangeMoney(int value)
    {
        if (!SafePrefs.IsEdited("money"))
        {
            money += value;
            if (money < 0)
                money = 0;
            SafePrefs.Create("money", money);
        }
        else
        {
            Debug.Log("CHEATED!!!");
            money = 0;
            SafePrefs.Create("money", money);
            BasicUI.cheat = true;
        }

    }

    public void Respawn()
    {
        UpdateData(50, 100);
    }

}
