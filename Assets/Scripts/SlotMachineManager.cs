using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    public List<Slot> baseSlotList;
    public GameObject slot;
    public GameObject[] genSlot;
    public GameObject[] endSlot;

    private List<int> nowSlotList;
    private List<int> nextSlotList;

    private void Start()
    {
        Init();
    }
    
    private void Update()
    {
        AutoGenSlotObject();
    }

    public void ButtonSpin()
    {
        StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        for (int i = 0; i<endSlot.Length; i++)
        {
            endSlot[i].GetComponent<SlotDeleter>().ChangeTimeOfSpins(10);
            endSlot[i].GetComponent<Collider2D>().isTrigger = true;
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void Init()
    {
        nowSlotList = GenerateNowSlotList();
        // Debug_ShowListInt(nowSlotList);
        nextSlotList = RandomizationSlotList(nowSlotList);
        // Debug_ShowListInt(nextSlotList);
    }

    void InitGenerateSlot(List<int> SlotList)
    {

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
                GenerateSlotObject(genSlot[i].transform.position.x, genSlot[i].transform.position.y, baseSlotList[nowSlotList[0]]);
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

    /// <summary>
    /// Generate Slot Object
    /// </summary>
    /// <param name="x">Slot Tranfrom Position X</param>
    /// <param name="y">Slot Tranfrom position Y</param>
    /// <param name="slotInfo">Slot Object</param>
    private void GenerateSlotObject(float x, float y, Slot slotInfo)
    {
        slot.name = slotInfo.name;
        slot.GetComponent<SpriteRenderer>().sprite = slotInfo.sprite;
        Instantiate(slot, new Vector3(x, y, 0), Quaternion.identity);
    }

    /// <summary>
    /// Debug.log for list
    /// </summary>
    /// <param name="intList"></param>
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
