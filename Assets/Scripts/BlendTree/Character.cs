using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character : MonoBehaviour
{
    Transform lockTarget;

    public Transform LockTarget
    {
        get;
        set;
    }

    private void Awake()
    {
        RegisterComponents();
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponent characterComponent in GetComponentsInChildren<ICharacterComponent>())
        {
            characterComponent.ParentCharacter = this;
        }
    }
}
