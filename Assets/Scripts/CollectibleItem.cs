using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int value = 1; // Giá trị của vật phẩm (ví dụ: cộng 1 điểm)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem player có tag là "Player" không
        if (other.CompareTag("Player"))
        {
            // Gọi hàm tăng điểm hoặc thêm vào inventory
            Debug.Log("Player collected item! + " + value);

            // Hủy item sau khi nhặt
            Destroy(gameObject);
        }
    }
}