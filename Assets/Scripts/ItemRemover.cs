using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemover : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Funkar inte >:(
        print("hej");
        Destroy(collision.gameObject);
    }
}
