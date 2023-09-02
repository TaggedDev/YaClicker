using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class PlayerProfilePicture : MonoBehaviour
    {
        [SerializeField] private RawImage profilePicture;
        [SerializeField] private YandexGame yandex;

        public void HandlePictureClick()
        {
            if (YandexGame.auth)
                return;
            yandex._OpenAuthDialog();
        }

        public void SetPlayerPicture()
        {
            StartCoroutine(SetPhoto());
            Debug.Log("Finished");

            IEnumerator SetPhoto()
            {
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(YandexGame.playerPhoto);
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                    Debug.Log("Error occured downloading image");
                else
                    profilePicture.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
        }
    }
}