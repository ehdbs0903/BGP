// PhotonLauncher.cs
using Photon.Pun;
using UnityEngine;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon ������ ����
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon ������ ���� �õ� ��...");
    }

    // ���� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon ������ ����Ǿ����ϴ�.");
    }

    // ���� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogWarningFormat("Photon ���� ����: {0}", cause);
    }
}
