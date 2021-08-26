using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkService : MonoBehaviour
{
    // добавить APIKey
    private const string xmlAPI =
        "https://api.openweathermap.org/data/2.5/weather?q=London&appid=3de13cf7a316b8f17a438574537faa23&mode=xml";

    private const string jsonAPI =
        "https://api.openweathermap.org/data/2.5/weather?q=London&appid=3de13cf7a316b8f17a438574537faa23";

    private const string webImage=
        "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
    private bool IsResponseValid(WWW www)
    {
        if (www.error != null)
        {
            Debug.Log("Bad connection!");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("Bad data!");
            return false;
        }
        else return true;
    }

    private IEnumerator CallAPI(string url, Action<string> callback )
    {
        WWW www = new WWW(url);
        yield return www;

        if (!IsResponseValid(www))
           yield break;

        callback(www.text);
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlAPI,callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonAPI, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        WWW www = new WWW(webImage);
        yield return www;
        callback(www.texture);
    }
}
