using UnityEngine;
using UnityEngine.Audio;

public class InGameSettings : MonoBehaviour
{
    
    public GameObject bricks;

    public void ToggleBricks()
    {
        bricks.SetActive(bricks.activeInHierarchy == false);
    }
       
}
