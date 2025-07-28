using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character : MonoBehaviour
{
    private void RegisterComponents() {
        foreach(ICharacterComponent component in GetComponentsInChildren<ICharacterComponent>()) {
            component.ParentCharacter = this;
        }
    }
}
