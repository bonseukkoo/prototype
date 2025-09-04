using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float boundary = 4.5f;
    
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    
    private Rigidbody2D rb;
    private float nextFireTime = 0f;
    private Camera mainCamera;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        
        // Fire point를 플레이어 앞쪽에 설정
        if (firePoint == null)
        {
            GameObject firePointObj = new GameObject("FirePoint");
            firePointObj.transform.SetParent(transform);
            firePointObj.transform.localPosition = new Vector3(0, 0.5f, 0);
            firePoint = firePointObj.transform;
        }
    }
    
    void Update()
    {
        HandleMovement();
        HandleShooting();
    }
    
    void HandleMovement()
    {
        // 키보드 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // 터치 입력 (모바일용)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            
            Vector3 direction = (touchPosition - transform.position).normalized;
            horizontal = direction.x;
            vertical = direction.y;
        }
        
        // 이동
        Vector2 movement = new Vector2(horizontal, vertical) * moveSpeed;
        rb.linearVelocity = movement;
        
        // 화면 경계 제한
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -boundary, boundary);
        pos.y = Mathf.Clamp(pos.y, -boundary, boundary);
        transform.position = pos;
    }
    
    void HandleShooting()
    {
        // 발사 입력 (스페이스바 또는 터치)
        bool shouldShoot = Input.GetKey(KeyCode.Space) || 
                          (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        
        if (shouldShoot && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}