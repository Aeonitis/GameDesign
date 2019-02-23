using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;

public class GameManager_Pure : Manager<GameManager_Pure> {

#region Common Code

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
 
    private EntityManager manager;
    private float rotationSpeed = 1;

    private void Start() {
        manager = World.Active.GetOrCreateManager<EntityManager>();
    }

	
    private void Update()
    {
        updateFPS();

        if(Input.GetKeyDown("space")) {
            if(null != firstBlock) {
                Destroy(firstBlock);
            }
            addMoreBlocks();
        }
    }

    private void addMoreBlocks() {

        // Initialise size of native container to number of spawns, telling system to allocate memory that will be shortlived by passing in Allocator.Temp
        NativeArray<Entity> entities = new NativeArray<Entity>(numberToSpawn, Allocator.Temp);
        // Keep in mind, this manager is not the one instantiating gameobjects
        manager.Instantiate(PS_Block, entities);

        for (int i = 0; i < numberToSpawn; i++)
        {
            float xPosition = Random.Range(leftBound, rightBound);
            float yPosition = Random.Range(upperBound, lowerBound);
            float zPosition = Random.Range(0, 8);

            Position spawnPos = new Position { Value = new Vector3(xPosition, yPosition, zPosition) };
            Rotation spawnRot = new Rotation { Value = Quaternion.identity };
            BlockControllerData blockData = new BlockControllerData {
                rotateSpeed = rotationSpeed,
                rotationDirection = rotDirections[Random.Range(0, 4)]
            };

            // Set values of component data, so that calls made set the pos/rot/blockdata values of the current entity[i]
            manager.SetComponentData(entities[i], spawnPos);
            manager.SetComponentData(entities[i], spawnRot);
            manager.SetComponentData(entities[i], blockData);

        }

        // Clean up
        entities.Dispose();
        numBlocks += numberToSpawn;
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


}
