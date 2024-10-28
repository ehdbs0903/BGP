using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviourPunCallbacks
{
    // 게임 시작 버튼 클릭 시 호출되는 함수
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

        SceneManager.LoadScene("NicknameScene");
    }

    // 설정 버튼 클릭 시 호출되는 함수
    public void Option() {
        SceneManager.LoadScene("OptionScene");
    }

    // 종료하기 버튼 클릭 시 호출되는 함수
    public void QuitGame()
    {
        Application.Quit();  // 실제 게임을 종료 (빌드된 게임에서만 작동)
    }
}
