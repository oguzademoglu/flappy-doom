using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float moveSpeed = 2f;
    public float deathYThreshold = -16f; // Aşağıya düşme sınırı

    public int maxHealth = 3;
    private int currentHealth;

    public int comboCount = 0;


    public float slowMoDuration = 1.5f;
    public float slowTimeScale = 0.3f;
    private bool hasUsedSlowMo = false;
    private bool isSlowingTime = false;


    public Volume sloweMoVolume;


    private Rigidbody2D rb;
    private HealthUI healthUI;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthUI = FindAnyObjectByType<HealthUI>();
        currentHealth = maxHealth;
    }

    [System.Obsolete]
    void Update()
    {
        // Space tuşu ile slow motion (test için)
        if (Input.GetKeyDown(KeyCode.Space) && !isSlowingTime && !hasUsedSlowMo)
        {
            StartCoroutine(SlowTime());
        }


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
        comboCount = 0;
        currentHealth -= damage;
        Debug.Log($"currentHealth: {currentHealth}");

        healthUI.UpdateHearts(currentHealth);

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


    IEnumerator SlowTime()
    {
        isSlowingTime = true;
        hasUsedSlowMo = true;

        sloweMoVolume.weight = 1f;

        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(slowMoDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        sloweMoVolume.weight = 0f;

        isSlowingTime = false;
    }
}
