using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI hitCounterText;

    public int count = 0;

    private void Start()
    {
        count = 0;
        hitCounterText.text = "Hits: " + count;
    }
    private void Update()
    {
        hitCounterText.text = "Hits: " + count;
    }

   /* private void OnTriggerEnter(Collider other)
    {
        count += 1;
        hitCounterText.text = "Hits: " + count;
    }*/
}
