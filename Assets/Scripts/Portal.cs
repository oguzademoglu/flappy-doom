using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool hasScored = false;
    private bool hasCheckedPass = false;


    void Update()
    {
        float playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;

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

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasScored && other.CompareTag("Player"))
        {
            hasScored = true;

            Rigidbody2D rb = other.attachedRigidbody;
            float fallSpeed = rb.velocity.y;

            int fallBonus = (fallSpeed < -5f) ? 1 : 0;

            PlayerController pc = other.GetComponent<PlayerController>();
            pc.comboCount++;

            // Skor: sadece geçiş ve düşüş bonusu
            int totalScore = 1 + fallBonus;
            GameManager.Instance.AddScore(totalScore);

            // Coin: combo kadar bonus
            int comboCoinBonus = pc.comboCount - 1;
            GameManager.Instance.AddCoins(1 + comboCoinBonus);

            Debug.Log($"Skor: +{totalScore} | Coin: +{1 + comboCoinBonus} (Combo: {pc.comboCount})");
        }
    }
}

