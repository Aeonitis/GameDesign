using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class GameManager_JS : Manager<GameManager_JS> {

    #region Common Code

	private float rotationSpeed = 1;
	private Vector3 rotationDirection;

	// Native Container to use with TransformAccess data
	TransformAccessArray blockAccessArray;
	// Custom Job
	BlockControllerJob blockJob;
	// Interact and control Job
	JobHandle blockJobHandle;

	private void Start() {
		blockAccessArray = new TransformAccessArray(0);
	}

    private void Update()
    {
        updateFPS();

		blockJobHandle.Complete();

        if(Input.GetKeyDown("space")) {
            if(null != firstBlock) {
                Destroy(firstBlock);
            }
            addMoreBlocks();
        }

		blockJob = new BlockControllerJob {
			upperBounds = upperBound,
			lowerBounds = lowerBound,
			leftBounds = leftBound,
			rightBounds = rightBound,
			rotateSpeed = rotationSpeed,
			rotationDirection = rotationDirection,
			deltaTime = Time.deltaTime
		};

		blockJob.Schedule(blockAccessArray);
		JobHandle.ScheduleBatchedJobs();
    }

	private void addMoreBlocks() {
		blockAccessArray.capacity = blockAccessArray.length + numberToSpawn;

		rotationDirection = rotDirections[Random.Range(0, 4)];

		for(int i=0; i < numberToSpawn; i++) {
			float xPosition = Random.Range(leftBound, rightBound);
			float yPosition = Random.Range(upperBound, lowerBound);
			float zPosition = Random.Range(0, 8);

			Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);
			Quaternion spawnRotation = Quaternion.identity;

			GameObject newBlock = Instantiate(PS_Block, spawnPosition, spawnRotation);

			blockAccessArray.Add(newBlock.transform);
		}

		numBlocks += numberToSpawn;
	}

	private void OnDisable() {
		// Ensure all jobs are finished before class is disabled
		blockJobHandle.Complete();
		// Free up memory
		blockAccessArray.Dispose();
	}
   
    void updateFPS()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0f / updateRate;

            fpsDisplay.text = string.Format("FPS: {0}", fps);
            blockCountDisplay.text = string.Format("Blocks: {0}", numBlocks);
        }
    }

    [Header("Screen Bounds")]
    public float upperBound = -4.5f;
    public float lowerBound = 3.5f;
    public float leftBound = -9.17f;
    public float rightBound = 9.0f;

    [Header("Block Prefab")]
    public GameObject PS_Block;
    public GameObject firstBlock;

    [Header("Block Spawning")]
    public int numberToSpawn;

    [Header("Stats")]
    public Text blockCountDisplay;
    public Text fpsDisplay;

    public readonly Vector3[] rotDirections = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    private int numBlocks;

    private int frameCount = 0;
    private float dt = 0.0f;
    private float fps = 0.0f;
    private readonly float updateRate = 4.0f;  // 4 updates per sec.

    #endregion

}
