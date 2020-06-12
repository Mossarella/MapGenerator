using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tileObjects;
    void Start()
    {
        int rand = Random.Range(0, tileObjects.Length);
        GameObject instance=(GameObject)Instantiate(tileObjects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
