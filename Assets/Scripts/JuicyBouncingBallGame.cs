using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuicyBouncingBallGame : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private Text bestLable;

    [SerializeField] private GameObject tutorial;
    [SerializeField] private Text scoreLable;

    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private Text coinsLable;

    [SerializeField] private GameObject endScreen;
    [SerializeField] private Text endScoreLable;
    [SerializeField] private Text endBestLable;

    [Header("Ingame")]
    [SerializeField] private Platform[] platforms;
    [SerializeField] private Transform blade;

    private int Coins
    {
        get => PlayerPrefs.GetInt("Coins", 0);
        set
        {
            PlayerPrefs.SetInt("Coins", value);
            coinsLable.text = value.ToString();
        }
    }

    private int Best
    {
        get => PlayerPrefs.GetInt("Best", 0);
        set => PlayerPrefs.SetInt("Best", value);
    }

    private int score;

    public static float Speed;

    // Start is called before the first frame update
    void Start()
    {
        ControlScreen.OnDragAction += OnControl;
        Platform.OnTriggered += OnTriggered;

        mainCamera.orthographicSize = 5 * ((float)Screen.width / Screen.height / (1080f / 1920f));

        bestLable.text = $"BEST SCORE:{Best}";
        coinsLable.text = Coins.ToString();

        for(int i = 1; i < platforms.Length; i++)
        {
            platforms[i] = Instantiate(platforms[0], platforms[0].transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(boosterTimer > 0f)
        {
            boosterTimer -= Time.deltaTime;
        }

        Speed = Mathf.Lerp(Speed, (2f + score/10f + (boosterTimer > 0f ? 20f : 0f)) * (PauseActive ? 0f : 1f), Time.deltaTime);
    }

    public void StartGame()
    {
        Sounds.Instance.Click();

        startScreen.SetActive(false);
        endScreen.SetActive(false);

        for(int i = 0; i < platforms.Length; i++)
        {
            platforms[i].transform.position = new Vector2(
                    Random.Range(Platform.MinBorder, Platform.MaxBorder), 
                    9f + (i * 3f));

            platforms[i].SetRandom();
        }

        score = 0;
        scoreLable.text = $"SCORE: {score}";

        tutorial.SetActive(true);
        SetPause(false);

        Vector2 pos = blade.position;
        pos.x = 0f;
        blade.position = pos;
    }

    float boosterTimer;
    public void BuyBooster()
    {
        if(Coins >= 10)
        {
            Coins -= 10;

            boosterTimer = 4f;
            Sounds.Instance.Boost();

            ShowShop(false);
        }
    }

    public void ShowShop(bool show)
    {
        shopScreen.SetActive(show);
        SetPause(show);

        Sounds.Instance.Click();
    }

    public void ShowSettings(bool show)
    {
        settingsScreen.SetActive(show);
        SetPause(show || startScreen.activeSelf);

        Sounds.Instance.Click();
    }

    bool PauseActive;

    public void SetPause(bool isPause)
    {
        PauseActive = isPause;
        if (isPause)
        {
            Speed = 0f;
        }
    }

    private void OnControl(float horizontal)
    {
        tutorial.SetActive(false);

        blade.Translate(Vector2.right * Time.deltaTime * horizontal * 0.5f);

        Vector2 pos = blade.position;
        pos.x = Mathf.Clamp(pos.x, -2f, 2f);
        blade.position = pos;
    }

    private void OnTriggered(Platform platform)
    {
        switch(platform.Type)
        {
            case Platform.PlatformType.bomb:
                if (boosterTimer > 0f) return;


                Sounds.Instance.Lose();
                SetPause(true);

                endScreen.SetActive(true);

                if(score > Best)
                {
                    Best = score;
                    endBestLable.text = $"NEW RECORD!";
                }
                else
                {
                    endBestLable.text = $"BEST SCORE: {Best}";
                }

                endScoreLable.text = $"SCORE: {score}";
                break;
            case Platform.PlatformType.score:
                score++;
                scoreLable.text = $"SCORE: {score}";

                Sounds.Instance.Score();
                break;
            case Platform.PlatformType.coin:
                Sounds.Instance.Coin();
                Coins++;
                break;
        }
    }
}
