using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {
	//Hingeコンポーネントを入れる
	private HingeJoint myHingeJoint;

	//初期の傾き
	private float defaultAngle = 20;
	//弾いた時の傾き
	private float flickAngle = -20;

	int Leftfripper = -1;
	int Rightfripper = -1;

	// Use this for initialization
	void Start () {
		//HingeJointコンポーネント取得
		this.myHingeJoint = GetComponent<HingeJoint>();

		//フリッパーの傾きを設定
		SetAngle(this.defaultAngle);
	}

	// Update is called once per frame
	void Update ()
	{
		//左矢印キーを押した時左フリッパーを動かす
		if (Input.GetKeyDown (KeyCode.LeftArrow) && tag == "LeftFripperTag") {
			SetAngle (this.flickAngle);
		}
		//右矢印キーを押した時右フリッパーを動かす
		if (Input.GetKeyDown (KeyCode.RightArrow) && tag == "RightFripperTag") {
			SetAngle (this.flickAngle);
		}

		//矢印キーが離された時フリッパーを元に戻す
		if (Input.GetKeyUp (KeyCode.LeftArrow) && tag == "LeftFripperTag") {
			SetAngle (this.defaultAngle);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow) && tag == "RightFripperTag") {
			SetAngle (this.defaultAngle);
		}
		for (int i = 0; i < Input.touches.Length; i++) {
			if (Screen.width / 2 > Input.touches [i].position.x && tag == "LeftFripperTag" && Input.touches [i].phase == TouchPhase.Began && Leftfripper == -1) {
				SetAngle (this.flickAngle);
				Leftfripper = Input.touches [i].fingerId;
			}
			if (Screen.width / 2 < Input.touches [i].position.x && tag == "RightFripperTag" && Input.touches [i].phase == TouchPhase.Began && Rightfripper == -1) {
				SetAngle (this.flickAngle);
				Rightfripper = Input.touches [i].fingerId;
			}
			if (tag == "LeftFripperTag" && Input.touches [i].phase == TouchPhase.Ended && Input.touches [i].fingerId == Leftfripper) {
				SetAngle (this.defaultAngle);
				Leftfripper = -1;
			}
			if (tag == "RightFripperTag" && Input.touches [i].phase == TouchPhase.Ended && Input.touches [i].fingerId == Rightfripper) {
				SetAngle (this.defaultAngle);
				Rightfripper = -1;
			}
		}
}

	//フリッパーの傾きを設定
	public void SetAngle (float angle){
		JointSpring jointSpr = this.myHingeJoint.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoint.spring = jointSpr;
	}
}