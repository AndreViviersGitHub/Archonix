using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapButtons : MonoBehaviour {

    public GameObject bCanvas;

	public void ButtonAccept()
    {
        Application.LoadLevel(2);
    }

    public void ButtonDecline()
    {
        bCanvas.SetActive(false);
    }
}
