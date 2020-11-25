using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButtonComponent : MonoBehaviour
{
	public int index = 0;
	public GridButtonType type = GridButtonType.Sound;
	public StyleHUD hud;

	private CountLogger logger;

	void Start()
	{
		logger = FindObjectOfType<CountLogger>();
	}

	public void Execute()
	{
		if (logger != null) logger.IncSoundBankClicks();

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