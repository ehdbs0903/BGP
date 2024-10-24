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
        statusText.text = "���� ���� ��...";

        // Photon ������ ����
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon ������ ���� �õ� ��...");
    }

    // ���� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        statusText.text = "���� ���� ����!";
        Debug.Log("Photon ������ ����Ǿ����ϴ�.");

        // delay �Ŀ� MainScene���� ��ȯ
        StartCoroutine(LoadMainSceneAfterDelay(2f));
    }

    // ���� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        statusText.text = $"���� ���� ����: {cause}";
        Debug.LogWarningFormat("Photon ���� ����: {0}", cause);
    }

    private IEnumerator LoadMainSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // delay
        SceneManager.LoadScene("MainScene"); // MainScene �ε�
    }
}
