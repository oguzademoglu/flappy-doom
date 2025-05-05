using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float moveSpeed = 2f;
    public float deathYThreshold = -16f; // Aşağıya düşme sınırı

    public int maxHealth = 3;
    private int currentHealth;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    [System.Obsolete]
    void Update()
    {
        // Mouse click ya da ekrana dokunma
        if (Input.GetMouseButtonDown(0)) // Mobilde de çalışır
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // Yere düştüyse öl
        if (transform.position.y < deathYThreshold)
        {
            GameManager.Instance.GameOver();
        }
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        // Sabit sağa doğru hareket
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HurtZone"))
            TakeDamage(1, other.transform);
    }

    [System.Obsolete]
    public void TakeDamage(int damage, Transform hitSource)
    {
        currentHealth -= damage;
        Debug.Log($"currentHealth: {currentHealth}");

        // // Küçük bir geri tepme uygulayalım
        // Vector2 knockback = new(20f, 5f); // Hafif sola ve yukarı
        // rb.velocity = Vector2.zero; // Eski hızı sıfırla
        // rb.AddForce(knockback, ForceMode2D.Impulse);

        // Tepme yönü: Oyuncudan çarpışan objeye doğru ters yön
        Vector2 direction = (transform.position - hitSource.position).normalized;
        Vector2 knockback = direction * 10f; // Kuvvet isteğe göre ayarlanabilir
        knockback.y = Mathf.Clamp(knockback.y, 5f, 30f); // Yukarı yönlü itme sınırı

        rb.velocity = Vector2.zero; // Önce hız sıfırla
        rb.AddForce(knockback, ForceMode2D.Impulse); // Tepme kuvveti uygula


        // Tepme işleminden sonra:
        FindObjectOfType<CameraShake>().Shake();

        if (currentHealth <= 0)
            GameManager.Instance.GameOver();
    }
}
