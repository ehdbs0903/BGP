// PhotonLauncher.cs
using Photon.Pun;
using UnityEngine;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon 서버에 연결
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon 서버에 연결 시도 중...");
    }

    // 연결 성공 시 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon 서버에 연결되었습니다.");
    }

    // 연결 실패 시 호출되는 콜백 함수
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogWarningFormat("Photon 연결 실패: {0}", cause);
    }
}
