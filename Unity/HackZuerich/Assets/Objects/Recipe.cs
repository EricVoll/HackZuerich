using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Nutrients
{
    public int calories { get; set; }
    public int carbohydrates { get; set; }
    public int carbohydrates_percent { get; set; }
    public int fat { get; set; }
    public double fat_percent { get; set; }
    public int kilojoule { get; set; }
    public int proteins { get; set; }
    public double proteins_percent { get; set; }
}

public class Step
{
    public object title { get; set; }
    public string description { get; set; }
    public object video { get; set; }
    public object image { get; set; }
}

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
}

public class Tag
{
    public string name { get; set; }
    public string id { get; set; }
    public string type { get; set; }
}

public class Logo
{
    public string id { get; set; }
    public DateTime modification_date { get; set; }
    public string hash { get; set; }
    public string stack { get; set; }
}

public class Canonical
{
    public string master { get; set; }
    public string url { get; set; }
    public Logo logo { get; set; }
}

public class Data
{
    public string id { get; set; }
    public string language { get; set; }
    public string title { get; set; }
    public string teasertext { get; set; }
    public string slug { get; set; }
    public int duration_total_in_minutes { get; set; }
    public Nutrients nutrients { get; set; }
    public List<Step> steps { get; set; }
    public List<Ingredient> ingredients { get; set; }
    public List<Tag> tags { get; set; }
    public Canonical canonical { get; set; }
}

public class Recipe
{
    public Data data { get; set; }
}

