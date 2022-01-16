using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsGenerator : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    private List<GameObject> activeRoad = new List<GameObject>(); 
    private float spawnPos = 0;
    private float roadLengt = 100;
    [SerializeField] private Transform player;
    private int startRoad = 3;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startRoad; i++)
        {
            if (i == 0)
                SpawnRoad(3);
            SpawnRoad(Random.Range(0, roadPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z -60> spawnPos - (startRoad * roadLengt))
        {
            SpawnRoad(Random.Range(0, roadPrefabs.Length));
            DeleteROad();
        }
           
    }

    private void SpawnRoad (int roadIndex)
    {
        GameObject nextRoad = Instantiate(roadPrefabs[roadIndex], transform.forward * spawnPos, transform.rotation);
        activeRoad.Add(nextRoad);
        spawnPos += roadLengt;
    }

    private void DeleteROad()
    {
        Destroy(activeRoad[0]);
        activeRoad.RemoveAt(0);
    }
}
