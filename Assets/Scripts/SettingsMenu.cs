using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer mixer;
    public GameObject bricks;
    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }

    public void ToggleBricks()
    {
        bricks.SetActive(bricks.activeInHierarchy == false);
    }
       
}
