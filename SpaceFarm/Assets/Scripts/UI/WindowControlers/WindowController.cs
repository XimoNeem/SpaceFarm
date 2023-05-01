using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public GameObject WindowObject;
    public bool isInput;

    [SerializeField] private AudioClip _openSound, _closeSound;

    public virtual void ShowWindow(bool state)
    {
        WindowObject.SetActive(state);

        if (!isInput)
        {
            MainContext.Instance.InputManager.IsActive = !state;
        }

        if (_openSound != null)
        {
            if (state)
            {
                SoundManager.Instance.PlaySound(_openSound);
            }
            else
            {
                SoundManager.Instance.PlaySound(_closeSound);
            }
        }
    }
}
