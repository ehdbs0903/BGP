using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraManager : MonoBehaviourPunCallbacks
{
    // �����Ϳ��� �Ҵ��� ī�޶��
    public Camera player1Camera;
    public Camera player2Camera;

    void Start()
    {
        // ���� �÷��̾��� ActorNumber�� ������
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        // ActorNumber�� ���� ī�޶� Ȱ��ȭ
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
            Debug.LogWarning("�������� �ʴ� �÷��̾� ��ȣ�Դϴ�.");
        }
    }


    void ActivateCamera(Camera activeCamera)
    {
        // ��� ī�޶� ��Ȱ��ȭ
        if (player1Camera != null) player1Camera.gameObject.SetActive(false);
        if (player2Camera != null) player2Camera.gameObject.SetActive(false);

        // ������ ī�޶� Ȱ��ȭ
        if (activeCamera != null)
        {
            activeCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Ȱ��ȭ�� ī�޶� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}
