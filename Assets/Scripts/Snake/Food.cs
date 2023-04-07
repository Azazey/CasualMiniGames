using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collider.GetComponent<Snake>().GrowTail();
            collider.GetComponent<Snake>().OnSnackEaten();
            gameObject.SetActive(false);
        }
    }
}
