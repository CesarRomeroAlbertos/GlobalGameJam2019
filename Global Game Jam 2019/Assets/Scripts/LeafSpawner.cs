using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    public int nLeafs;
    public float spawnRatio;

    public GameObject leafPrefab;

    public List<GameObject> leafList;
    public int leafFloorCount;

    // Start is called before the first frame update
    void Start()
    {
        leafList = new List<GameObject>();
        leafFloorCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addLeaf()
    {
        GameObject.Instantiate(leafPrefab, new Vector3(transform.position.x + Random.Range(-1f,1f), transform.position.y, transform.position.z), Quaternion.identity);
    }
}
