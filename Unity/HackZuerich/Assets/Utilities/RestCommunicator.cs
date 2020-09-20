using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// The RestCommunicator handles all the REST request to the REST API and provides some untiliy functions
/// </summary>
public class RestCommunicator : MonoBehaviour
{
    /// <summary>
    /// The URL that will be used to send the requests to
    /// </summary>
    public string RestServerUrl = "https://mrshopper.azurewebsites.net/";

    public enum RequestUrl
    {
        GetRecipe,
        GetIngredient,
        GetSubstitute,
        GetProduct,
    };

    public Dictionary<RequestUrl, string> RestUrl = new Dictionary<RequestUrl, string>() {
            { RequestUrl.GetRecipe,   "https://mrshopper.azurewebsites.net/get_recipe" },
            { RequestUrl.GetIngredient,  "https://mrshopper.azurewebsites.net/get_ingredients" },
            { RequestUrl.GetSubstitute, "https://mrshopper.azurewebsites.net/get_substitute" },
            { RequestUrl.GetProduct,  "https://mrshopper.azurewebsites.net/get_product" },
        };

    public void Awake()
    {
        
    }

    /// <summary>
    /// Sends a Get-Request to the specified url without a body and calls the callback after completion
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack">The callback called after completion with the return json deserialized into <typeparamref name="T"/></param>
    /// <returns></returns>
    public void SendGetRequest<T>(string url, Action<T> callBack)
    {
        StartCoroutine(Get(url, callBack));
    }


    /// <summary>
    /// Sends a Post-Request to the specified url and calls the optional callback after completion.
    /// </summary>
    /// <param name="callBack">An optional callback called after completion with the parameter being a boolean that indicates whether or not the request succeeded</param>
    /// <param name="url">The url to send the request to</param>
    /// <param name="requestObject">An optional object that is serialized and sent as a json body with the request</param>
    /// <returns></returns>
    public void SendPostRequest(string url, Action<bool> callBack = null, object requestObject = null)
    {
        string requestUrl = url;

        if (requestObject != null)
        {
            string json = JsonConvert.SerializeObject(requestObject);
            //Send request with json body
            StartCoroutine(Post(requestUrl, json, callBack));
        }
        else
        {
            //Send request without json body
            StartCoroutine(Post(requestUrl, "", callBack));
        }
    }

    private IEnumerator Get<T>(string url, Action<T> callBack)
    {
        Debug.Log("Sending to " + url);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    T res = JsonConvert.DeserializeObject<T>(jsonResult);

                    callBack(res);
                    Debug.Log(jsonResult);
                }
            }
        }
    }

    private IEnumerator Post(string url, string payload, Action<bool> callBack = null)
    {

        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(payload);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
            Debug.LogError(string.Format("{0}: {1}", www.url, www.error));
        else
            Debug.Log(string.Format("Response: {0}", www.downloadHandler.text));

        callBack?.Invoke(!www.isNetworkError);
    }

}
