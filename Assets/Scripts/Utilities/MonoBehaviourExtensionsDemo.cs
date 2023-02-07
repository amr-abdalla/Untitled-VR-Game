using UnityEngine;

public class MonoBehaviourExtensionsDemo : MonoBehaviour
{
	public int i = 0;

	void Start()
	{
		this.Wait(1);
		Debug.Log("done waiting");
		this.InvokeDelayed(1, nameof(HelloWorld));
		this.Wait(1);
		this.InvokeDelayed(1, nameof(HelloWorld), 4);
		this.Wait(1);
		this.InvokeRepeated(2, 2, nameof(CountedHelloWorld));
	}

	public void HelloWorld()
	{
		Debug.Log("hello world!");
	}

	public void HelloWorld(int num)
	{
		Debug.Log("hello world #" + num);
	}

	public void CountedHelloWorld()
	{
		HelloWorld(i);
		i++;
	}

}
