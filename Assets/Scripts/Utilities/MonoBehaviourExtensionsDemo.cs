using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourExtensionsDemo : MonoBehaviour
{
	public int i = 0;

	private IEnumerator Start()
	{
		yield return (this.Wait(1));
		Debug.Log("done waiting");

		UnityAction helloWorld1 = () => HelloWorld();
		yield return(this.InvokeDelayed(helloWorld1, 2)); //this will halt everything below until it finishes invoking

		UnityAction helloWorld2 = () => HelloWorld(2);
		yield return(this.InvokeDelayed(helloWorld2, 2)); //this will halt everything below until it finishes invoking

		UnityAction helloWorld3= () => HelloWorld(200);
		this.InvokeDelayed(helloWorld3, 2); //this will NOT halt everything below until it finishes invoking, it just invokes it with a delay

		UnityAction countedHelloWorld = () => CountedHelloWorld();
		var repeatedRoutine = this.InvokeRepeated(countedHelloWorld, 0, 0.5f);

		yield return (this.Wait(3));
		Debug.Log("stopping now");
		StopCoroutine(repeatedRoutine);

	}

	public void HelloWorld()
	{
		Debug.Log("hello world!");
	}

	public void HelloWorld(int num)
	{
		Debug.Log("hello world # " + num);
	}

	public void CountedHelloWorld()
	{
		HelloWorld(i);
		i++;
	}

}
