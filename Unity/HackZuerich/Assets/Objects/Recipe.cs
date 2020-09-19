using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Name
{
    public string singular { get; set; }
    public string plural { get; set; }
}

public class Ingredient
{
    public int id { get; set; }
    public List<string> synonyms { get; set; }
    public Name name { get; set; }
    public DateTime modified { get; set; }
    public List<int> family_ids { get; set; }

    //custom ones
    public bool IsActive {get; set;}
    public double Cost {get;set;}
    public string ScoreHealth {get;set;}
    public int Rating {get;set;}
    public int kcal {get;set;}
}



