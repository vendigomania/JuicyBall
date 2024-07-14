using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatform : MonoBehaviour
{
    [SerializeField] private PlatformTrigger[] platforms;

    private float[] xArr = { Sector.MinBorder, -0.48f, Sector.MaxBorder };

    public void LoadCell()
    {
        int start = Random.Range(0, xArr.Length);
        foreach(var platform in platforms)
        {
            platform.Enable();
            platform.transform.position = new Vector2(xArr[start++], platform.transform.position.y);

            start %= xArr.Length;
        }
    }
}
