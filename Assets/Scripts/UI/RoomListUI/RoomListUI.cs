using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomListUI : MonoBehaviour
{
    // 방 목록 패널과 방 만들기 패널
    public GameObject roomListPanel;
    public GameObject roomCreationPanel;

    // 탭 버튼
    public GameObject roomListTabButton;
    public GameObject roomCreationTabButton;

    public void OnCloseButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 탭 전환 시 호출될 함수
    public void ShowRoomListPanel()
    {
        // 방 목록 패널을 활성화하고 방 만들기 패널을 비활성화
        roomListPanel.SetActive(true);
        roomCreationPanel.SetActive(false);

        // 선택된 탭 버튼의 상태를 업데이트 (선택된 상태로 강조)
        roomListTabButton.GetComponent<UnityEngine.UI.Button>().interactable = false;  // 현재 탭을 비활성화하여 선택된 것처럼 보이게 함
        roomCreationTabButton.GetComponent<UnityEngine.UI.Button>().interactable = true; // 다른 탭은 활성화
    }

    public void ShowRoomCreationPanel()
    {
        // 방 만들기 패널을 활성화하고 방 목록 패널을 비활성화
        roomListPanel.SetActive(false);
        roomCreationPanel.SetActive(true);

        // 선택된 탭 버튼의 상태를 업데이트
        roomListTabButton.GetComponent<UnityEngine.UI.Button>().interactable = true;   // 다른 탭 활성화
        roomCreationTabButton.GetComponent<UnityEngine.UI.Button>().interactable = false;  // 현재 탭을 비활성화
    }

    // Start 함수에서 초기 탭 설정
    void Start()
    {
        // 초기에는 방 목록 패널을 활성화
        ShowRoomListPanel();
    }
}
