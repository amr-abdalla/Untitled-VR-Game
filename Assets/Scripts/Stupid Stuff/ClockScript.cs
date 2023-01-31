using System;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
	[SerializeField] private Transform hours;
	[SerializeField] private Transform minutes;
	[SerializeField] private Transform seconds;

	private const float _MaxSeconds = 60f;
	private const float _MaxMinutes = 60f;
	private const float _MaxHours = 24f;

	private float timeOffset = 90f;

	private void Update()
	{
		seconds.rotation = Quaternion.Euler(Mathf.Lerp(0, 360, DateTime.Now.Second / _MaxSeconds) + timeOffset, 0f, -90f);
		minutes.rotation = Quaternion.Euler(Mathf.Lerp(0, 360, DateTime.Now.Minute / _MaxMinutes) + timeOffset, 0f, -90f);
		hours.rotation = Quaternion.Euler(Mathf.Lerp(0, 360, DateTime.Now.Hour / _MaxHours) + timeOffset, 0f, -90f);
	}
}
