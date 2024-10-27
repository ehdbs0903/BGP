using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviourPunCallbacks
{
    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void PlayGame()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            Debug.LogError("Photon Network is not connected!");
        }

        Debug.Log("�κ� �����߽��ϴ�.");
        SceneManager.LoadScene("LobbyScene");
    }

    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void Option() {
        SceneManager.LoadScene("OptionScene");
    }

    // �����ϱ� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void QuitGame()
    {
        Application.Quit();  // ���� ������ ���� (����� ���ӿ����� �۵�)
    }
}
