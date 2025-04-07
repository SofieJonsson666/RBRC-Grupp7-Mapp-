using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] background;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = -30; i < 30; i += 3)
        {
            if (i % 2 == 0)
            {
                Instantiate(background[0], new Vector3(i, 0, 0), Quaternion.Euler(0, 180, 0));
            }
            else
            {
                Instantiate(background[1], new Vector3(i, 0, 0), Quaternion.Euler(0, 180, 0));
            }
        }
    }
}