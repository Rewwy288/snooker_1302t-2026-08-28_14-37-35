using UnityEngine;

public class SettingsMenuToggle : MonoBehaviour
{
    // ใส่ตัว Panel ที่ซ้อน Slider ไว้ลงในช่องนี้
    public GameObject settingsPanel;

    // ฟังก์ชันสำหรับสลับเปิด-ปิด (Toggle)
    public void ToggleSettings()
    {
        if (settingsPanel != null)
        {
            // ถ้า Panel เปิดอยู่จะสั่งปิด / ถ้าปิดอยู่จะสั่งเปิด
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
    }
}