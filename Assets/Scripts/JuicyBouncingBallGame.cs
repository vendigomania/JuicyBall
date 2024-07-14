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
    [SerializeField] private Image progress;

    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject winTitle;
    [SerializeField] private GameObject loseTitle;
    [SerializeField] private Text endScoreLable;
    [SerializeField] private Text endBestLable;
    [SerializeField] private GameObject nextBtn;

    [Header("Ingame")]
    [SerializeField] private SectorConstructor[] sectors;
    [SerializeField] private Transform bounceRoot;
    [SerializeField] private Transform bounce;

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

    private static int Score
    {
        get => PlayerPrefs.GetInt("Score", 0);
        set => PlayerPrefs.SetInt("Score", value);
    }

    private static int Level
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set => PlayerPrefs.SetInt("Level", value);
    }

    public static int LevelCellsCount = 0;
    public static int LevelMaxCellsCount => 10 + 6 * Level;

    public static float Speed;
    public float ballHeight;

    public const float CellsDistance = 3f;

    public float BallStep => CellsDistance + 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ControlScreen.OnDragAction += OnControl;
        PlatformTrigger.OnTriggered += OnTriggered;

        float coeff = ((float)Screen.height / Screen.width / (1920f / 1080f));
        mainCamera.orthographicSize = 5 * coeff;
        mainCamera.transform.position = new Vector3(0f, coeff > 1? coeff : - coeff, -10f);

        bestLable.text = $"BEST SCORE:{Best}";
        coinsLable.text = Coins.ToString();

        for(int i = 1; i < sectors.Length; i++)
        {
            sectors[i] = Instantiate(sectors[0], sectors[0].transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(boosterTimer > 0f)
        {
            boosterTimer -= Time.deltaTime;
        }

        Speed = Mathf.Lerp(Speed, (2f + LevelCellsCount/10f + (boosterTimer > 0f ? 20f : 0f)) * (PauseActive ? 0f : 1f), Time.deltaTime);

        if (ballHeight > 0f)
        {
            ballHeight -= Speed * Time.deltaTime;

            if (ballHeight < 0f)
            {
                Debug.Log("fall");
                ShowEndScreen(false);
            }
        }

        if (ballHeight > BallStep / 2f)
        {
            float multiplier = (BallStep / 2f - ballHeight % (BallStep / 2f)) / BallStep * 2f;
            bounce.localPosition = Vector2.up * multiplier / 2f;
            bounce.localScale = Vector2.one * (0.7f + 0.3f * multiplier);
        }
        else
        {
            float multiplier = ballHeight % (BallStep / 2f) / BallStep * 2f;
            bounce.localPosition = Vector2.up * multiplier / 2f;
            bounce.localScale = Vector2.one * (0.7f + 0.3f * multiplier);
        }
    }

    public void StartGame()
    {
        BackgroundSetter.Instance.SetRandomBack();

        Sounds.Instance.Click();

        LevelCellsCount = 0;

        startScreen.SetActive(false);
        endScreen.SetActive(false);

        for(int i = 0; i < sectors.Length; i++)
        {
            sectors[i].transform.position = new Vector2(0f, -0.9f + (i * CellsDistance));

            sectors[i].SetSector();
        }

        scoreLable.text = $"SCORE: {Score}";

        tutorial.SetActive(true);
        SetPause(false);

        Vector2 pos = bounceRoot.position;
        pos.x = 0f;
        bounceRoot.position = pos;

        ballHeight = BallStep;
        boosterTimer = 0f;


        SetProgress();
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

    bool PauseActive = true;

    public void SetPause(bool isPause)
    {
        PauseActive = isPause;
        if (isPause)
        {
            Speed = 0f;
        }
    }

    public void NextLevel()
    {
        Level++;

        StartGame();
    }

    private void OnControl(float horizontal)
    {
        tutorial.SetActive(false);

        bounceRoot.Translate(Vector2.right * Time.deltaTime * horizontal * 0.5f);

        Vector2 pos = bounceRoot.position;
        pos.x = Mathf.Clamp(pos.x, -2f, 2f);
        bounceRoot.position = pos;
    }

    private void OnTriggered(PlatformTrigger platform)
    {
        Debug.Log($"Triggred {platform.Type}");
        switch(platform.Type)
        {
            case PlatformTrigger.PlatformType.bomb:
                if (boosterTimer > 0f) return;

                ShowEndScreen(false);
                break;
            case PlatformTrigger.PlatformType.finish:
                ShowEndScreen(true);
                break;
            default:

                LevelCellsCount++;
                SetProgress();

                ballHeight = BallStep;

                Score++;
                scoreLable.text = $"SCORE: {Score}";

                if (platform.Type == PlatformTrigger.PlatformType.empty)
                {
                    Sounds.Instance.Score();
                }
                else
                {
                    Sounds.Instance.Coin();
                    Coins++;
                }
                break;
        }
    }

    private void ShowEndScreen(bool isWin)
    {
        Sounds.Instance.Lose();
        SetPause(true);


        endScreen.SetActive(true);
        winTitle.SetActive(isWin);
        loseTitle.SetActive(!isWin);
        
        
        if (isWin)
        {
            endScoreLable.text = $"LEVEL: {Level}";
            endBestLable.text = $"SCORE: {Score}";
        }
        else
        {
            if (Score > Best)
            {
                Best = Score;
                endBestLable.text = $"NEW RECORD!";
            }
            else
            {
                endBestLable.text = $"BEST SCORE: {Best}";
            }

            endScoreLable.text = $"SCORE: {Score}";

            Score = 0;
        }

        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(LevelCellsCount > LevelMaxCellsCount / 3 * i || isWin);
        }

        nextBtn.SetActive(isWin);
    }

    private void SetProgress()
    {
        progress.fillAmount = LevelCellsCount / 1f / LevelMaxCellsCount;
    }
}
