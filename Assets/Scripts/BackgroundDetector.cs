using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundDetector : MonoBehaviour
{
    private Vector3 location = new Vector3(28.5f, 0, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Background"))
        {
            int amount = 0;
            float highestX = 0;

            GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
            Object[] lista = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject obj in gameObjects)
            {
                if (obj.CompareTag("Background"))
                {
                    amount += 1;
                    if (highestX < obj.transform.position.x)
                    {
                        highestX = obj.transform.position.x;
                    }
                }
            }
            location = new Vector3(highestX + 3, 0, 0);
            //Debug.Log("Entered");
            other.transform.position = location;
        }
    }
}