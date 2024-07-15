using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramplinePLatform : MonoBehaviour
{
    [SerializeField] private PlatformTrigger platform;

    public void LoadCell()
    {
        platform.transform.position = new Vector2(Random.Range(Sector.MinBorder, Sector.MaxBorder), platform.transform.position.y);
        platform.Enable();
    }
}
