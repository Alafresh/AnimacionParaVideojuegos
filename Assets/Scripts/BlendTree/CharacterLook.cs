using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharacterLook : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private Transform target;
    [SerializeField] private FloatDamper horizontalDamper;
    [SerializeField] private FloatDamper verticalDamper;
    [SerializeField] private float horizontalRotationSpeed;
    [SerializeField] private float verticalRotationSpeed;
    [SerializeField] private Vector2 verticalRotationLimit;
    private float verticalRotation;

    public void OLook(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();
        inputValue = inputValue / new Vector2(Screen.width, Screen.height);
        horizontalDamper.TargetValue = inputValue.x;
        verticalDamper.TargetValue = inputValue.y;
    }

    private void Update()
    {
        horizontalDamper.Update();
        verticalDamper.Update();
        ApplyLookRotation();
    }

    private void ApplyLookRotation()
    {
        if(target == null)
        {
            throw new NullReferenceException("Look target not assigned");
        }
        if (ParentCharacter.LockTarget != null)
        {

        }
        target.RotateAround(target.position, 
            transform.up, 
            horizontalDamper.CurrentValue * horizontalRotationSpeed * 360 * Time.deltaTime);

        verticalRotation += verticalDamper.CurrentValue * 
            verticalRotationSpeed * 360 * Time.deltaTime;

        verticalRotation = Math.Clamp(verticalRotation, verticalRotationLimit.x, verticalRotationLimit.y);
        Vector3 euler = target.localEulerAngles;
        euler.x = verticalRotation;
        target.localEulerAngles = euler;
    }

    public Character ParentCharacter { get; set; }
}

