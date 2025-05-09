using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public GameObject[] skinButtons;

    private bool[] unlockedSkins;
    public int[] skinPrices;

    private int coins;
    private int selectedSkin;



    void Start()
    {
        skinPrices = new int[] { 0, 100, 200 }; // Skin 0 ücretsiz, diğerleri coin istiyor
        coins = PlayerPrefs.GetInt("Coins", 100);
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        // Unlocked skins listesi (PlayerPrefs’ten oku veya oluştur)
        unlockedSkins = new bool[skinPrices.Length];
        for (int i = 0; i < skinPrices.Length; i++)
        {
            unlockedSkins[i] = PlayerPrefs.GetInt("SkinUnlocked_" + i, i == 0 ? 1 : 0) == 1;
        }

        UpdateUI();
    }

    public void SelectSkin(int index)
    {
        if (unlockedSkins[index])
        {
            selectedSkin = index;
            PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
            PlayerPrefs.Save();
            UpdateUI();
        }
        else
        {
            int price = skinPrices[index];
            if (coins >= price)
            {
                coins -= price;
                unlockedSkins[index] = true;
                PlayerPrefs.SetInt("Coins", coins);
                PlayerPrefs.SetInt("SkinUnlocked_" + index, 1);
                PlayerPrefs.SetInt("SelectedSkin", index);
                PlayerPrefs.Save();
                UpdateUI();
            }
            else
            {
                Debug.Log("Yetersiz coin!");
                // UI'da uyarı gösterebilirsin
            }
        }
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coin: " + coins;

        for (int i = 0; i < skinButtons.Length; i++)
        {
            var text = skinButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (unlockedSkins[i])
            {
                text.text = (i == selectedSkin) ? "SEÇİLDİ" : "SEÇ";
            }
            else
            {
                text.text = skinPrices[i] + " COIN";
            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

