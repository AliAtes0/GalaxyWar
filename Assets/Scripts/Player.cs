using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f; //Speed of our player
    private float _speedBuff = 5f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _ultraShotPrefab;
    [SerializeField]
    private float _fireRate = .3f;
    private float nextFire = 0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    private bool _isUltraShotActive = false;
    [SerializeField]
    private GameObject shieldEffect;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _explodeFX;
    AudioSource _laserAudio;
    AudioSource _damageAudio;
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _laserAudio = GameObject.Find("Laser").GetComponent<AudioSource>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _damageAudio = GameObject.Find("Damage").GetComponent<AudioSource>();
        if (spawnManager == null)
        {
            Debug.LogError("The SpawnManager is null");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is null");
        }

    }

    void Update()
    {
        CalculateMovement();
        // if i hit the space key
        //shoot
        if (Input.GetButtonDown("Jump") && Time.time > nextFire)
        {
            FireLaser();
        }



    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Get x axis
        _anim.SetFloat("speed", horizontalInput);
        float verticalInput = Input.GetAxisRaw("Vertical"); // Get y axis
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f); // Direction of player
        transform.Translate(direction * Time.deltaTime * _speed); //move
        //if player position on the y is greater than 0
        // y position = 0
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2.7f, 0));

        if (transform.position.x > 11.35f)
        {
            transform.position = new Vector3(-11.35f, transform.position.y, 0f);
        }
        else if (transform.position.x < -11.35f)
        {
            transform.position = new Vector3(11.35f, transform.position.y, 0f);
        }
    }
    private void FireLaser()
    {
        nextFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            _laserAudio.Play();
        }
        else if (_isUltraShotActive)
        {
            Instantiate(_ultraShotPrefab, transform.position+Vector3.up, Quaternion.identity);
            _laserAudio.Play();
        }
        else
        {
            Instantiate(laserPrefab, transform.position + Vector3.up * .6f, Quaternion.identity);
            _laserAudio.Play();
        }
    }


    public void Damage()
    {
        if (_isShieldActive)
        {
            return;
        }
        _lives--;
        _damageAudio.Play();
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            spawnManager.OnPlayerDeath();
            spawnManager.gameObject.SetActive(false);
            Instantiate(_explodeFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(7f);
        _isTripleShotActive = false;
        _uiManager.TurnBuffSprite();
    }
    public void SpeedBuffActive()
    {
        _speed += _speedBuff;
        StartCoroutine(SpeedBuffDownRoutine());
    }
    IEnumerator SpeedBuffDownRoutine()
    {
        yield return new WaitForSeconds(3f);
        _speed -= _speedBuff;
        _uiManager.TurnBuffSprite();
    }
    public void ShieldBuffActive()
    {
        _isShieldActive = true;
        shieldEffect.SetActive(true);
        StartCoroutine(ShieldBuffDown());
    }
    IEnumerator ShieldBuffDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldActive = false;
        _uiManager.TurnBuffSprite();
        shieldEffect.SetActive(false);
    }
    public void UltraShotActive()
    {
        _isUltraShotActive = true;
        StartCoroutine(UltraShotDown());
    }
    IEnumerator UltraShotDown()
    {
        yield return new WaitForSeconds(7.0f);
        _isUltraShotActive= false;
        _uiManager.TurnBuffSprite();
    }

    //method to add 10 to the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
