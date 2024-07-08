using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject[] views;

    public const float MinBorder = -2.3f;
    public const float MaxBorder = 1.65f;

    public static UnityAction<Platform> OnTriggered;

    public enum PlatformType
    {
        empty,
        score,
        coin,
        bomb,
    }


    public PlatformType Type;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * JuicyBouncingBallGame.Speed);

        if(transform.position.y < -9f)
        {
            SetRandom();
            transform.Translate(Vector2.up * 18f);
            transform.position = new Vector2(Random.Range(MinBorder, MaxBorder), transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggered?.Invoke(this);

        if(Type != PlatformType.bomb)
        {
            SetType(PlatformType.empty);
        }
    }

    public void SetRandom()
    {
        var chance = Random.Range(0, 100);

        if (chance < 20) SetType(PlatformType.coin);
        else if (chance < 50) SetType(PlatformType.bomb);
        else if (chance < 90) SetType(PlatformType.score);
        else SetType(PlatformType.empty);
    }

    public void SetType(PlatformType _type)
    {
        Type = _type;

        for(int i = 0; i < views.Length; i++)
        {
            views[i].gameObject.SetActive(i == (int)Type);
        }
    }
}
