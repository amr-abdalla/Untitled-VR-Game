using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HazardManager : MonoBehaviour
{
	#region serializable variables
	[SerializeField] private Hazard[] hazards;

	#endregion

	#region events
	public UnityAction<Hazard> BeforeHazardInvoke;
	public UnityAction<Hazard> AfterHazardInvoke;

	#endregion

	#region public API
	public IEnumerable<Hazard> GetAvaialableHazards()
	{
		return hazards.Where(hazard => hazard.CheckAvailable());
	}

	public void InvokeHazard(Hazard hazard)
	{
		BeforeHazardInvoke(hazard);
		hazard.Invoke();
		AfterHazardInvoke(hazard);
	}

	#endregion
}
