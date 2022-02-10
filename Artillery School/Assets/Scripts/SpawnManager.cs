using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject targetPrefab;
    private int targetCount;
    private int waveCount;

    // Start is called before the first frame update
    void Start()
    {
        waveCount = 1;
        targetCount = GameObject.FindGameObjectsWithTag("Target").Length;
    }

    // Update is called once per frame
    void Update()
    {
        targetCount = GameObject.FindGameObjectsWithTag("Target").Length;

        if(targetCount < 1)
        {
            for(int i = 0; i < waveCount; i++)
            {
                Vector3 randomSpawnPos = new Vector3(Random.Range(-50,-150), 1, Random.Range(-50,50));

                Instantiate(targetPrefab, randomSpawnPos, targetPrefab.transform.rotation);
            }

            waveCount++;
        }
    }
}
