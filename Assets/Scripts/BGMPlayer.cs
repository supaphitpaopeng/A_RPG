using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    // ทำให้มีตัวเดียวในเกม และไม่ถูกทำลายทิ้งเวลาเปลี่ยน Scene
    private static BGMPlayer instance;

    void Awake()
    {
        // เช็คว่ามี BGMManager ตัวอื่นอยู่แล้วหรือยัง ถ้ามีให้ทำลายตัวซ้ำทิ้ง
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // สั่งห้ามทำลาย GameObject นี้เวลากดเปลี่ยน Scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
}