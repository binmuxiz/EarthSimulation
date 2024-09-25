using UnityEngine;

public class BGMManger : MonoBehaviour
{
    public enum Bgm
    {
        Intro = 0,
        IntroStory,
        ChoiceTime,
        GamePlaying,
        Ending,
        Default
    }

    [SerializeField] private bool isSoundPlaying;
    public static BGMManger Instance;
    public AudioClip[] sounds;
    private AudioSource audiosource;
    public Bgm _Bgm;

    void Awake()
    {
        if (Instance == null) Instance = this;
        _Bgm = Bgm.Intro;
        audiosource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }


    private void Update()
    {
        switch (_Bgm)
        {
            case Bgm.Intro:
                audiosource.clip = sounds[0];
                break;
            case Bgm.IntroStory:
                audiosource.clip = sounds[1];
                break;
            case Bgm.GamePlaying:
                audiosource.clip = sounds[2];
                break;
            case Bgm.ChoiceTime:
                audiosource.clip = sounds[3];
                break;
            case Bgm.Ending:
                audiosource.clip = sounds[4];
                break;
           
        }
        
        if(!isSoundPlaying) SoundPlay();
    }

    public void SoundChange(Bgm _Bgm)
    {
        this._Bgm= _Bgm;
        SoundStop();
        isSoundPlaying = false;
    }
    public void SoundPlay()
    {
        audiosource.Play();
        isSoundPlaying = true;
    }

    public void SoundStop()
    {
        audiosource.Stop();
    }
    
}
