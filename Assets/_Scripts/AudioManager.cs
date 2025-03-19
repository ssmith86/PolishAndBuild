using UnityEngine;

public class Audiomanager : MonoBehaviour
{

    public static Audiomanager instance;

    [Header ("Audio Sources")]
    public AudioSource SFX; 
    public AudioSource Ambience; // backgorund
    public AudioSource BSource; //ball source


    [Header ("Audio Clips")]
    public AudioClip Bounce;
    public AudioClip Break;
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

    public void PlayBounce()
    {
        if (BSource != null && Bounce != null)
        {   
            BSource.clip = Bounce;
            BSource.Play();
        }
        else
        {
            Debug.LogWarning("Bounce sound or AudioSource is missing!");
        }
    }

    public void PlayBreak()
    {
        if (SFX != null && Break != null)
        {
            SFX.clip = Break;
            SFX.Play();
        }
        else
        {
            Debug.LogWarning("Bounce sound or AudioSource is missing!");
        }
    }



    }
