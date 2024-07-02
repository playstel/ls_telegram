using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace scr
{
    public class WebRequests<T> : MonoBehaviour
    {
        public async Task<UnityWebRequest> PostRequest(string URL, T type, string URLParam = "", string JSON = "", string Token = "")
        {
            Debug.Log("URL: " + URL + " | type: " + type + " | URLParam: " + URLParam);
            var fullUrl = FullURL(URL, type, URLParam);
            Debug.Log("POST: " + fullUrl + " | JSON: " + JSON);
            
            var uwr = new UnityWebRequest(fullUrl, "POST");

            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            if (JSON != null)
                SendOneJSON(JSON, ref uwr);
            
            if (JSON != null)
                SendOneToken(Token, ref uwr);

            await uwr.Send();

            if (uwr.error != null)
            {
                Debug.LogError($"Error {uwr.error}");
                return uwr;
            }
            
            if (uwr.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"Error Connection {uwr.error}");
                return null;
            }
            
            return uwr;
        }

        public async Task<UnityWebRequest> GetRequest(string URL, T type, string URLParam = "", string Token = "", bool ActiveGlobalLoading = true)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(FullURL(URL, type, URLParam));

            webRequest.SetRequestHeader("Authorization", $"Bearer {Token}");
            Debug.Log("GET: " + FullURL(URL, type, URLParam));
    
            await webRequest.SendWebRequest();

            return webRequest;
        }
        
        public string FullURL(string URL, T type, string URLParam = "") => URL + "Account/" +  type + URLParam;
        
        private void SendOneJSON(string JSON, ref UnityWebRequest request)
        {
            byte[] jsonToSend = new UTF8Encoding().GetBytes(JSON);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.SetRequestHeader("Content-Type", "application/json");
        }
        
        private void SendOneToken(string Token, ref UnityWebRequest request)
        {
            request.SetRequestHeader("Authorization", $"Bearer {Token}");
        }
    }
}