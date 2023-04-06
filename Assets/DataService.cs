using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class DataService : MonoBehaviour
{

    private const string API_URL = "https://jsonplaceholder.typicode.com/users/1";
    private Text text;
    private void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(GetDataFromAPI());
    }

    IEnumerator GetDataFromAPI()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(API_URL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                text.text = "Error: " + webRequest.error;
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;
                // text.text = jsonResult;

                User user = JsonUtility.FromJson<User>(jsonResult);

                text.text = " user.name: " + user.name + " \nuser.email: " + user.email + " \nuser.id: " + user.id ;

            }

        }
    }
}
