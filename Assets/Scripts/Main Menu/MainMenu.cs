using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Canvas loadingCanvas;
    public void LoadGame()
    {
        loadingCanvas.gameObject.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
