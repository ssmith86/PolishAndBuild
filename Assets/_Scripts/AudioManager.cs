using UnityEngine;

public class Audiomanager : MonoBehaviour
{

    public static Audiomanager instance;

    [Header ("Audio Sources")]
    public AudioSource SFX; //bouncince and destroy
    public AudioSource Ambience; // backgorund
    public AudioSource LSource; //ball launch 


    [Header ("Audio Clips")]
    public AudioClip Bounce;
    public AudioClip Break;
    public AudioClip Launch;
    public AudioClip Background;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBackground();
    }

    public void PlayBackground()
    {
        if (Background != null && Ambience != null)
        {
            Ambience.clip = Background;
            Ambience.loop = true;
            Ambience.Play();
        }
    }

    public void playSound(AudioClip clip)
    {
        if(clip != null)
        {
            SFX.PlayOneShot(clip);
        }
    }


}
