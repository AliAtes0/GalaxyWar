using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] Planets;
    void Start()
    {
        InvokeRepeating("SpawnPlanet", 5, 30);

    }

    void SpawnPlanet()
    {
        int randomPos = Random.Range(-9, 10);
        Vector3 posToSpawn = new Vector3(randomPos, transform.position.y, 1f);
        int randomPlanet=Random.Range(0, Planets.Length);
        int randomScale = Random.Range(2, 6);
        Planets[randomPlanet].transform.localScale = new Vector3(randomScale, randomScale, 1f);

        Instantiate(Planets[randomPlanet], posToSpawn, Quaternion.identity, this.transform);
    }
}
