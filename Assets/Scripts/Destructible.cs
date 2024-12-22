using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;

    [Range(0f, 1f)]
    public float spawnChance = 0.2f;

    public GameObject[] spawnableItems;


    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < spawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
