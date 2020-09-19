using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLineHandler : MonoBehaviour
{
    public List<GameObject> Prefabs;
    public int StartStep;

    List<GameObject> LivingGameObjects = new List<GameObject>();

    public void Awake()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(SlightDelay());
    }

    IEnumerator SlightDelay()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        LaunchStep(StartStep);
    }

    private void LaunchStep(int step)
    {
        for (int i = LivingGameObjects.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(LivingGameObjects[i]);
        }

        if (Prefabs.Count > step)
        {
            GameObject newGameObject = Instantiate(Prefabs[step]);
            LivingGameObjects.Add(newGameObject);
            if (newGameObject != null)
            {
                newGameObject.GetComponent<StoryLineStep>().SetCallBack(LaunchStep);
            }
            else
            {
                Debug.LogError($"Instantiating new step with id {step} failed. Prefab instantiated was null");
            }
        }
        else
        {
            Debug.LogError("Requested Step was too high. Index out of range");
        }
    }
}
