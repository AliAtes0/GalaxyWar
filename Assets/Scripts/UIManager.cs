using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _highScoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private TextMeshProUGUI _gameoverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    private GameManager _gameManager;
    [SerializeField]
    private Sprite[] _buffSprites;
    [SerializeField]
    private Image _buffImage;
    void Start()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        _highScoreText.text="High Score : "+PlayerPrefs.GetInt("HighScore",0).ToString();
        _buffImage.gameObject.SetActive(false);
        _scoreText.text = "Score : " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score : " + playerScore;
        if (playerScore>PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            _highScoreText.text="High Score : "+playerScore.ToString();
        }

    }
    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(true);
        _gameoverText.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
    public void ChangeBuffSprite(int x)
    {
        _buffImage.sprite = _buffSprites[x];
        _buffImage.gameObject.SetActive(true);
    }
    public void TurnBuffSprite()
    {
        _buffImage.gameObject.SetActive(false);
    }
    
}
