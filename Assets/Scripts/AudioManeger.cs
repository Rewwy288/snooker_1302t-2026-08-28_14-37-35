using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgm;

    [SerializeField]
    private AudioSource[] sfx;

    [SerializeField]
    private AudioMixer mixer;
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
            bgm[i].Stop();
    }

    public void PlayBGM(int i)
    {
        StopAllBGM();
        if (i < bgm.Length)
            bgm[i].Play();
    }
    public void PlaySFX(int i)
    {
        if (i < sfx.Length)
            sfx[i].PlayOneShot(sfx[i].clip);
    }

    public void AdjustMasterVolume(float volume)
    {
        mixer.SetFloat("master", volume);
        PlayerPrefs.SetFloat("master", volume);
        PlayerPrefs.Save();
    }

    public float LoadCurrentMasterVol()
    {
        return PlayerPrefs.GetFloat("master", 0f);
    }
}