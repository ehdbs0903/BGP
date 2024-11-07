using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionUI : MonoBehaviour
{
    public void OnCloseButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }
}
