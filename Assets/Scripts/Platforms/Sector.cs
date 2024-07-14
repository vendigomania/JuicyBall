using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sector : MonoBehaviour
{
    public const float MinBorder = -2.3f;
    public const float MaxBorder = 1.65f;

    public UnityEvent OnSetStart;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * JuicyBouncingBallGame.Speed);

        if(transform.position.y < -9f)
        {
            OnSetStart?.Invoke();

            transform.Translate(Vector2.up * 18f);
        }
    }
}
