using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviourPunCallbacks
{
    // 방 목록 패널과 방 만들기 패널
    public GameObject roomListPanel;
    public GameObject roomCreationPanel;

    // 탭 버튼
    public GameObject roomListTabButton;
    public GameObject roomCreationTabButton;

    public void OnCloseButtonPressed()
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("로비에서 나갔습니다.");
        SceneManager.LoadScene("MainScene");
    }

    // 탭 전환 시 호출될 함수
    public void ShowRoomListPanel()
    {
        // 방 목록 패널을 활성화하고 방 만들기 패널을 비활성화
        roomListPanel.SetActive(true);
        roomCreationPanel.SetActive(false);

        // 선택된 탭 버튼의 상태를 업데이트 (선택된 상태로 강조)
        roomListTabButton.GetComponent<Button>().interactable = false;
        roomCreationTabButton.GetComponent<Button>().interactable = true;
    }

    public void ShowRoomCreationPanel()
    {
        // 방 만들기 패널을 활성화하고 방 목록 패널을 비활성화
        roomListPanel.SetActive(false);
        roomCreationPanel.SetActive(true);

        // 선택된 탭 버튼의 상태를 업데이트
        roomListTabButton.GetComponent<Button>().interactable = true;
        roomCreationTabButton.GetComponent<Button>().interactable = false;
    }

    // Start 함수에서 초기 탭 설정
    void Start()
    {
        ShowRoomListPanel();
    }
}
