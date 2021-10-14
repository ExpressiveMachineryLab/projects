﻿using System.Collections;
using System.Collections.Generic;
using TE;
using UnityEngine;
using UnityEngine.UI;

public class EmitterTangible : MonoBehaviour {
	public SelectedElementType type = SelectedElementType.Emitter;
	public string id = "";
	public ElemColor color;
	public GameObject ballPrefab;
	public string launchKey;

	private Transform firePoint;
	private Animator emitterAnimator;
	
	private SoundManager soundMan;
	private CountLogger logger;

	public Tangible tangible;
	public Vector2 pos;

	void Start() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
		emitterAnimator = GetComponent<Animator>();
		logger = FindObjectOfType<CountLogger>();

		Transform[] gettingFirePoint = GetComponentsInChildren<Transform>();
		foreach (Transform fire in gettingFirePoint) {
			if (fire.gameObject.name == "FirePoint") firePoint = fire;
		}

		//Create a unique ID
		if (id == "") {
			id = "2" + (int)color;
			RandomString randomstring = new RandomString();
			id += randomstring.CreateRandomString(5);
		} else if (!id[0].Equals("2".ToCharArray()[0])) {
			id = "2" + id;
		}

		InspectorData idata = this.GetComponent<InspectorData>();
		idata.colorReplace = color.ToString();

	}

	private void Update()
	{
		if (tangible != null)
		{
			pos = tangible.Pos;
		}
		if (Input.GetButtonDown(launchKey))
        {
			ShootBall();
        }
    }
    void OnEnable() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
	}

	public void ShootBall() {
		GameObject newBall = FindObjectOfType<GameManager>().AssignBall(ballPrefab);
		newBall.transform.position = firePoint.position;
		newBall.transform.rotation = firePoint.rotation;
		newBall.GetComponent<Ball>().ResetVelocity();

		emitterAnimator.SetTrigger("Shoot");
	}
	public void BecomeCloneOf(GameObject emitterModel) {
		color = emitterModel.GetComponent<EmitterTangible>().color;
		ballPrefab = emitterModel.GetComponent<EmitterTangible>().ballPrefab;
		launchKey = emitterModel.GetComponent<EmitterTangible>().launchKey;
		GetComponent<Animator>().runtimeAnimatorController = emitterModel.GetComponent<Animator>().runtimeAnimatorController;
		GetComponent<SpriteRenderer>().sprite = emitterModel.GetComponent<SpriteRenderer>().sprite;

		transform.position = emitterModel.transform.position;
		transform.rotation = emitterModel.transform.rotation;
	}

	public string BirdToSO() {
		string SOstring = id;
		SOstring += "," + transform.position.x.ToString("0.00") + "," + transform.position.y.ToString("0.00");
		SOstring += "," + transform.rotation.eulerAngles.z.ToString("0.00");

		return SOstring;
	}

	public void BirdFromSO(string SObird) {
		string[] SOstring = SObird.Split(new[] { "," }, System.StringSplitOptions.None);

		id = SOstring[0];
		transform.position = new Vector3(float.Parse(SOstring[1]), float.Parse(SOstring[2]), 0);
		transform.eulerAngles = new Vector3(0, 0, float.Parse(SOstring[3]));
	}
}
