using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifetime = 3f;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 위쪽으로 이동
        rb.linearVelocity = Vector2.up * speed;
        
        // 일정 시간 후 자동 삭제
        Destroy(gameObject, lifetime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // 적과 충돌 시
        if (other.CompareTag("Enemy"))
        {
            // 적 파괴
            Destroy(other.gameObject);
            // 총알도 파괴
            Destroy(gameObject);
        }
        
        // 화면 경계와 충돌 시
        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}