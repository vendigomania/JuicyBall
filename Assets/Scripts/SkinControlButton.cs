using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinControlButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private int cost;
    [SerializeField] private GameObject coin;
    [SerializeField] private Text costText;

    public static Sprite choicedSkin;

    JuicyBouncingBallGame main;

    private bool isBuyed
    {
        get => PlayerPrefs.GetInt(icon.sprite.name, 0) == 1;
        set => PlayerPrefs.SetInt(icon.sprite.name, value ? 1 : 0);
    }

    void Start()
    {
        main = FindAnyObjectByType<JuicyBouncingBallGame>();
        PlayerPrefs.SetInt("1", 1);

        if (icon.sprite.name == "1") Click();
    }

    private void Update()
    {
        costText.text = isBuyed ? (choicedSkin == icon.sprite? "CHOICED" : "CHOICE") : cost.ToString();
        coin.SetActive(!isBuyed);
    }

    public void Click()
    {
        Sounds.Instance.Click();
        if (isBuyed)
        {
            choicedSkin = icon.sprite;
        }
        else
        {
            if(main.Coins >= cost)
            {
                main.Coins -= cost;
                choicedSkin = icon.sprite;
                isBuyed = true;
            }
        }
    }
}
