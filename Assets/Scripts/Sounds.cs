using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource lose;
    [SerializeField] private AudioSource boost;
    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource score;
    [SerializeField] private AudioSource win;

    public static Sounds Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void SetVolume(float vol)
    {
        click.volume = vol;
        lose.volume = vol;
        boost.volume = vol;
        coin.volume = vol;
        score.volume = vol;
        win.volume = vol;
    }

    public void Click()
    {
        click.Play();
    }

    public void Lose()
    {
        lose.Play();
    }

    public void Boost()
    {
        boost.Play();
    }

    public void Coin()
    {
        coin.Play();
    }

    public void Score()
    {
        score.Play();
    }

    public void Win()
    {
        win.Play();
    }
}
