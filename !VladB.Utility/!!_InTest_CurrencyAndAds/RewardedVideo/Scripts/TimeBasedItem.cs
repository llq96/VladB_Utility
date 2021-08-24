using UnityEngine;
using System;
using System.Collections.Generic;

public class TimeBasedItem : MonoBehaviour {
	/*
	[Header("Player Prefs:")]
	public string ppName;
	public int itemIndex = 0;

	[Header ("Settings")]
	public float timeBetweenOperations = 3600;
	public bool available = false;

	[Header ("UI")]
	public UI2DSprite timeIndicator;
	public UILabel timerLabel;

	DateTime currentDate;
	DateTime oldDate;
	TimeSpan difference;

	public delegate void HintAction();
	public static event HintAction OnRewardReceived;

	[Header ("On Video Shown Events")]
	public List<EventDelegate> onVideoShown = new List<EventDelegate>();


	void Awake(){
		UpdateEffect ();
	}

	public void Start () {
		CheckLastTimeOnStart ();
	}
	
	void Update () {
		UpdateIndicator ();
	}

	public void UpdateIndicator(){
		currentDate = DateTime.Now;

		if(!available){
			difference = currentDate.Subtract(oldDate);
			timeIndicator.fillAmount = (float)difference.TotalSeconds / timeBetweenOperations;

			if(timeIndicator.fillAmount >= 1){
				available = true;
				UpdateEffect ();
			}
		}

		UpdateTimer ();
	}

	public virtual void ShowUserDialog(){

	}

	public virtual void OnVideoShown(){

    }
		
	public void UpdateLastTime(){
		PlayerPrefs.SetString($"TBI_time_{ppName}_{itemIndex}", currentDate.ToBinary().ToString());
		oldDate = currentDate;
	}

	public void CheckLastTimeOnStart(){
		currentDate = DateTime.Now;

		long temp = Convert.ToInt64(PlayerPrefs.GetString($"TBI_time_{ppName}_{itemIndex}", currentDate.ToBinary().ToString()));
		oldDate = DateTime.FromBinary(temp);

		if(oldDate != currentDate){
			difference = currentDate.Subtract(oldDate);

			if (difference.TotalSeconds >= timeBetweenOperations) {
				available = true;
				timeIndicator.fillAmount = 1;
			} else {
				available = false;
				timeIndicator.fillAmount = 0;
			}
		}

		UpdateEffect ();
	}

	public virtual void UpdateEffect(){

	}

	void UpdateTimer(){
		if(timerLabel){
			if(!available){
				float time = timeBetweenOperations - (float)difference.TotalSeconds;
				timerLabel.text = RewardedVideoController.SecondsToTime (time);
			}
		}
	}
	*/
}