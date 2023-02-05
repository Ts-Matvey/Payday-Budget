using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PlaySoundOnTriggerEnter : MonoBehaviour
{
    public AudioSource soundEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            soundEffect.Play();
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}