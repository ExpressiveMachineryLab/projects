using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridButtonComponent : MonoBehaviour
{
	public int index = 0;
	public GridButtonType type = GridButtonType.Sound;
	public StyleHUD hud;

	public void Execute()
	{
		if (type == GridButtonType.Style)
		{
			hud.SetStyle(index);
		}
		if (type == GridButtonType.Sound)
		{
			hud.SetSound(index);
			hud.ResetTextNames();
		}
		
	}

}

public enum GridButtonType
{
	Style,
	Sound
}