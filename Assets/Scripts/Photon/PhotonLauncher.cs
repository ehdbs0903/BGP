// PhotonLauncher.cs
using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI statusText;

    void Start()
    {
        statusText.text = "서버 연결 중...";

        // Photon 서버에 연결
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon 서버에 연결 시도 중...");
    }

    // 연결 성공 시 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        statusText.text = "서버 연결 성공!";
        Debug.Log("Photon 서버에 연결되었습니다.");

        // delay 후에 MainScene으로 전환
        StartCoroutine(LoadMainSceneAfterDelay(2f));
    }

    // 연결 실패 시 호출되는 콜백 함수
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        statusText.text = $"서버 연결 실패: {cause}";
        Debug.LogWarningFormat("Photon 연결 실패: {0}", cause);
    }

    private IEnumerator LoadMainSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // delay
        SceneManager.LoadScene("MainScene"); // MainScene 로드
    }
}
