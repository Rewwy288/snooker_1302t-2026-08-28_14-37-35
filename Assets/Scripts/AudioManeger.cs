using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioMixer mixer;

    public static AudioManager instance;

    void Awake()
    {
        // ทำ Singleton ป้องกันตัวซ้ำเวลาเปลี่ยน Scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // โหลดค่าความดังเดิมมาตั้งค่าให้ Mixer ทันทีที่เริ่มเกม
        float savedVol = LoadCurrentMasterVol();
        SetMixerVolume(savedVol);
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

    // ฟังก์ชันนี้ไว้ผูกกับ UI Slider ( On Value Changed )
    public void AdjustMasterVolume(float volume)
    {
        SetMixerVolume(volume);

        // เซฟค่า Slider (0.0001 ถึง 1) ไว้ใช้ครั้งถัดไป
        PlayerPrefs.SetFloat("master", volume);
        PlayerPrefs.Save();
    }

    // ฟังก์ชันสำหรับคำนวณแปลงค่าแล้วส่งให้ Mixer
    private void SetMixerVolume(float volume)
    {
        // ป้องกันค่า 0 หรือติดลบ (Log10(0) จะเกิด Error)
        float clampedVol = Mathf.Clamp(volume, 0.0001f, 1f);

        // แปลงค่า 0.0001..1 เป็น -80dB..0dB แล้วส่งเข้า Mixer (ชื่อ Parameter ต้องตรงกับใน Mixer)
        mixer.SetFloat("master", Mathf.Log10(clampedVol) * 20);
    }

    public float LoadCurrentMasterVol()
    {
        // ถ้าไม่เคยเซฟ ให้ใช้ค่าเริ่มต้นเป็น 1 (เสียงดังสุด)
        return PlayerPrefs.GetFloat("master", 1f);
    }
}