using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] PickUpPrefabs;
    public int objectCount = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < objectCount; i++)
        {
            int randomIndex = Random.Range(0, PickUpPrefabs.Length);
            Instantiate(PickUpPrefabs[randomIndex], new Vector3(Random.Range(-8f, 10f), Random.Range(-2.5f, 1.5f), 0f), Quaternion.identity);
        }
    }
}