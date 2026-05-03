using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public GameObject npcPrefab;
    public int maxNPC = 2;
    public float spawnDelay = 3f;

    private List<GameObject> currentNPC = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            // Bersihkan NPC yang sudah mati
            currentNPC.RemoveAll(npc => npc == null);

            if (currentNPC.Count < maxNPC)
            {
                SpawnNPCNow();
            }
        }
    }

    void SpawnNPCNow()
    {
        GameObject npc = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        currentNPC.Add(npc);
    }
}