using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlatform : MonoBehaviour
{
    [SerializeField] private PlatformTrigger empty;
    [SerializeField] private PlatformTrigger bomb;

    public void LoadCell()
    {
        bomb.Enable();
        empty.Enable();

        if(Random.Range(0, 2) == 1)
        {
            empty.transform.position = new Vector2(Random.Range(0.5f, Sector.MaxBorder), empty.transform.position.y);
            bomb.transform.position = new Vector2(Random.Range(Sector.MinBorder, -0.5f), bomb.transform.position.y);
        }
        else
        {
            bomb.transform.position = new Vector2(Random.Range(0.5f, Sector.MaxBorder), bomb.transform.position.y);
            empty.transform.position = new Vector2(Random.Range(Sector.MinBorder, -0.5f), empty.transform.position.y);
        }
    }
}
