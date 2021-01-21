using UnityEngine;
using UnityEngine.UI;

public class LoadStats : MonoBehaviour
{
    // scriptable object
    public CharStats charStat;
    public Text nameText;
    public Text attackText;
    public Text defenseText;
    public Text speedText;
    public Text maxHealthText;
    public MeshRenderer currentColor;

    private PlayerControls _playerControls;
    
    void Start()
    {
        DisplayStats();
        charStat.PrintMessage();
        
        _playerControls = new PlayerControls();
        _playerControls.PlayerInputActionMap.Pass.performed += callbackContext => PlayerPassed();
        _playerControls.PlayerInputActionMap.Enable();
    }
    
    void DisplayStats()
    {
        nameText.text = "Name: " + charStat.charName;
        attackText.text = "Attack: " + charStat.attack;
        defenseText.text = "Defense: " + charStat.defense;
        speedText.text = "Speed: " + charStat.speed;
        maxHealthText.text = "Stamina: " + charStat.maxHealth;
        currentColor.material = charStat.newColor;
    }
    
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     charStat.RandomizeStats();
        //     DisplayStats();
        // }
    }

    void PlayerPassed()
    {
        print("****");
        charStat.RandomizeStats();
        DisplayStats();
    }
}
