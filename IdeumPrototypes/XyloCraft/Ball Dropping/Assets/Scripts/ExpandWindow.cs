using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandWindow : MonoBehaviour
{
    public GameObject Window;

    public void Expand() {
        Window.SetActive(!Window.activeSelf);
	}

	public void Open() {
		Window.SetActive(true);
	}

	public void Close() {
		Window.SetActive(false);
	}
}
