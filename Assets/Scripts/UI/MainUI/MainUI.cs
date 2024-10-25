using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenu : MonoBehaviour
{
    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void PlayGame()
    {
        SceneManager.LoadScene("RoomListScene");
    }

    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void Option() {
        SceneManager.LoadScene("OptionScene");
    }

    // �����ϱ� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void QuitGame()
    {
        Application.Quit();  // ���� ������ ���� (����� ���ӿ����� �۵�)
    }
}
