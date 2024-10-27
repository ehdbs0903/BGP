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
    // �� ������ �Է¹޴� TMP InputField
    public TMP_InputField roomNameInputField;

    // ���� ������ �ִ� �ο��� ���� TMP ��Ӵٿ� �� �ؽ�Ʈ
    public TMP_Dropdown gameTypeDropdown;
    public TMP_Dropdown maxPlayersDropdown;

    // ���� ������ �ִ� �ο� ������ ���� �¿� ȭ��ǥ ��ư
    public Button leftGameTypeButton;
    public Button rightGameTypeButton;
    public Button leftMaxPlayersButton;
    public Button rightMaxPlayersButton;

    // ���� ������ �ִ� �ο��� ���� ������ �� �ε���
    private List<string> gameTypes = new List<string> { "��: ������"};
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
        // ��Ӵٿ��� ��Ȱ��ȭ�ϰ� ȭ��ǥ�θ� ����
        gameTypeDropdown.interactable = false;
        maxPlayersDropdown.interactable = false;

        // ��Ӵٿ� �ʱ�ȭ
        InitializeGameTypeDropdown();
        InitializeMaxPlayersDropdown();

        // ȭ��ǥ ��ư �̺�Ʈ ����
        leftGameTypeButton.onClick.AddListener(PreviousGameType);
        rightGameTypeButton.onClick.AddListener(NextGameType);

        leftMaxPlayersButton.onClick.AddListener(PreviousMaxPlayers);
        rightMaxPlayersButton.onClick.AddListener(NextMaxPlayers);

        UpdateDropdownText(); // �ʱ� �ؽ�Ʈ ����
    }

    // ��Ӵٿ� �ʱ�ȭ: ���� ����
    private void InitializeGameTypeDropdown()
    {
        gameTypeDropdown.ClearOptions();
        gameTypeDropdown.AddOptions(gameTypes);
    }

    // ��Ӵٿ� �ʱ�ȭ: �ִ� �ο�
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

    // ���� ���� ������ ����
    public void PreviousGameType()
    {
        currentGameTypeIndex = (currentGameTypeIndex > 0) ? currentGameTypeIndex - 1 : gameTypes.Count - 1;
        UpdateDropdownText();
    }

    // ���� ���� ������ ����
    public void NextGameType()
    {
        currentGameTypeIndex = (currentGameTypeIndex < gameTypes.Count - 1) ? currentGameTypeIndex + 1 : 0;
        UpdateDropdownText();
    }

    // ���� �ִ� �ο����� ����
    public void PreviousMaxPlayers()
    {
        currentMaxPlayersIndex = (currentMaxPlayersIndex > 0) ? currentMaxPlayersIndex - 1 : maxPlayerOptions.Count - 1;
        UpdateDropdownText();
    }

    // ���� �ִ� �ο����� ����
    public void NextMaxPlayers()
    {
        currentMaxPlayersIndex = (currentMaxPlayersIndex < maxPlayerOptions.Count - 1) ? currentMaxPlayersIndex + 1 : 0;
        UpdateDropdownText();
    }

    // ��Ӵٿ��� �ؽ�Ʈ ������Ʈ
    private void UpdateDropdownText()
    {
        gameTypeDropdown.value = currentGameTypeIndex;
        maxPlayersDropdown.value = currentMaxPlayersIndex;
    }

    // �� ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
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

        // �� ���� �ɼ� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;  // �ִ� �ο� ����

        // Custom Properties�� ���� ���� �߰�
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties.Add("GameType", gameType);
        roomOptions.CustomRoomProperties = customProperties;

        roomOptions.CustomRoomPropertiesForLobby = new string[] { "GameType" };

        // PhotonNetwork�� ����Ͽ� �� ����
        PhotonNetwork.CreateRoom(roomName, roomOptions);

        Debug.Log($"Creating room: {roomName}, Game Type: {gameType}, Max Players: {maxPlayers}");

        SceneManager.LoadScene("RoomScene");
    }

    // �� ���� ���� �� ȣ��Ǵ� �ݹ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully!");
    }

    // �� ���� ���� �� ȣ��Ǵ� �ݹ�
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room creation failed: {message}");
    }

    // ��� ��ư Ŭ�� �� ȣ��Ǵ� �޼ҵ�
    public void OnCancelClick()
    {
        lobbyUI.ShowRoomListPanel();
        Debug.Log("Room creation cancelled.");
    }
}
