using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Text that indicates communication between to characters
/// </summary>
[System.Serializable]
public class Dialog 
{
    /// <summary>
    /// Stores lines of dialog as a string
    /// </summary>
    [SerializeField] List<string> lines;

    /// <summary>
    /// Gets the lines of dialog
    /// </summary>
    public List<string> Lines
    {
        get { return lines; }
    }
}
