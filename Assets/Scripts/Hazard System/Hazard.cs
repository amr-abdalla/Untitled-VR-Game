using UnityEngine;
using UnityEngine.Events;

public abstract class Hazard : MonoBehaviour
{
	#region serializable variables
	[SerializeField] private float expiryTime;
	[SerializeField] private DayPeriod availablePeriod;
	[SerializeField] private EnvironmentType[] availableEnvironments;

	#endregion

	#region private variables
	private float timeStarted;

	#endregion

	#region events
	public UnityAction BeforeInvoke;
	public UnityAction AfterInvoke;
	public UnityAction OnApplyEffects;
	public UnityAction OnEndEffects;

	#endregion

	#region private methods
	private bool CheckExpired()
	{
		return Time.time > (expiryTime + timeStarted);
	}

	protected virtual void ApplyEffects()
	{
		OnApplyEffects();
	}

	protected virtual void EndEffects()
	{
		OnEndEffects();
	}

	#endregion

	#region public API
	public bool CheckAvailable()
	{
		return !CheckExpired(); // && currentPeriod == availablePeriod && availableEnvironments.Any(environment == currentEnvironment)
	}

	public void Invoke()
	{
		BeforeInvoke();
		timeStarted = Time.time;
		ApplyEffects();
		AfterInvoke();
	}

	#endregion

}
