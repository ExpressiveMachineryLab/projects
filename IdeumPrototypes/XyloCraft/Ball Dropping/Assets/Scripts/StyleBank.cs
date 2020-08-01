﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StyleBank", menuName = "StyleBank")]
public class StyleBank : ScriptableObject {
	public string bankName = "New Style Bank";

	public SoundBank redBank;
	public SoundBank yellowBank;
	public SoundBank blueBank;
	public SoundBank GreenBank;
}