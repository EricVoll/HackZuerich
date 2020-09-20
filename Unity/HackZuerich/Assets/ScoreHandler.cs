using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System.Linq;
using System;

public class ScoreHandler : MonoBehaviour
{
    public TextMeshPro[] Texts;

    public void Awake()
    {
        float sec = Time.realtimeSinceStartup;
        //double h = Math.Round(CheckoutCart.instance.Items.Select(x => map[x.ScoreHealth]).Average(), 1);
        double h = 4.3;
        Texts[0].text = ((int)(Time.realtimeSinceStartup / 60)).ToString() + "m " + ((int)Time.realtimeSinceStartup % 60).ToString() + "s";
        Texts[1].text = h + "/5";
        Texts[2].text = "3,5/5";
        Texts[3].text = sec.ToString();
        Texts[4].text = h + " * 30 = " + h * 30;
        Texts[5].text = "3.5 * 30 = 105";
        Texts[6].text = (sec + h * 30 + 3.5 * 30).ToString() + " Points";
    }

    Dictionary<string, int> map = new Dictionary<string, int>(){
        {"A",5},
      {"B",4},
      {"C",3},
      {"D",2},
      {"E",1},
      {"F",0},
    };

}
