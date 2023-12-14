using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Text moneyText;
    public Text userName;
    public GameObject getMoneyPanel;
    public GameObject loginPanel;

    private int previousAmount;

    private void Start()
    {
        if (!GameManager.GetLoginState())
        {
            loginPanel.SetActive(true);
        }
        previousAmount = 0;
        ShowMoney();
    }

    public void Login()
    {
        if(userName.text != null && userName.text != "")
        {
            GameManager.Login(userName.text);
            loginPanel.SetActive(false);
            ShowMoney();
        }
    }

    public void AddMoneyButtonSwitch()
    {
        getMoneyPanel.SetActive(!getMoneyPanel.activeSelf);
    }

    void ShowMoney()
    {
        StartCoroutine(MoneyChangeAnimation_Increase(GameManager.GetMoney()));
    }

    IEnumerator MoneyChangeAnimation_Increase(int targetAmount)
    {
        int speed = 10;
        int nowAmount = previousAmount;
        int changeAmount = (targetAmount - previousAmount) / speed;

        for (int i = 0; i < speed; i++)
        {
            nowAmount += changeAmount;
            moneyText.text = "$" + nowAmount.ToString();
            yield return new WaitForFixedUpdate();
        }
        
        moneyText.text = "$" + targetAmount.ToString();
        previousAmount = targetAmount;
        
    }

    public void GetMoney(int amount)
    {
        GameManager.GetMoney(amount);
        ShowMoney();
    }

}
