using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class NicknameUI : MonoBehaviour
{
    public TMP_InputField nicknameInputField; // �г��� �Է� �ʵ�

    // Ȯ�� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnConfirmButtonClicked()
    {
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("�г����� �Է��ϼ���.");
            return;
        }

        if (PhotonNetwork.IsConnected)
        {
            // Photon ��Ʈ��ũ�� �÷��̾� �̸� ����
            PhotonNetwork.NickName = nickname;

            PhotonNetwork.JoinLobby();

            // �κ� ������ �̵�
            Debug.Log("�κ� �����߽��ϴ�.");
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            Debug.LogError("Photon Network is not connected!");
        }
    }

    public void OnCancelButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
