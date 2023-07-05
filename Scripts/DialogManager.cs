using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the Dialog
/// </summary>
public class DialogManager : MonoBehaviour
{
    /// <summary>
    /// Where the dialog is
    /// </summary>
    [SerializeField] GameObject dialogBox;
    /// <summary>
    /// The text of the dialog
    /// </summary>
    [SerializeField] Text dialogText;
    /// <summary>
    /// How fast the letters appear in the dialog box
    /// </summary>
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    /// <summary>
    /// Shows dialog
    /// </summary>
    /// <param name="dialog">the dialog being shown</param>
    /// <returns></returns>
    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }
    /// <summary>
    /// Updates
    /// </summary>
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                OnHideDialog?.Invoke();
            }
        }
    }

    /// <summary>
    /// Types the dialog 
    /// </summary>
    /// <param name="line">the line being typed</param>
    /// <returns></returns>
    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping=false;
    }
}
