using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private GameObject view;

    public static UnityAction<PlatformTrigger> OnTriggered;

    public enum PlatformType
    {
        empty,
        coin,
        bomb,
        finish,
    }


    public PlatformType Type;

    public void Enable()
    {
        view?.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        view?.SetActive(false);

        OnTriggered?.Invoke(this);
    }

}
