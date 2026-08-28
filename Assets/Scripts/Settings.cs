using UnityEngine;

public static class Settings
{
    public static bool fromSave;
}using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        if (AudioManager.instance != null)
        {
            // ดึงค่าที่เคยเซฟไว้มาตั้งให้ Slider ขยับไปตรงนั้น
            slider.value = AudioManager.instance.LoadCurrentMasterVol();

            // ผูก Event ให้เวลากดเลื่อน Slider จะไปเรียกใช้ AdjustMasterVolume ใน AudioManager
            slider.onValueChanged.AddListener(AudioManager.instance.AdjustMasterVolume);
        }
    }
}