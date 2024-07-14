using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform platform;

    float xMoving = 1f;

    private void Update()
    {
        if (JuicyBouncingBallGame.Speed == 0f) return;

        platform.Translate(Vector2.right * xMoving * Time.deltaTime * 2f);

        if (platform.position.x < Sector.MinBorder) xMoving = 1f;
        else if(platform.position.x > Sector.MaxBorder) xMoving = -1f;
    }
}
