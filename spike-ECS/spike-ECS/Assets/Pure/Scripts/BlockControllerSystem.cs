using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;
using Unity.Collections;

public class BlockControllerSystem : JobComponentSystem {

	private struct BlockControlJob : IJobProcessComponentData<Position, Rotation, BlockControllerData> {

		public float upperBounds;
		public float lowerBounds;
		public float leftBounds;
		public float rightBounds;
		public float deltaTime;

		public void Execute([ReadOnly]ref Position blockPosition, ref Rotation blockRotation, ref BlockControllerData blockControllerData) {
			
			Quaternion rotation	= blockRotation.Value;
			rotation = blockRotation.Value * Quaternion.AngleAxis(180 * blockControllerData.rotateSpeed * deltaTime, blockControllerData.rotationDirection);
			blockRotation.Value = rotation;
		}
	}
	

	// Handle to manage dependencies, makes sure one job completes before another one starts
	protected override JobHandle OnUpdate(JobHandle inputDeps) {
		float delta = Time.deltaTime;

		// Note: No position/rotation/blockcomponentdata, as they're included in filter for our blockcontroljob
		BlockControlJob blockControl = new BlockControlJob {
			upperBounds = GameManager_Pure.Instance.upperBound,
			lowerBounds = GameManager_Pure.Instance.lowerBound,
			leftBounds = GameManager_Pure.Instance.leftBound,
			rightBounds = GameManager_Pure.Instance.rightBound,
			deltaTime = delta
		};

		JobHandle blockControlHandle = blockControl.Schedule(this, inputDeps);

		return blockControlHandle;
	}
}
