using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource audioSource;

    private bool isMuted;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt(nameof(isMuted), 0) == 0)
        {
            isMuted = false;
        }
        else
        {
            isMuted = true;
        }
    }

    public void PlayJumpingSound()
    {
        if (isMuted) return;


    }

    public void PlayPassSound()
    {
        if (isMuted) return;


    }

    public bool ToggleMute()
    {
        if (isMuted)
        {
            isMuted = false;
            PlayerPrefs.SetInt(nameof(isMuted), 0);
        }
        else
        {
            isMuted = true;
            PlayerPrefs.SetInt(nameof(isMuted), 1);
        }

        return isMuted;
    }
}
