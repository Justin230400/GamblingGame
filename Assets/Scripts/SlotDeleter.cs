using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDeleter : MonoBehaviour
{
    int spinsTime = 50;

    public void ChangeTimeOfSpins(int time)
    {
        spinsTime = time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spinsTime--;
        Destroy(collision.gameObject);
        if (spinsTime < 1)
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
