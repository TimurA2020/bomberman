using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Bomb,
        Blast,
        Speed,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.Bomb:
                player.GetComponent<BombController>().BombIncreas();
                break;

            case ItemType.Blast:
                player.GetComponent<BombController>().explosionRadius++;
                break;

            case ItemType.Speed:
                player.GetComponent<MovementController>().speed++;
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            OnItemPickup(other.gameObject);
        }
    }

}
