using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private Player _player;
    private Animator _anim;
    private AudioSource _explodeSound;
    [SerializeField]
    private int _lives = 1;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _explodeSound = GameObject.Find("Explode").GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -5)
        {
            transform.position = new Vector3(Random.Range(-9, 9), 7f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            _explodeSound.Play();
            OnEnemyDeath();
            _speed = 0f;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject, 1.2f);


            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
        if (collision.tag == "Laser")
        {
            //damage to enemy
            //destroy the laser
            _lives--;
            Destroy(collision.gameObject);
            if (_lives <= 0)
            {
                _explodeSound.Play();
                OnEnemyDeath();
                _speed = 0f;
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                Destroy(this.gameObject, 1.2f);

            }
            //add 10 score
            if (_player != null)
            {
                _player.AddScore(10);
            }
        }
        else if(collision.tag == "UltraShot")
        {
            _lives-=2;
            Destroy(collision.gameObject);
            if (_lives <= 0)
            {
                _explodeSound.Play();
                OnEnemyDeath();
                _speed = 0f;
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                Destroy(this.gameObject, 1.2f);

            }
            //add 10 score
            if (_player != null)
            {
                _player.AddScore(10);
            }
        }
        void OnEnemyDeath()
        {
            _anim.SetTrigger("OnEnemyDeath");
        }
    }
}
