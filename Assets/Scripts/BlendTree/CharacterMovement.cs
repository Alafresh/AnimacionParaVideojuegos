using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour, ICharacterComponent
{/*
    [SerializeField] private FloatDamper speedX;
    [SerializeField] private FloatDamper speedY;*/

    private Animator _animator;
    private int _speedXHash;
    private int _speedYHash;
    public Character ParentCharacter { get; set; }

    private void Awake() {
        _animator = GetComponent<Animator>();
        _speedXHash = Animator.StringToHash("SpeedX");
        _speedYHash = Animator.StringToHash("SpeedY");
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        Vector2 inputValue = ctx.ReadValue<Vector2>();
        //speedX.Value = inputValue.x;
        //speedY.Value = inputValue.y;
    }

    private void Update() {
        //speedX.Update();
        //speedY.Update();
        //_animator.SetFloat(_speedXHash, speedX.Value);
        //_animator.SetFloat(_speedYHash, speedY.Value);
    }
}
