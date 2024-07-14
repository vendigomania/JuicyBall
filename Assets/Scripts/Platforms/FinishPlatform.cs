using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField] private PlatformTrigger[] platforms;

    public void LoadCell()
    {
        foreach(var platform in platforms)
            platform.Enable();
    }
}
