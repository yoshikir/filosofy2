using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters/Character")]
public class Character : ScriptableObject
{
    public int experience;
    public int evolution;
    public Color[] colors;
    public Sprite[] evolutions;
}
