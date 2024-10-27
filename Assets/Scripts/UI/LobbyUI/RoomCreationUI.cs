using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RoomCreationUI : MonoBehaviourPunCallbacks
{
    public LobbyUI lobbyUI;
    // 방 제목을 입력받는 TMP InputField
    public TMP_InputField roomNameInputField;

    // 게임 종류와 최대 인원을 위한 TMP 드롭다운 및 텍스트
    public TMP_Dropdown gameTypeDropdown;
    public TMP_Dropdown maxPlayersDropdown;

    // 게임 종류와 최대 인원 변경을 위한 좌우 화살표 버튼
    public Button leftGameTypeButton;
    public Button rightGameTypeButton;
    public Button leftMaxPlayersButton;
    public Button rightMaxPlayersButton;

    // 게임 종류와 최대 인원을 위한 데이터 및 인덱스
    private List<string> gameTypes = new List<string> { "얼쑤: 곶감전"};
    private int currentGameTypeIndex = 0;

    private List<int> maxPlayerOptions = new List<int> { 2 };
    private int currentMaxPlayersIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (gameTypeDropdown == null)
        {
            Debug.LogError("Game Type Dropdown is not assigned!");
        }

        if (maxPlayersDropdown == null)
        {
            Debug.LogError("Max Players Dropdown is not assigned!");
        }
        // 드롭다운을 비활성화하고 화살표로만 변경
        gameTypeDropdown.interactable = false;
        maxPlayersDropdown.interactable = false;

        // 드롭다운 초기화
        InitializeGameTypeDropdown();
        InitializeMaxPlayersDropdown();

        // 화살표 버튼 이벤트 연결
        leftGameTypeButton.onClick.AddListener(PreviousGameType);
        rightGameTypeButton.onClick.AddListener(NextGameType);

        leftMaxPlayersButton.onClick.AddListener(PreviousMaxPlayers);
        rightMaxPlayersButton.onClick.AddListener(NextMaxPlayers);

        UpdateDropdownText(); // 초기 텍스트 설정
    }

    // 드롭다운 초기화: 게임 종류
    private void InitializeGameTypeDropdown()
    {
        gameTypeDropdown.ClearOptions();
        gameTypeDropdown.AddOptions(gameTypes);
    }

    // 드롭다운 초기화: 최대 인원
    private void InitializeMaxPlayersDropdown()
    {
        maxPlayersDropdown.ClearOptions();
        List<string> maxPlayersStringOptions = new List<string>();
        foreach (int option in maxPlayerOptions)
        {
            maxPlayersStringOptions.Add(option.ToString());
        }
        maxPlayersDropdown.AddOptions(maxPlayersStringOptions);
    }

    // 이전 게임 종류로 변경
    public void PreviousGameType()
    {
        currentGameTypeIndex = (currentGameTypeIndex > 0) ? currentGameTypeIndex - 1 : gameTypes.Count - 1;
        UpdateDropdownText();
    }

    // 다음 게임 종류로 변경
    public void NextGameType()
    {
        currentGameTypeIndex = (currentGameTypeIndex < gameTypes.Count - 1) ? currentGameTypeIndex + 1 : 0;
        UpdateDropdownText();
    }

    // 이전 최대 인원으로 변경
    public void PreviousMaxPlayers()
    {
        currentMaxPlayersIndex = (currentMaxPlayersIndex > 0) ? currentMaxPlayersIndex - 1 : maxPlayerOptions.Count - 1;
        UpdateDropdownText();
    }

    // 다음 최대 인원으로 변경
    public void NextMaxPlayers()
    {
        currentMaxPlayersIndex = (currentMaxPlayersIndex < maxPlayerOptions.Count - 1) ? currentMaxPlayersIndex + 1 : 0;
        UpdateDropdownText();
    }

    // 드롭다운의 텍스트 업데이트
    private void UpdateDropdownText()
    {
        gameTypeDropdown.value = currentGameTypeIndex;
        maxPlayersDropdown.value = currentMaxPlayersIndex;
    }

    // 방 생성 버튼 클릭 시 호출되는 메소드
    public void OnConfirmClick()
    {
        string roomName = roomNameInputField.text;
        string gameType = gameTypes[currentGameTypeIndex];
        int maxPlayers = maxPlayerOptions[currentMaxPlayersIndex];

        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogError("Room name is empty!");
            return;
        }

        // 방 생성 옵션 설정
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;  // 최대 인원 설정

        // Custom Properties에 게임 종류 추가
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties.Add("GameType", gameType);
        roomOptions.CustomRoomProperties = customProperties;

        roomOptions.CustomRoomPropertiesForLobby = new string[] { "GameType" };

        // PhotonNetwork를 사용하여 방 생성
        PhotonNetwork.CreateRoom(roomName, roomOptions);

        Debug.Log($"Creating room: {roomName}, Game Type: {gameType}, Max Players: {maxPlayers}");

        SceneManager.LoadScene("RoomScene");
    }

    // 방 생성 성공 시 호출되는 콜백
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully!");
    }

    // 방 생성 실패 시 호출되는 콜백
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room creation failed: {message}");
    }

    // 취소 버튼 클릭 시 호출되는 메소드
    public void OnCancelClick()
    {
        lobbyUI.ShowRoomListPanel();
        Debug.Log("Room creation cancelled.");
    }
}
