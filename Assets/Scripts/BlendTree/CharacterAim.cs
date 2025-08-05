using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAim : MonoBehaviour, ICharacterComponent {
    public Character ParentCharacter { get; set; }
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private FloatDamper aimDamper;

    private Animator anim;

    private void Awake() {
        anim.GetComponent<Animator>();
    }
    public void OnAnim(InputAction.CallbackContext ctx) {
        if(!ctx.started && !ctx.canceled) return;

        aimCamera?.gameObject.SetActive(ctx.started);
        ParentCharacter.IsAiming = ctx.started;
        aimDamper.TargetValue = ctx.started ? 1f : 0f;
    }

    private void Update() {
        aimDamper.Update();
        anim.SetLayerWeight(1, aimDamper.CurrentValue);
    }
}
