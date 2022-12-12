using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    
    private void Update()
    {
        
        if ((Input.GetKeyDown(KeyCode.R)||Input.GetButtonDown("Jump"))&&_isGameOver)
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
