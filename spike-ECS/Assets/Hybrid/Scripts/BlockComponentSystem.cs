using UnityEngine;
using Unity.Entities;

public class BlockComponentSystem : ComponentSystem
{
	private struct blockComponent {
		public HybridBlockController blockController;
		public Transform transform;
	}

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

		ComponentGroupArray<blockComponent> blockComponentArray = GetEntities<blockComponent>();

		foreach(blockComponent bc in blockComponentArray) {
			bc.transform.rotation = bc.transform.rotation * Quaternion.AngleAxis(180 * bc.blockController.rotateSpeed * deltaTime, bc.blockController.rotationDirection);
		}
    }
}
