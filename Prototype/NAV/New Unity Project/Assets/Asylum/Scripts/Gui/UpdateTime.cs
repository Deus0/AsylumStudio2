using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour {
	public FadeScreen MyFadeScreen;
	public int DayNumber = 1;
	public float TimeStarted = 0;
	public float MinutesPerDay = 20.0f;
	public float SecondsPerDay = 20.0f;
	// Use this for initialization
	void Start () {
		TimeStarted = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float SecondsPassed = (Time.time - TimeStarted);
		int MinutesPassed = Mathf.FloorToInt(SecondsPassed / 60.0f);
		SecondsPassed = SecondsPassed - MinutesPassed * 60f;
		gameObject.GetComponent<Text> ().text = "Day: " + DayNumber.ToString() + "\n" + 
												"Time: " + MinutesPassed + "m" + (Mathf.RoundToInt(SecondsPassed)) + "s";
		if (MinutesPassed*60f+SecondsPassed >= MinutesPerDay*60f+SecondsPerDay) {
			TimeStarted = Time.time;
			DayNumber++;
			MyFadeScreen.ResetFade(DayNumber);
		}
	}
}
