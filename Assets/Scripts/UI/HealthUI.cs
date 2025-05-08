using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts; // 3 kalp Image objesi
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
