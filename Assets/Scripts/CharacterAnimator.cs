using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private readonly Animator _animator;
    private readonly int _speedHash = Animator.StringToHash("Speed");

    public CharacterAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateSpeed(float speed)
    {
        _animator.SetFloat(_speedHash, speed);
    }
}
