using UnityEngine;

public class CharacterInputFactory : MonoBehaviour
{
    public static ICharacterInput CreateInput(InputType type)
    {
        switch (type)
        {
            case InputType.Player:
                return new PlayerInput();
            case InputType.Enemy:
                return new EnemyInput();
            default:
                return new PlayerInput();
        }
    }
    public enum InputType
    {
        Player,
        Enemy
    }
}
