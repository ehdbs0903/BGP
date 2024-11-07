using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraManager : MonoBehaviourPunCallbacks
{
    // 에디터에서 할당할 카메라들
    public Camera player1Camera;
    public Camera player2Camera;

    void Start()
    {
        // 현재 플레이어의 ActorNumber을 가져옴
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        // ActorNumber에 따라 카메라 활성화
        if (actorNumber == 1)
        {
            ActivateCamera(player1Camera);
        }
        else if (actorNumber == 2)
        {
            ActivateCamera(player2Camera);
        }
        else
        {
            Debug.LogWarning("지원되지 않는 플레이어 번호입니다.");
        }
    }


    void ActivateCamera(Camera activeCamera)
    {
        // 모든 카메라 비활성화
        if (player1Camera != null) player1Camera.gameObject.SetActive(false);
        if (player2Camera != null) player2Camera.gameObject.SetActive(false);

        // 지정된 카메라 활성화
        if (activeCamera != null)
        {
            activeCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("활성화할 카메라가 할당되지 않았습니다.");
        }
    }
}
