using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookMovementSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookMovementSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookMovementSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x; 
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }
}
