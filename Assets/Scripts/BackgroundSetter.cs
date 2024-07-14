using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSetter : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private Image[] images;


    public static BackgroundSetter Instance;

    private int index;

    private void Start()
    {
        Instance = this;
    }

    public void SetRandomBack()
    {
        index += Random.Range(0, sprites.Length);
        index %= sprites.Length;

        foreach(var img in images) img.sprite = sprites[index];
    }
}
