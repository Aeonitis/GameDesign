using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct BlockControllerData : IComponentData {
	public float rotateSpeed;
	public Vector3 rotationDirection;
}

public class BlockControllerComponent : ComponentDataWrapper<BlockControllerData> {}
