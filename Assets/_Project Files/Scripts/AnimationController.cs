using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (characterController == null || animator == null)
            return;

        float speedForward = Vector3.Dot(characterController.velocity, transform.forward);
        float speedRight = Vector3.Dot(characterController.velocity, transform.right);

        animator.SetFloat("SpeedForward", speedForward);
        animator.SetFloat("SpeedRight", speedRight);
    }
}
