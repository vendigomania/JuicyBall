using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPlatform : MonoBehaviour
{
    [SerializeField] private Transform platform;

    private void OnEnable()
    {
        platform.position = new Vector2(Random.Range(Sector.MinBorder, Sector.MaxBorder), platform.position.y);
    }
}
