using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class MonoBehaviourExtensions
{
	#region Public API

	public static Coroutine Wait(this MonoBehaviour monoBehaviour, float seconds)
	{
		return monoBehaviour.StartCoroutine(Wait(seconds));
	}

	public static Coroutine InvokeDelayed(this MonoBehaviour monoBehaviour, float delayInSeconds, string methodName, params object[] parameters)
	{
		return monoBehaviour.StartCoroutine(InnerInvokeDelayed(monoBehaviour, delayInSeconds, methodName, parameters));
	}

	public static Coroutine InvokeRepeated(this MonoBehaviour monoBehaviour, float delayInSeconds, float repeatRateInSeconds, string methodName, params object[] parameters)
	{
		return monoBehaviour.StartCoroutine(InnerInvokeRepeated(monoBehaviour, delayInSeconds, repeatRateInSeconds, methodName, parameters));
	}

	#endregion

	#region Private methods

	private static IEnumerator InnerInvokeDelayed(MonoBehaviour monoBehaviour, float delayInSeconds, string methodName, params object[] parameters)
	{
		yield return new WaitForSeconds(delayInSeconds);

		MethodInfo methodInfo = GetMethodFromMonoBehaviour(monoBehaviour, methodName, parameters);

		methodInfo.Invoke(monoBehaviour, parameters);
	}

	private static IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

	private static IEnumerator InnerInvokeRepeated(MonoBehaviour monoBehaviour, float delayInSeconds, float repeatRateInSeconds, string methodName, params object[] parameters)
	{
		yield return new WaitForSeconds(delayInSeconds);

		MethodInfo methodInfo = GetMethodFromMonoBehaviour(monoBehaviour, methodName, parameters);

		methodInfo.Invoke(monoBehaviour, parameters);

		while (true)
		{
			yield return new WaitForSeconds(repeatRateInSeconds);

			methodInfo.Invoke(monoBehaviour, parameters);
		}
	}

	private static MethodInfo GetMethodFromMonoBehaviour(MonoBehaviour monoBehaviour, string methodName, params object[] parameters)
	{

		Type[] parameterTypes = parameters.Select(parameter => parameter.GetType()).ToArray();

		return monoBehaviour.GetType().GetMethod(methodName, parameterTypes);
	}

	#endregion

}
