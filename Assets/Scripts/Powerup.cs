using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerupID;
    private UIManager _uiManager;
    private AudioSource _powerupAudio;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _powerupAudio=GameObject.Find("Powerup").GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -5)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _powerupAudio.Play();
            Player player = collision.transform.GetComponent<Player>();
            /*if (player != null)
            {
                if (powerupID == 0)
                    player.TripleShotActive();
                else if (powerupID == 1)
                    player.SpeedBuffActive();
                else
                    player.ShieldBuffActive();
            }*/
            switch (powerupID)
            {
                case 0:

                    player.TripleShotActive();
                    break;
                case 1:

                    player.SpeedBuffActive();
                    break;
                case 2:

                    player.ShieldBuffActive();
                    break;
                case 3:
                    player.UltraShotActive();
                    break;
            }
            _uiManager.ChangeBuffSprite(powerupID);
            Destroy(this.gameObject);
        }
    }
}
