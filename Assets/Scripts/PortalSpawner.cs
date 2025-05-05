using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;
    public float spawnDistance = 10f;
    public float minY = -2f;
    public float maxY = 2f;

    private float lastPortalX = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPortalX = player.position.x;
        SpawnPortal(); // İlk portal
    }

    void Update()
    {
        if (player.position.x + spawnDistance > lastPortalX)
        {
            SpawnPortal();
        }
    }

    void SpawnPortal()
    {
        float randomY = Random.Range(minY, maxY);
        float spawnX = lastPortalX + spawnDistance;

        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
        Instantiate(portalPrefab, spawnPos, Quaternion.identity);

        lastPortalX = spawnX;
    }
}
