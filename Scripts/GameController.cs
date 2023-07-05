using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What is currently happening in the game
/// </summary>

public enum GameState { FreeRoam, Dialog, Battle}
/// <summary>
/// Controls aspects of the game
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState state;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if(state == GameState.Dialog)
            {
              state = GameState.FreeRoam;
            }
            
        };
    }

    private void Update()
    {
        if(state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if(state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if(state == GameState.Battle)
        {

        }
    }
}
