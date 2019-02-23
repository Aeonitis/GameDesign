using UnityEngine;
using UnityEngine.Jobs;

/**
A job is:
	- A unit of work that performs a task
	- Can receive parameters
	- Operates on data
	- No reference types
 */

public struct BlockControllerJob : IJobParallelForTransform
{
	public float upperBounds;
	public float lowerBounds;
	public float leftBounds;
	public float rightBounds;

	public float rotateSpeed;
	public Vector3 rotationDirection;

	public float deltaTime;

    public void Execute(int index, TransformAccess transform)
    {
		transform.rotation = transform.rotation * Quaternion.AngleAxis(180 * rotateSpeed * deltaTime, rotationDirection);
    }
}
