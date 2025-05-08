using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public GameObject[] playerSkins;
    public int selectedSkinIndex = 0;

    public void SelectSkin(int index)
    {
        selectedSkinIndex = index;

        for (int i = 0; i < playerSkins.Length; i++)
        {
            playerSkins[i].SetActive(i == selectedSkinIndex);
        }

        PlayerPrefs.SetInt("SelectedSkin", selectedSkinIndex);
    }

    void Start()
    {
        int savedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        SelectSkin(savedSkin);
    }
}

