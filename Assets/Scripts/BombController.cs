using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    public GameObject bombPrefab;

    public KeyCode inputKey = KeyCode.Space;
    public int bombNumber = 1;
    private int remainingBombs;
    public float bombExposionTime = 3.5f;
    
    public Explosion explosionPrefab;
    public float explosionDuration = 1.5f;
    public int explosionRadius = 1;
    public LayerMask explosionMask;

    public Tilemap nukableTiles;
    public Destructible destructiblePrefab;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        remainingBombs = bombNumber;
    }    

    // Update is called once per frame
    void Update()
    {
        if (remainingBombs > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        // making sure that the bomb is not between tiles
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        remainingBombs--;
        yield return new WaitForSeconds(bombExposionTime);
        
        // in case it was pushed
        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.EnableRenderer(explosion.beginning);
        explosion.DestroyAfter(explosionDuration);

        
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        
        Destroy(bomb);
        audioManager.PlaySFX(audioManager.explosion);
        remainingBombs++;
    }

    private void Explode(Vector2 position, Vector2 dir, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += dir;

        //making sure the explosion doesn't overlap indestructible tiles
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.EnableRenderer(length > 1 ? explosion.middle : explosion.finish);
        explosion.SetDirection(dir);
        explosion.DestroyAfter(explosionDuration);
        
        Explode(position, dir, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = nukableTiles.WorldToCell(position);
        TileBase tile = nukableTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            nukableTiles.SetTile(cell, null);
        }
    }

    public void BombIncreas()
    {
        bombNumber++;
        remainingBombs++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            other.isTrigger = false;
        }
    }
}
