using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    public bool canGenerate = true;

    private void OnTriggerExit2D(Collider2D collision)
    {
        canGenerate = true;
    }


}
