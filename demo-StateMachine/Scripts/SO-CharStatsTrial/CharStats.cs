using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character Creation/Player Units")]
public class CharStats : ScriptableObject
{
    public string charName;
    public int attack;
    public int defense;
    public int speed;
    public int maxHealth;
    public Material newColor;
    
    public void PrintMessage()
    {
        Debug.Log("The " + charName + " character has been loaded.");
    }
    
    public void RandomizeStats() 
    {
        attack = RandomNumberBetweenXAndN(1,20);
        defense = RandomNumberBetweenXAndN(1, 20);
        speed = RandomNumberBetweenXAndN(1, 20);
        maxHealth = RandomNumberBetweenXAndN(1, 20);
    }

    private static int RandomNumberBetweenXAndN(int x, int n)
    {
        return Random.Range(1, 20);
    }
}