using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character : MonoBehaviour
{
    Transform lockTarget;

    public Transform LockTarget
    {
        get => lockTarget;
        set => lockTarget = value;
    }

    private void Awake()
    {
        RegisterComponents();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponent characterComponent in GetComponentsInChildren<ICharacterComponent>())
        {
            characterComponent.ParentCharacter = this;
        }
    }
}
