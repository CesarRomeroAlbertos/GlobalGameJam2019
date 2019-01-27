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

    public bool spawning;

    // Start is called before the first frame update
    void Start()
    {
        leafList = new List<GameObject>();
        leafFloorCount = 0;
        spawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning) StartCoroutine(addLeaf());
    }

    IEnumerator addLeaf()
    {
        spawning = true;
        leafList.Add(GameObject.Instantiate(leafPrefab, new Vector3(transform.position.x + Random.Range(-2f,2f), transform.position.y, transform.position.z), Quaternion.identity));
        yield return new WaitForSeconds(spawnRatio);
        spawning = false;
        yield return null;
    }

    public void removeLeaf(GameObject leaf)
    {
        leafList.Remove(leaf);
        Destroy(leaf);
    }
}
