using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    void Start()
    {   
    }

    void Update()
    {
        transform.Translate(Vector3.up*speed*Time.deltaTime);
        if (transform.position.y>9)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
