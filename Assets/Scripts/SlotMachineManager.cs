using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    [Header("Slot")]
    public GameObject slot;
    public List<Slot> baseSlotList;
    public GameObject[] genSlot;
    public GameObject[] endSlot;
    public GameObject[] checkSlot;

    [Header("Spin")]
    public int spinTime;
    public float spinTimeInterval;
    public GameObject spinButtonObject;

    [Header("UI")]
    public int maxBet;
    public Text moneyText;
    public Text betText;

    private int money;
    private int bet;
    private int betAmount;

    private List<int> nowSlotList;
    private List<int> nextSlotList;
    private GameObject[][] checkSlotArray;

    private Button spinButton;

    private void Awake()
    {
        nowSlotList = GenerateNowSlotList();
        // Debug_ShowListInt(nowSlotList);
        nextSlotList = RandomizationSlotList(nowSlotList);
        // Debug_ShowListInt(nextSlotList);
        spinButton = spinButtonObject.GetComponent<Button>();
        InitCheckSlotArray();
    }

    void InitCheckSlotArray()
    {
        checkSlotArray = new GameObject[][] { new GameObject[3], new GameObject[5]};
        for(int i = 0; i < checkSlotArray.Length; i++)
        {
            for(int j = 0; j < checkSlotArray[i].Length; j++)
            {

            }
        }
    }

    private void Start()
    {
        money = GameManager.Money;
        ShowText(moneyText, "$" + money.ToString());
        bet = 100;
        betAmount = 100;
        ShowText(betText, "Total Bet\n" + bet);
    }

    private void Update()
    {
        AutoGenSlotObject();
    }

    public void SetBetAmount(int betAmount)
    {
        this.betAmount = betAmount;
    }

    public void IncreaseBet()
    {
        bet += betAmount;
        BetCheck();
    }

    public void DecreaseBet()
    {
        bet -= betAmount;
        BetCheck();
    }

    public void MaxinumBet()
    {
        bet = maxBet;
        BetCheck();
    }

    void BetCheck()
    {
        if (bet > money)
        {
            bet = money;
        }
        else if (bet < 0)
        {
            bet = 0;
        }
        
        if(bet > maxBet)
        {
            bet = maxBet;
        }

        ShowText(betText, "Total Bet\n" + bet.ToString());
    }


    void ShowText(Text t, string s)
    {
        t.text = s;
    }

    public void ButtonSpin()
    {
        spinButton.interactable = false;

        money -= bet;
        ShowText(moneyText, "$" + money.ToString());

        StartCoroutine(Spin());
        StartCoroutine(ButtonStateCheck());
    }

    IEnumerator Spin()
    {
        for (int i = 0; i < endSlot.Length; i++)
        {
            endSlot[i].GetComponent<SlotDeleter>().ChangeTimeOfSpins(spinTime);
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator ButtonStateCheck()
    {
        for (int i = 0; i < endSlot.Length; i++)
        {
            while (endSlot[i].GetComponent<SlotDeleter>().isSpin)
            {
                yield return new WaitForSeconds(1);
            }
        }
        
        // winMoney
        spinButton.interactable = true;
        BetCheck();
    }

    void AutoGenSlotObject()
    {
        for(int i = 0; i < genSlot.Length; i++)
        {
            if (nowSlotList.Count < 1)
            {
                nowSlotList.AddRange(nextSlotList);
                nextSlotList = RandomizationSlotList(nowSlotList);
                // Debug.Log("Randomization Slot List");
            }

            if (genSlot[i].GetComponent<SlotGenerator>().canGenerate == true)
            {
                GenerateSlotObject(genSlot[i].transform.position.x, genSlot[i].transform.position.y, nowSlotList[0]);
                nowSlotList.RemoveAt(0);
                genSlot[i].GetComponent<SlotGenerator>().canGenerate = false;
            }
        }
    }

    private List<int> GenerateNowSlotList()
    {
        List<int> tempList = new List<int>();

        if (baseSlotList.Count == 0)
        {
            Debug.Log("Error: There are no slots in the list");
        }

        for(int i = 0; i < baseSlotList.Count; i++)
        {
            for (int j = 0; j < baseSlotList[i].Weights; j++)
            {
                tempList.Add(i);
            }
            
        }

        return RandomizationSlotList(tempList);
    }

    private List<int> RandomizationSlotList(List<int> orgSlotList)
    {
        List<int> orgList_t = new List<int>();
        orgList_t.AddRange(orgSlotList);

        List<int> list_t = new List<int>();
        
        int rnd;
        while (orgList_t.Count > 0)
        {
            rnd = Random.Range(0, orgList_t.Count);
            list_t.Add(orgList_t[rnd]);
            orgList_t.RemoveAt(rnd);
        }

        return list_t;
    }

    private void GenerateSlotObject(float x, float y,int index)
    {
        slot.name = index.ToString();
        slot.GetComponent<SpriteRenderer>().sprite = baseSlotList[index].sprite;
        Instantiate(slot, new Vector3(x, y, 0), Quaternion.identity);
    }

    void Debug_ShowListInt(List<int> intList)
    {
        string s = "";
        foreach(int i in intList)
        {
            s += i + ", ";
        }
        Debug.Log(s);
    }
}
