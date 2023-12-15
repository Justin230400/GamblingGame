using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject slot;
    public List<Slot> baseSlotList;
    public GameObject[] genSlot;
    public GameObject[] endSlot;
    public GameObject[] checkSlot;
    public GameObject[] prizeLine;

    [Header("Spin")]
    public int spinTime;
    public float spinTimeInterval;
    public GameObject spinButtonObject;

    [Header("UI")]
    public int maxBet;
    public Text moneyText;
    public Text betText;
    public GameObject loadingScene;
    public Text bet0;
    public Slider slider;
    public Text text;


    private readonly int x = 5;
    private readonly int y = 3;

    private int bet;
    private int betAmount;
    private int earnMoney;
    private int previousAmount;

    private List<int> nowSlotList;
    private List<int> nextSlotList;
    private List<List<GameObject>> checkSlotList;
    private List<GameObject> prizeLineList;
    private List<GameObject> animationList;
    private List<List<GameObject>> rewardsLineList;

    private Button spinButton;

    IEnumerator WhenSceneLoading()
    {
        slider.value = 0.95f;
        text.text = "95%";
        yield return new WaitForSeconds(0.2f);
        slider.value = 0.96f;
        text.text = "96%";
        yield return new WaitForSeconds(0.2f);
        slider.value = 0.97f;
        text.text = "97%";
        yield return new WaitForSeconds(0.2f);
        slider.value = 0.98f;
        text.text = "98%";
        yield return new WaitForSeconds(0.2f);
        slider.value = 0.99f;
        text.text = "99%";
        yield return new WaitForSeconds(0.2f);
        slider.value = 1;
        text.text = "100%";
        loadingScene.SetActive(false);
        ShowMoney();
    }

    private void Awake()
    {
        nowSlotList = GenerateNowSlotList();
        // Debug_ShowListInt(nowSlotList);
        nextSlotList = RandomizationSlotList(nowSlotList);
        // Debug_ShowListInt(nextSlotList);
        spinButton = spinButtonObject.GetComponent<Button>();
        InitCheckSlotList();
        animationList = new List<GameObject>();
        prizeLineList = new List<GameObject>();
        rewardsLineList = InitCreateLine();
        earnMoney = 0;
        StartCoroutine(WhenSceneLoading());
    }

    List<List<GameObject>> InitCreateLine()
    {
        List<List<GameObject>> finalList = new List<List<GameObject>>();
        List<GameObject> tempList = new List<GameObject>();

        tempList.Add(checkSlotList[0][0]);
        tempList.Add(checkSlotList[0][1]);
        tempList.Add(checkSlotList[0][2]);
        tempList.Add(checkSlotList[0][3]);
        tempList.Add(checkSlotList[0][4]);

        finalList.Add(tempList);

        tempList = new List<GameObject>();
        tempList.Add(checkSlotList[1][0]);
        tempList.Add(checkSlotList[1][1]);
        tempList.Add(checkSlotList[1][2]);
        tempList.Add(checkSlotList[1][3]);
        tempList.Add(checkSlotList[1][4]);

        finalList.Add(tempList);

        tempList = new List<GameObject>();
        tempList.Add(checkSlotList[2][0]);
        tempList.Add(checkSlotList[2][1]);
        tempList.Add(checkSlotList[2][2]);
        tempList.Add(checkSlotList[2][3]);
        tempList.Add(checkSlotList[2][4]);

        finalList.Add(tempList);

        tempList = new List<GameObject>();
        tempList.Add(checkSlotList[0][0]);
        tempList.Add(checkSlotList[0][1]);
        tempList.Add(checkSlotList[1][2]);
        tempList.Add(checkSlotList[2][3]);
        tempList.Add(checkSlotList[2][4]);

        finalList.Add(tempList);

        tempList = new List<GameObject>();
        tempList.Add(checkSlotList[2][0]);
        tempList.Add(checkSlotList[2][1]);
        tempList.Add(checkSlotList[1][2]);
        tempList.Add(checkSlotList[0][3]);
        tempList.Add(checkSlotList[0][4]);

        finalList.Add(tempList);

        tempList = new List<GameObject>();
        tempList.Add(checkSlotList[2][0]);
        tempList.Add(checkSlotList[1][1]);
        tempList.Add(checkSlotList[0][2]);
        tempList.Add(checkSlotList[1][3]);
        tempList.Add(checkSlotList[2][4]);

        finalList.Add(tempList);

        tempList = new List<GameObject>();
        tempList.Add(checkSlotList[0][0]);
        tempList.Add(checkSlotList[1][1]);
        tempList.Add(checkSlotList[2][2]);
        tempList.Add(checkSlotList[1][3]);
        tempList.Add(checkSlotList[0][4]);

        finalList.Add(tempList);

        return finalList;
    }

    void InitCheckSlotList()
    {
        checkSlotList = new List<List<GameObject>>();
        for(int i = 0; i < y; i++)
        {
            List<GameObject> tempList = new List<GameObject>();
            for(int j = 0; j < x; j++)
            {
                tempList.Add(checkSlot[i * x + j]);
            }
            checkSlotList.Add(tempList);
        }
    }

    private void Start()
    {
        previousAmount = 0;
        ShowText(moneyText, GameManager.GetMoney().ToString());
        bet = 100;
        betAmount = 100;
        BetCheck();
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
        if (bet > GameManager.GetMoney())
        {
            bet = GameManager.GetMoney();
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
            moneyText.text = nowAmount.ToString();
            yield return new WaitForFixedUpdate();
        }

        moneyText.text = targetAmount.ToString();
        previousAmount = targetAmount;

    }

    void ShowText(Text t, string s)
    {
        t.text = s;
    }

    public void ButtonSpin()
    {
        if(bet < 1)
        {
            StartCoroutine(ShowBet0());
            return;
        }

        spinButton.interactable = false;

        GameManager.LostMoney(bet);
        // ShowText(moneyText, GameManager.GetMoney().ToString());
        ShowMoney();
        ResetMachineState();

        StartCoroutine(Spin());
        StartCoroutine(ButtonStateCheck());
    }

    IEnumerator ShowBet0()
    {
        bet0.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        bet0.gameObject.SetActive(false);
    }

    void ResetMachineState()
    {
        for (int i = 0; i < prizeLineList.Count; i++)
        {
            prizeLineList[i].SetActive(false);
        }
        prizeLineList.Clear();
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
                yield return new WaitForFixedUpdate();
            }
        }
        yield return new WaitForSeconds(1);

        WinMoneyCheck();
        spinButton.interactable = true;
        BetCheck();
    }

    void WinMoneyCheck()
    {
        for(int i = 0; i < rewardsLineList.Count; i++)
        {
            CheckRewards(rewardsLineList[i], i);
        }
        
        AnimationPlay();
        ShowPrizeLine();
        EarnRewards();
    }

    void CheckRewards(List<GameObject> line, int lineIndex)
    {
        int count;
        bool[] checkBox = new bool[line.Count];
        int index;
        List<GameObject> tempList = new List<GameObject>();

        for(int i = 0; i < checkBox.Length; i++)
        {
            checkBox[i] = false;
        }

        for(int i = 0; i < line.Count; i++)
        {
            if (checkBox[i])
                continue;

            checkBox[i] = true;
            index = int.Parse(line[i].name);
            count = 1;

            // Debug.Log(line.Count);
            for (int j = i + 1; j < line.Count; j++)
            {
                if (checkBox[j])
                    continue;

                if(line[i].name == line[j].name)
                {
                    if(!tempList.Contains(line[i]))
                        tempList.Add(line[i]);

                    tempList.Add(line[j]);
                    count++;
                    checkBox[j] = true;
                }
            }

            // Debug.Log(count);
            if (count > 2)
            {
                /*
                Debug.Log(lineIndex);
                Debug.Log(count);
                Debug.Log(tempList.Count);
                */

                foreach(GameObject anim in tempList)
                {
                    if (animationList.Contains(anim))
                        continue;

                    animationList.Add(anim);
                }
                
                earnMoney += baseSlotList[index].bettingOdds * (count - 1) * bet;
                
                if (!prizeLineList.Contains(prizeLine[lineIndex]))
                    prizeLineList.Add(prizeLine[lineIndex]);
                
            }
            
            tempList.Clear();
        }

    }

    void EarnRewards()
    {
        GameManager.WinMoney(earnMoney);
        // ShowText(moneyText, GameManager.GetMoney().ToString());
        ShowMoney();
        earnMoney = 0;
    }
    
    void AnimationPlay()
    {
        for (int i = 0; i < animationList.Count; i++)
        {
            animationList[i].name = "Played";
        }

        animationList.Clear();
    }

    void ShowPrizeLine()
    {
        for (int i = 0; i < prizeLineList.Count; i++)
        {
            prizeLineList[i].SetActive(true);
        }
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

    /*
    void Debug_ShowListInt(List<int> intList)
    {
        string s = "";
        foreach(int i in intList)
        {
            s += i + ", ";
        }
        Debug.Log(s);
    }
    */
}
