using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private bool isRandom;
    [SerializeField] private float spawnTime = 1f;

    private int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObjectsCreatorProcess());
    }

    private IEnumerator ObjectsCreatorProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            if (isRandom)
            {
                current = Random.Range(0, prefabs.Length);
            }

            GameObject obj = Instantiate(prefabs[current]);
            obj.transform.position = transform.position;

            if (isRandom)
            {
                continue;
            }

            current++;
            if (current >= prefabs.Length)
            {
                current = 0;
            }

        }
        
    }
}
