using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileScripts : MonoBehaviour
{
    public static ProfileScripts instance;

    // Start is called before the first frame update
    void Start()
    {
        LastPos = CamTransform.position;
        instance = this;
    }

    public Transform CamTransform;

    double totalDistance = 0;
    Vector3 LastPos;


    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        if(counter++ > 20){
            double delta = Vector3.Magnitude(CamTransform.position - LastPos);
            totalDistance += delta;
            LastPos = CamTransform.position;
            counter = 0;
            Debug.Log($"Updated distance: {totalDistance}");
            StepText.text = (1253 + (int)(totalDistance/0.7)).ToString();
        }
    }

    public TextMeshPro StepText;
    public TextMeshPro CalText;

    public void AddCal(int kcal){
        CalText.text = (500 + kcal).ToString() + " kcal";
    }
}
