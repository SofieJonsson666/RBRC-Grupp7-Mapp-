using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;

    // Start is called before the first frame update
    private void Awake()
    {
        highScore.text = DataSaver.instance.highScore.ToString();
    }
}