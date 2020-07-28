using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoDisplay;

    [SerializeField]
    private GameObject _coin;

    public void UpdateAmmo(int count)
    {
        _ammoDisplay.text = "Ammo : " + count.ToString();
    }

    public void showCoin()
    {
        _coin.SetActive(true);
    }

    public void removeCoin()
    {
        _coin.SetActive(false);
    }
}
