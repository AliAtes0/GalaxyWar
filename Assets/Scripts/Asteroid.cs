using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explodeFX;
    private float _rotateSpeed = 10.0f;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager= GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            _spawnManager.StartSpawning();
            Destroy(collision.gameObject);
            GameObject explode = Instantiate(_explodeFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject, .05f);
            Destroy(explode, 2.5f);
        }
    }
    
}
