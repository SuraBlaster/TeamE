using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform player;
    public GameObject chunkPrefab;
    public int viewRadius = 2;
    public int chunkSize = 10;

    private Dictionary<Vector2Int, GameObject> chunks = new Dictionary<Vector2Int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int playerChunk = WorldToChunkPos(player.position);

        for (int x = -viewRadius; x <= viewRadius; x++)
        {
            for (int y = -viewRadius; y <= viewRadius; y++)
            {
                Vector2Int coord = playerChunk + new Vector2Int(x, y);
                if (!chunks.ContainsKey(coord))
                {
                    SpawnChunk(coord);
                }
            }
        }

        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var kvp in chunks)
        {
            if (Mathf.Max(Mathf.Abs(kvp.Key.x - playerChunk.x), Mathf.Abs(kvp.Key.y - playerChunk.y)) > viewRadius)
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }
        foreach (var key in toRemove)
        {
            chunks.Remove(key);
        }
    }

    Vector2Int WorldToChunkPos(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / chunkSize);
        int y = Mathf.FloorToInt(pos.y / chunkSize);
        return new Vector2Int(x, y);
    }

    void SpawnChunk(Vector2Int coord)
    {
        Vector2 worldPos = new Vector2(coord.x * chunkSize, coord.y * chunkSize);
        var chunk = Instantiate(chunkPrefab, worldPos, Quaternion.identity);
        chunks.Add(coord, chunk);
    }
}
