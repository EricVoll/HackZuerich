using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarImage : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        string url = "https://upload.wikimedia.org/wikipedia/commons/2/29/Gold_Star.svg";
          // Start a download of the given URL
        using (WWW www = new WWW(url))
        {
            // Wait for download to complete
            yield return www;
            Debug.Log("Setting star");

            // assign texture
            this.GetComponent<MeshRenderer>().material.mainTexture = www.texture;
        }
    }
}
