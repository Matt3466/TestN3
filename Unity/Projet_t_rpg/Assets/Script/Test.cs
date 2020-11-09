using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    HealthBar BarreDevie = new HealthBar();
    void Start()
    {
        BarreDevie.max = 100;
        BarreDevie.valeur = 100;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            BarreDevie.valeur -= 10;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            BarreDevie.valeur += 10;
        }
    }
}
