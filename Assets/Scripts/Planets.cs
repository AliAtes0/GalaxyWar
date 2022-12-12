using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour
{
    [SerializeField]
    private float _speed = .1f;
    [SerializeField]
    private float _rotationSpeed = 1f;

    void Update()
    {
        transform.position += new Vector3(0f, -_speed * Time.deltaTime, 0f);
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed);
    }
}
