using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    public bool canGenerate;

    private void Start()
    {
        canGenerate = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Check")
            return;

        canGenerate = true;
    }


}
