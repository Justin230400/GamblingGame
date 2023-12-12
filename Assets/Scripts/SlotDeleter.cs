using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDeleter : MonoBehaviour
{
    int spinsTime;
    public bool isSpin;

    private void Start()
    {
        spinsTime = 0;
        isSpin = false;
    }

    public void ChangeTimeOfSpins(int time)
    {
        spinsTime = time;
        isSpin = true;
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spinsTime--;
        Destroy(collision.gameObject);
        if (spinsTime < 1)
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
            isSpin = false;
        }
    }
}
