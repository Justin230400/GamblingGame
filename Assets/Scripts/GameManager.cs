using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private static int money;

    public static int Money { get => money;}

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        money = 1000;
    }

    public static void WinMoney(int Amount)
    {
        money += Amount;
    }

    public static void LostMoney(int Amount)
    {
        money -= Amount;
    }
}
