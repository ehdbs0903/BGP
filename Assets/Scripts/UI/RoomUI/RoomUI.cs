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
    // UI ��ҵ�
    public TMP_Text roomNameText;
    public TMP_Text gameTypeText;
    public TMP_Text playerCountText;

    public Transform playerListContent;
    public GameObject playerListItemPrefab;

    public Button readyButton;
    public Button exitButton;
    public TMP_Text readyButtonText;

    private Dictionary<int, GameObject> playerListItems = new Dictionary<int, GameObject>();

    // ���� ����
    private bool isLeavingRoom = false;

    void Start()
    {
        // �濡 �̹� ������ �ִ� ��� �ʱ�ȭ ����
        if (PhotonNetwork.InRoom)
        {
            InitializeRoomUI();
        }
        // �濡 �������� ���� ���, OnJoinedRoom���� �ʱ�ȭ ����
    }

    void Awake()
    {
        // �ڵ� �� ����ȭ ����
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // �濡 ������ �� UI�� �ʱ�ȭ�ϴ� �޼���
    void InitializeRoomUI()
    {
        UpdateRoomInfo();
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // �� ���� ������Ʈ
    void UpdateRoomInfo()
    {
        Room room = PhotonNetwork.CurrentRoom;
        if (room != null)
        {
            roomNameText.text = room.Name;

            // ���� ������ ���� Custom Properties���� �����ɴϴ�.
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

    // �÷��̾� ����Ʈ ������Ʈ
    void UpdatePlayerList()
    {
        // ���� ����Ʈ ������ ����
        foreach (GameObject item in playerListItems.Values)
        {
            Destroy(item);
        }
        playerListItems.Clear();

        // �÷��̾� ����Ʈ ����
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject listItem = Instantiate(playerListItemPrefab, playerListContent);

            // ������Ʈ �̸��� ��ҹ��ڱ��� ��Ȯ�ϰ� ��ġ�ؾ� �մϴ�.
            Transform nameTransform = listItem.transform.Find("PlayerName");
            Transform statusTransform = listItem.transform.Find("PlayerStatus");

            if (nameTransform == null || statusTransform == null)
            {
                Debug.LogError("PlayerName �Ǵ� PlayerStatus�� ã�� �� �����ϴ�. Prefab�� �̸��� Ȯ���ϼ���.");
                continue;
            }

            TMP_Text playerNameText = nameTransform.GetComponent<TMP_Text>();
            TMP_Text playerStatusText = statusTransform.GetComponent<TMP_Text>();

            if (playerNameText == null || playerStatusText == null)
            {
                Debug.LogError("TMP_Text ������Ʈ�� ã�� �� �����ϴ�.");
                continue;
            }

            playerNameText.text = player.NickName;

            // ���� ���� Ȯ��
            if (player.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
            {
                playerStatusText.text = "����";
            }
            else
            {
                object isReady;
                if (player.CustomProperties.TryGetValue("IsReady", out isReady) && (bool)isReady)
                {
                    playerStatusText.text = "�غ�";
                }
                else
                {
                    playerStatusText.text = "";
                }
            }

            playerListItems.Add(player.ActorNumber, listItem);
        }
    }

    // �غ� ��ư ������Ʈ
    void UpdateReadyButton()
    {
        // Null üũ �߰�
        if (PhotonNetwork.LocalPlayer == null || PhotonNetwork.MasterClient == null)
        {
            Debug.LogWarning("LocalPlayer �Ǵ� MasterClient�� null�Դϴ�.");
            return;
        }

        if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
        {
            // ������ ���
            readyButtonText.text = "���ӽ���";

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
            // �Ϲ� �÷��̾��� ���
            object isReady;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("IsReady", out isReady) && (bool)isReady)
            {
                readyButtonText.text = "���";
            }
            else
            {
                readyButtonText.text = "�غ�";
            }
            readyButton.interactable = true;
        }
    }

    // ��� �÷��̾ �غ�Ǿ����� Ȯ��
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

    // �÷��̾ �濡 ������ ��
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateRoomInfo();
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // �÷��̾ �濡�� ������ ��
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRoomInfo();
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // �÷��̾� �Ӽ��� ����Ǿ��� ��
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        UpdatePlayerList();
        UpdateReadyButton();
    }

    // �濡 �������� �� ȣ��Ǵ� �ݹ�
    public override void OnJoinedRoom()
    {
        InitializeRoomUI();
    }

    // �غ� ��ư Ŭ�� �̺�Ʈ (public���� ����)
    public void OnReadyButtonClicked()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.MasterClient.ActorNumber)
        {
            // ������ ���
            if (AllPlayersReady() && PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                // ���� ���� ����
                PhotonNetwork.LoadLevel("GameScene");
            }
            else
            {
                Debug.LogWarning("��� �÷��̾ �غ���� �ʾҰų� �÷��̾� ���� �����մϴ�.");
            }
        }
        else
        {
            // �Ϲ� �÷��̾��� ���
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

    // ������ ��ư Ŭ�� �̺�Ʈ (public���� ����)
    public void OnExitButtonClicked()
    {
        if (PhotonNetwork.InRoom)
        {
            isLeavingRoom = true;

            // �ڵ� �� ����ȭ ��Ȱ��ȭ
            //PhotonNetwork.AutomaticallySyncScene = false;

            // ������ ��ư ��Ȱ��ȭ
            exitButton.interactable = false;
            readyButton.interactable = false;
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            Debug.LogWarning("�濡 �������� �ʾҽ��ϴ�.");
        }
    }

    // ���� ������ �� ȣ��Ǵ� �ݹ�
    public override void OnLeftRoom()
    {
        isLeavingRoom = false;
    }

    public override void OnConnectedToMaster()
    {
        // ������ ������ �翬��Ǿ����Ƿ� �κ� ����
        PhotonNetwork.JoinLobby();
    }

    // �κ� �������� �� ȣ��Ǵ� �ݹ� (�ʿ信 ���� ���)
    public override void OnJoinedLobby()
    {
        // �κ� ������ �̵�
        SceneManager.LoadScene("LobbyScene");
    }
}
