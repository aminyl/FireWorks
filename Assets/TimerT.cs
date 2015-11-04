using UnityEngine;
using System.Collections;

public class TimerT : MonoBehaviour
{
	float timer;
	float[] timers;
	// Use this for initialization
	void Start ()
	{
		timers = new float[10];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public bool timerCheck (float t)
	{
		return Time.time - timer > t;
	}
		
	public void timerStart ()
	{
		timer = Time.time;
	}
		
	public bool timerCheck (float t, int n)
	{
		return Time.time - timers [n] > t;
	}
		
	public void timerStart (int n)
	{
		timers [n] = Time.time;
	}

	public void chTimerNum (int n)
	{
		timers = new float[n];
	}

}
