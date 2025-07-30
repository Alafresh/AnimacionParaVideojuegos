using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private FloatDamper speedX;
    [SerializeField] private FloatDamper speedY;


    private int _speedXHash;
    private int _speedYHash;
    private Animator _animator;
    public Character ParentCharacter { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _speedXHash = Animator.StringToHash("SpeedX");
        _speedYHash = Animator.StringToHash("SpeedY");
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();
        speedX.TargetValue = inputValue.x;
        speedY.TargetValue = inputValue.y;
    }

    private void Update()
    {
        speedX.Update();
        speedY.Update();
        _animator.SetFloat(_speedXHash, speedX.CurrentValue);
        _animator.SetFloat(_speedYHash, speedY.CurrentValue);
    }
}
