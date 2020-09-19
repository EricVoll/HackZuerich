using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialRecipeViewer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region EndStoryLine

    public void ReportEndClick(){
        this.GetComponent<StoryLineStep>().ReportStepFinished(new int[]{2, 3});
    }
        
    #endregion
}
