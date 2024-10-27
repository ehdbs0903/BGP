using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviourPunCallbacks
{
    // �� ��� �гΰ� �� ����� �г�
    public GameObject roomListPanel;
    public GameObject roomCreationPanel;

    // �� ��ư
    public GameObject roomListTabButton;
    public GameObject roomCreationTabButton;

    public void OnCloseButtonPressed()
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("�κ񿡼� �������ϴ�.");
        SceneManager.LoadScene("MainScene");
    }

    // �� ��ȯ �� ȣ��� �Լ�
    public void ShowRoomListPanel()
    {
        // �� ��� �г��� Ȱ��ȭ�ϰ� �� ����� �г��� ��Ȱ��ȭ
        roomListPanel.SetActive(true);
        roomCreationPanel.SetActive(false);

        // ���õ� �� ��ư�� ���¸� ������Ʈ (���õ� ���·� ����)
        roomListTabButton.GetComponent<Button>().interactable = false;
        roomCreationTabButton.GetComponent<Button>().interactable = true;
    }

    public void ShowRoomCreationPanel()
    {
        // �� ����� �г��� Ȱ��ȭ�ϰ� �� ��� �г��� ��Ȱ��ȭ
        roomListPanel.SetActive(false);
        roomCreationPanel.SetActive(true);

        // ���õ� �� ��ư�� ���¸� ������Ʈ
        roomListTabButton.GetComponent<Button>().interactable = true;
        roomCreationTabButton.GetComponent<Button>().interactable = false;
    }

    // Start �Լ����� �ʱ� �� ����
    void Start()
    {
        ShowRoomListPanel();
    }
}
