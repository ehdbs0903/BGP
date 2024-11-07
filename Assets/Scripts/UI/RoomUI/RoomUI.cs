using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RoomUI : MonoBehaviourPunCallbacks
{
    // UI 요소들
    public TMP_Text roomNameText;
    public TMP_Text gameTypeText;
    public TMP_Text playerCountText;

    public Transform playerListContent;
    public GameObject playerListItemPrefab;

    public Button readyButton;
    public Button exitButton;
    public TMP_Text readyButtonText;

    private Dictionary<int, GameObject> playerListItems = new Dictionary<int, GameObject>();

    // 상태 변수
    private bool isLeavingRoom = false;

    void Start()
    {
        // 방에 이미 입장해 있는 경우 초기화 수행
        if (PhotonNetwork.InRoom)
        {
            InitializeRoomUI();
        }
        // 방에 입장하지 않은 경우, OnJoinedRoom에서 초기화 수행
    }

    void Awake()
    {
        // 자동 씬 동기화 설정
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // 방에 입장한 후 UI를 초기화하는 메서드
    void InitializeRoomUI()
    {
        UpdateRoomInfo();
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // 방 정보 업데이트
    void UpdateRoomInfo()
    {
        Room room = PhotonNetwork.CurrentRoom;
        if (room != null)
        {
            roomNameText.text = room.Name;

            // 게임 종류는 방의 Custom Properties에서 가져옵니다.
            object gameType;
            if (room.CustomProperties.TryGetValue("GameType", out gameType))
            {
                gameTypeText.text = gameType.ToString();
            }
            else
            {
                gameTypeText.text = "Unknown";
            }
            playerCountText.text = room.PlayerCount + " / " + room.MaxPlayers;
        }
        else
        {
            Debug.LogError("CurrentRoom is null in UpdateRoomInfo");
        }
    }

    // 플레이어 리스트 업데이트
    void UpdatePlayerList()
    {
        // 기존 리스트 아이템 제거
        foreach (GameObject item in playerListItems.Values)
        {
            Destroy(item);
        }
        playerListItems.Clear();

        // 플레이어 리스트 생성
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject listItem = Instantiate(playerListItemPrefab, playerListContent);

            // 오브젝트 이름은 대소문자까지 정확하게 일치해야 합니다.
            Transform nameTransform = listItem.transform.Find("PlayerName");
            Transform statusTransform = listItem.transform.Find("PlayerStatus");

            if (nameTransform == null || statusTransform == null)
            {
                Debug.LogError("PlayerName 또는 PlayerStatus를 찾을 수 없습니다. Prefab의 이름을 확인하세요.");
                continue;
            }

            TMP_Text playerNameText = nameTransform.GetComponent<TMP_Text>();
            TMP_Text playerStatusText = statusTransform.GetComponent<TMP_Text>();

            if (playerNameText == null || playerStatusText == null)
            {
                Debug.LogError("TMP_Text 컴포넌트를 찾을 수 없습니다.");
                continue;
            }

            playerNameText.text = player.NickName;

            // 방장 여부 확인
            if (player.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
            {
                playerStatusText.text = "방장";
            }
            else
            {
                object isReady;
                if (player.CustomProperties.TryGetValue("IsReady", out isReady) && (bool)isReady)
                {
                    playerStatusText.text = "준비";
                }
                else
                {
                    playerStatusText.text = "";
                }
            }

            playerListItems.Add(player.ActorNumber, listItem);
        }
    }

    // 준비 버튼 업데이트
    void UpdateReadyButton()
    {
        // Null 체크 추가
        if (PhotonNetwork.LocalPlayer == null || PhotonNetwork.MasterClient == null)
        {
            Debug.LogWarning("LocalPlayer 또는 MasterClient가 null입니다.");
            return;
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
        {
            // 방장인 경우
            readyButtonText.text = "게임시작";

            if (AllPlayersReady() && PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                readyButton.interactable = true;
            }
            else
            {
                readyButton.interactable = false;
            }
        }
        else
        {
            // 일반 플레이어인 경우
            object isReady;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("IsReady", out isReady) && (bool)isReady)
            {
                readyButtonText.text = "취소";
            }
            else
            {
                readyButtonText.text = "준비";
            }
            readyButton.interactable = true;
        }
    }

    // 모든 플레이어가 준비되었는지 확인
    bool AllPlayersReady()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber != PhotonNetwork.MasterClient.ActorNumber)
            {
                object isReady;
                if (!player.CustomProperties.TryGetValue("IsReady", out isReady) || !(bool)isReady)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // 플레이어가 방에 들어왔을 때
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateRoomInfo();
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // 플레이어가 방에서 나갔을 때
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRoomInfo();
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // 플레이어 속성이 변경되었을 때
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // 방에 입장했을 때 호출되는 콜백
    public override void OnJoinedRoom()
    {
        InitializeRoomUI();
    }

    // 준비 버튼 클릭 이벤트 (public으로 선언)
    public void OnReadyButtonClicked()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
        {
            // 방장인 경우
            if (AllPlayersReady() && PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                // 게임 시작 로직
                PhotonNetwork.LoadLevel("GameScene");
            }
            else
            {
                Debug.LogWarning("모든 플레이어가 준비되지 않았거나 플레이어 수가 부족합니다.");
            }
        }
        else
        {
            // 일반 플레이어인 경우
            object isReady;
            bool ready = false;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("IsReady", out isReady))
            {
                ready = !(bool)isReady;
            }
            else
            {
                ready = true;
            }
            Hashtable props = new Hashtable() { { "IsReady", ready } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }
    }

    // 나가기 버튼 클릭 이벤트 (public으로 선언)
    public void OnExitButtonClicked()
    {
        if (PhotonNetwork.InRoom)
        {
            isLeavingRoom = true;

            // 자동 씬 동기화 비활성화
            //PhotonNetwork.AutomaticallySyncScene = false;

            // 나가기 버튼 비활성화
            exitButton.interactable = false;
            readyButton.interactable = false;
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            Debug.LogWarning("방에 입장하지 않았습니다.");
        }
    }

    // 방을 나갔을 때 호출되는 콜백
    public override void OnLeftRoom()
    {
        isLeavingRoom = false;
    }

    public override void OnConnectedToMaster()
    {
        // 마스터 서버에 재연결되었으므로 로비에 접속
        PhotonNetwork.JoinLobby();
    }

    // 로비에 입장했을 때 호출되는 콜백 (필요에 따라 사용)
    public override void OnJoinedLobby()
    {
        // 로비 씬으로 이동
        SceneManager.LoadScene("LobbyScene");
    }
}
