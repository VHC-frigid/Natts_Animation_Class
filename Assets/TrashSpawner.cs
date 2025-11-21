using TMPro;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Trash Objects")]
    public GameObject trashPrefab;

    [Header("Dropping Position")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("D/N Cycling activation")]
    public float timeOfDayActivation;
    public bool activatedToday;
    public int day;
    
    void Update()
    {
        var timeOfDay = LightingManager.ins.timeOfDay;

        if (day != LightingManager.ins.day)
        {
            activatedToday = false;
            day = LightingManager.ins.day;
        }

        if (timeOfDay >= timeOfDayActivation && !activatedToday)
        {
            activatedToday = true;
            SpawnTrash();
            SpawnTrash();
            SpawnTrash();
            SpawnTrash();
            SpawnTrash();
        }
    }

    public void SpawnTrash()
    {
        Vector3 RandomSpawningPositions = new Vector3(Random.Range(minX, maxX), 25, Random.Range(minZ, maxZ));
        Instantiate(trashPrefab, RandomSpawningPositions, Quaternion.identity);
    }
}
