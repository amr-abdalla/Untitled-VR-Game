using UnityEngine;

public class SwingingArmMotion : MonoBehaviour
{
	public enum movementState
	{
		MovingForward,
		MovingBackward,
		Stopped
	}

	private movementState currentState;

	// Game Objects
	[SerializeField] private GameObject LeftHand;
	[SerializeField] private GameObject RightHand;
	[SerializeField] private GameObject MainCamera;
	[SerializeField] private GameObject ForwardDirection;

	//Vector3 Positions
	[SerializeField] private Vector3 PositionPreviousFrameLeftHand;
	[SerializeField] private Vector3 PositionPreviousFrameRightHand;
	[SerializeField] private Vector3 PlayerPositionPreviousFrame;
	[SerializeField] private Vector3 PlayerPositionCurrentFrame;
	[SerializeField] private Vector3 PositionCurrentFrameLeftHand;
	[SerializeField] private Vector3 PositionCurrentFrameRightHand;

	//Speed
	[SerializeField] private float Speed = 70;
	[SerializeField] private float HandSpeed;
	[SerializeField] private float StopTime = 0.1f;

	private float currentStopTime;

	void Start()
	{
		PlayerPositionPreviousFrame = transform.position; //set current positions
		PositionPreviousFrameLeftHand = LeftHand.transform.position; //set previous positions
		PositionPreviousFrameRightHand = RightHand.transform.position;
		currentState = movementState.Stopped;
	}

	// Update is called once per frame
	void Update()
	{

		// get forward direction from the center eye camera and set it to the forward direction object
		float yRotation = MainCamera.transform.eulerAngles.y;
		ForwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

		// get positons of hands
		PositionCurrentFrameLeftHand = LeftHand.transform.position;
		PositionCurrentFrameRightHand = RightHand.transform.position;

		// position of player
		PlayerPositionCurrentFrame = transform.position;

		// get distance the hands and player has moved from last frame
		var playerDistanceMoved = Vector3.Distance(PlayerPositionCurrentFrame, PlayerPositionPreviousFrame);
		var leftHandDistanceMoved = Vector3.Distance(PositionPreviousFrameLeftHand, PositionCurrentFrameLeftHand);
		var rightHandDistanceMoved = Vector3.Distance(PositionPreviousFrameRightHand, PositionCurrentFrameRightHand);

		if(PositionPreviousFrameLeftHand.y == PositionCurrentFrameLeftHand.y && PositionPreviousFrameRightHand.y == PositionCurrentFrameRightHand.y)
		{
			currentStopTime += Time.deltaTime;

			if(currentStopTime >= StopTime)
			{
				currentState = movementState.Stopped;
				// set previous position of hands for next frame
				PositionPreviousFrameLeftHand = PositionCurrentFrameLeftHand;
				PositionPreviousFrameRightHand = PositionCurrentFrameRightHand;
				// set player position previous frame
				PlayerPositionPreviousFrame = PlayerPositionCurrentFrame;
				return;
			}
			
		}
		else
		{
			currentStopTime = 0;
		}

		if(currentState == movementState.Stopped)
		{
			if(PositionPreviousFrameLeftHand.y > PositionCurrentFrameLeftHand.y || PositionPreviousFrameRightHand.y > PositionCurrentFrameRightHand.y)
			{
				currentState = movementState.MovingForward;
			}
			else
			{
				currentState = movementState.MovingBackward;
			}
		}

		// aggregate to get hand speed
		//HandSpeed = ((leftHandDistanceMoved - playerDistanceMoved) + (rightHandDistanceMoved - playerDistanceMoved));

		if (Time.timeSinceLevelLoad > 1f)
		{
			if(currentState == movementState.MovingForward)
			{
				transform.position += ForwardDirection.transform.forward * Speed * Time.deltaTime;
			}
			else
			{
				transform.position -= ForwardDirection.transform.forward * Speed * Time.deltaTime;
			}
		}

		// set previous position of hands for next frame
		PositionPreviousFrameLeftHand = PositionCurrentFrameLeftHand;
		PositionPreviousFrameRightHand = PositionCurrentFrameRightHand;
		// set player position previous frame
		PlayerPositionPreviousFrame = PlayerPositionCurrentFrame;
	}
}