using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButtonComponent : MonoBehaviour
{
	public int index = 0;
	public GridButtonType type = GridButtonType.Sound;
	public StyleHUD hud;

	public void Execute()
	{
		if (type == GridButtonType.Style) hud.SetStyle(index);
		if (type == GridButtonType.Sound) hud.setSound(index);
	}

}

public enum GridButtonType
{
	Style,
	Sound
}