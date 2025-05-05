using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool hasScored = false;
    private bool hasCheckedPass = false;


    void Update()
    {
        float playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;

        // // Oyuncunun 10 birim arkasındaysa yok et
        // if (transform.position.x < playerX - 20f)
        // {
        //     Destroy(gameObject);
        // }


        // Oyuncu portalın hizasını geçti ama skoru almadıysa = öldü
        if (!hasCheckedPass && transform.position.x + 7f < playerX)
        {
            hasCheckedPass = true;

            if (!hasScored)
            {
                // Oyuncu geçemedi = Game Over
                GameManager.Instance.GameOver();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasScored && other.CompareTag("Player"))
        {
            hasScored = true;
            GameManager.Instance.AddScore(1); // Skor ekle

            // 1 saniye sonra bu portalı yok et
            Destroy(gameObject, 0.5f);
        }
    }
}

