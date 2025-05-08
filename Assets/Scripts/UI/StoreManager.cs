using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public GameObject[] skinButtons;

    private int coins;
    private int selectedSkin;

    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        UpdateUI();
    }

    public void SelectSkin(int index)
    {
        selectedSkin = index;
        PlayerPrefs.SetInt("SelectedSkin", selectedSkin);
        PlayerPrefs.Save();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coin: " + coins;

        for (int i = 0; i < skinButtons.Length; i++)
        {
            skinButtons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                (i == selectedSkin) ? "SEÇİLDİ" : "SEÇ";
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

