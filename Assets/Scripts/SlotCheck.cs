using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.name = collision.gameObject.name.Substring(0,1);
    }
}
