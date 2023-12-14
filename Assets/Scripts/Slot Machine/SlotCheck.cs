using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Check") 
            return;

        this.name = col.transform.parent.name.Substring(0,1);
    }
}
