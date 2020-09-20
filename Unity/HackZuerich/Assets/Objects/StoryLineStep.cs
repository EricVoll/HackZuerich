using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StoryLineStep : MonoBehaviour
{
    private Action<int[]> StepFinishedCallBack;
    
    public void SetCallBack(Action<int[]> callback){
        StepFinishedCallBack = callback;
    }

    public void ReportStepFinished(int[] nextIds){
        StepFinishedCallBack?.Invoke(nextIds);
    }
}
