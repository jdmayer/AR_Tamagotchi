using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationTest : MonoBehaviour
{
	public Animator anim;
	public AnimatorOverrideController animOverrideController;
	public AnimationClip animationClip;

	protected int weaponIndex;

	// Use this for initialization
	void Start()
	{
	}

    private void Update()
    {
		if (Input.GetButtonUp("Jump"))
		{
			Debug.Log("space!");
			anim = GetComponent<Animator>();
			AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController();
			animatorOverrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
			animatorOverrideController["idleState"] = animationClip;
			anim.runtimeAnimatorController = animatorOverrideController;

		}
	}
}
