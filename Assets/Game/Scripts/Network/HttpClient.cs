using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Config;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Scripts.Network
{
    public class HttpClient : MonoBehaviour
    {
        private GameConfig config;

        private void Start()
        {
            config = GetComponent<GameConfig>();
        }

        public void SendPost(string url, Dictionary<string, string> data)
        {
            StartCoroutine(Post(url, data));
        }

        IEnumerator Post(string url, Dictionary<string, string> data)
        {
            var form = new WWWForm();
            
            foreach (var item in data)
            {
                form.AddField(item.Key, item.Value);
            }

            using (var www = UnityWebRequest.Post(url, form))
            {
                www.SetRequestHeader("auth", config.ApiAuthKey);
                yield return www.SendWebRequest();
                
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Data sending complete!");
                }
            }
        }
    }
}
