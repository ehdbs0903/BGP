using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomListUI : MonoBehaviour
{
    // �� ��� �гΰ� �� ����� �г�
    public GameObject roomListPanel;
    public GameObject roomCreationPanel;

    // �� ��ư
    public GameObject roomListTabButton;
    public GameObject roomCreationTabButton;

    public void OnCloseButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    // �� ��ȯ �� ȣ��� �Լ�
    public void ShowRoomListPanel()
    {
        // �� ��� �г��� Ȱ��ȭ�ϰ� �� ����� �г��� ��Ȱ��ȭ
        roomListPanel.SetActive(true);
        roomCreationPanel.SetActive(false);

        // ���õ� �� ��ư�� ���¸� ������Ʈ (���õ� ���·� ����)
        roomListTabButton.GetComponent<UnityEngine.UI.Button>().interactable = false;  // ���� ���� ��Ȱ��ȭ�Ͽ� ���õ� ��ó�� ���̰� ��
        roomCreationTabButton.GetComponent<UnityEngine.UI.Button>().interactable = true; // �ٸ� ���� Ȱ��ȭ
    }

    public void ShowRoomCreationPanel()
    {
        // �� ����� �г��� Ȱ��ȭ�ϰ� �� ��� �г��� ��Ȱ��ȭ
        roomListPanel.SetActive(false);
        roomCreationPanel.SetActive(true);

        // ���õ� �� ��ư�� ���¸� ������Ʈ
        roomListTabButton.GetComponent<UnityEngine.UI.Button>().interactable = true;   // �ٸ� �� Ȱ��ȭ
        roomCreationTabButton.GetComponent<UnityEngine.UI.Button>().interactable = false;  // ���� ���� ��Ȱ��ȭ
    }

    // Start �Լ����� �ʱ� �� ����
    void Start()
    {
        // �ʱ⿡�� �� ��� �г��� Ȱ��ȭ
        ShowRoomListPanel();
    }
}
