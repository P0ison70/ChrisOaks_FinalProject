using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the NPCs
/// </summary>
public class NPCController : MonoBehaviour, Interactable
{
    /// <summary>
    /// What they say when the player interacts with them
    /// </summary>
    [SerializeField] Dialog dialog;
    /// <summary>
    /// What happens when the Player interacts with them
    /// </summary>
    public void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
