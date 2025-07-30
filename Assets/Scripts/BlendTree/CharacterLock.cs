using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLock : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private float detectionRadius;
    
    public void OnLook(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;

        if(ParentCharacter.LockTarget != null)
        {
            ParentCharacter.LockTarget = null;
            return;
        }

        Collider[] detectedObjects = Physics.OverlapSphere(transform.position,
            detectionRadius, 
            detectionMask);

        if (detectedObjects.Length > 0) return;
    }
    public Character ParentCharacter {  get; set; }
}
