using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class MonoBehaviourExtensions
{
	#region Public API

	public static Coroutine Wait(this MonoBehaviour monoBehaviour, float seconds)
	{
		return monoBehaviour.StartCoroutine(Wait(seconds));
	}

	public static Coroutine InvokeDelayed(this MonoBehaviour monoBehaviour, UnityAction action, float delayInSeconds)
	{
		return monoBehaviour.StartCoroutine(InnerInvokeDelayed(action, delayInSeconds));
	}

	public static Coroutine InvokeRepeated(this MonoBehaviour monoBehaviour, UnityAction action, float delayInSeconds, float repeatRateInSeconds)
	{
		return monoBehaviour.StartCoroutine(InnerInvokeRepeated(action, delayInSeconds, repeatRateInSeconds));
	}

	#endregion

	#region Private methods
	private static IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

	private static IEnumerator InnerInvokeDelayed(UnityAction action, float delayInSeconds)
	{
		yield return new WaitForSeconds(delayInSeconds);
		action.Invoke();
	}

	private static IEnumerator InnerInvokeRepeated(UnityAction action, float delayInSeconds, float repeatRateInSeconds)
	{
		yield return new WaitForSeconds(delayInSeconds);

		action.Invoke();

		while (true)
		{
			yield return new WaitForSeconds(repeatRateInSeconds);

			action.Invoke();
		}
	}

	#endregion

}
