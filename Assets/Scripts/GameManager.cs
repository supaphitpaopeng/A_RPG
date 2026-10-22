using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // ใช้สำหรับ TextMeshPro

public class GameManager : MonoBehaviour
{
    private bool isPlayerDead = false;

    [Header("UI Reference")]
    [SerializeField] private GameObject restartTextObject; // ลาก Text มาใส่ตรงนี้

    void Update()
    {
        // ถ้าผู้เล่นตายแล้ว และกดปุ่ม R บนคีย์บอร์ด
        if (isPlayerDead && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // ฟังก์ชันเรียกเมื่อผู้เล่นตาย
    public void TriggerGameOver()
    {
        isPlayerDead = true;

        // เปิดแสดงข้อความ Text ขึ้นมา
        if (restartTextObject != null)
        {
            restartTextObject.SetActive(true);
        }
    }

    // ฟังก์ชันรีสตาร์ทเกม
    public void RestartGame()
    {
        Time.timeScale = 1f; // คืนค่าเวลาให้ปกติ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}