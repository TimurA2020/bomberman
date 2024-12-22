using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer beginning;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer finish;

    public void EnableRenderer(AnimatedSpriteRenderer renderer)
    {
        beginning.enabled = renderer == beginning;
        middle.enabled = renderer == middle;
        finish.enabled = renderer == finish;
    }

    public void SetDirection(Vector2 dir)
    {
        //instead of getting sprites for each direction of the explosion, we just rotate them
        float angle = Mathf.Atan2(dir.y, dir.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
