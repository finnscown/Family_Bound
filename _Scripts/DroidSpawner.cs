using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidSpawner : MonoBehaviour
{
    public GameObject Android;
    // Start is called before the first frame update
    void Start()
    {
       
        InvokeRepeating("SpawnAndroid", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAndroid()
    {
        Instantiate<GameObject>(Android, new Vector3(Random.Range(-50, 50), Random.Range(10, 30), transform.position.z), Quaternion.identity);
    }
}
