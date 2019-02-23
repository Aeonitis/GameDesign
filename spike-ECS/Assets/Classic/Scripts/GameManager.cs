
using UnityEngine;
using UnityEngine.UI;


public class GameManager : Manager<GameManager> 
{
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

    private void addMoreBlocks() {

        // Instantiate at random positions
        for(int i = 0; i < numberToSpawn; i++) {
            float yPosition = Random.Range(upperBound, lowerBound);
            float xPosition = Random.Range(leftBound, rightBound);
            float zPosition = Random.Range(0, 8);

            Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(PS_Block, spawnPosition, spawnRotation);
        }

        // Randomly select/set directions for each block found
        BlockController [] blocks = FindObjectsOfType<BlockController>();
        foreach(BlockController block in blocks) {
            block.rotationDirection = rotDirections[Random.Range(0,4)];
        }

        numBlocks += numberToSpawn;
    }
}
