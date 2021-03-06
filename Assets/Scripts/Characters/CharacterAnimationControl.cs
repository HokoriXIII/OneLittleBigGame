﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour {

    public ControlMeta _ControlMeta;
     Animator animator;
    float verticalLast = 0f;
    float horizontalLast = 0f;
   
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	
	void Update () {
        Vector2 AnimatonVerticalHorizontal= CalculateMovingAnimationValues();
       
        float targetVertical = Mathf.Lerp(verticalLast, AnimatonVerticalHorizontal.y, 0.3f);
        float targetHorizontal = Mathf.Lerp(horizontalLast, AnimatonVerticalHorizontal.x, 0.3f);

        animator.SetFloat("Vertical", targetVertical);
       animator.SetFloat("Horizontal", targetHorizontal);
        verticalLast = targetVertical;
        horizontalLast = targetHorizontal;
    }

    public Vector2 CalculateMovingAnimationValues()
    {
        float rotationAngle = _ControlMeta._RotationControl.RotationAngle;
        Vector2 moveDirection = _ControlMeta._MoveControl.moveDirection;
        return (Vector2)(Quaternion.Euler(0, 0, rotationAngle) * moveDirection);
       
    }
    
    public void AnimationAttackStart(float AttackSpeed,AttackItem equipped)
    {
        if (equipped == null)
        {
            //Bare Hands Attack
            return;
        }

        if (equipped._WeaponType == AttackWeaponType.Melee)
        {
            //Melee Satart
            AttackMeleeStart(AttackSpeed);
        }else if(equipped._WeaponType == AttackWeaponType.CrossBow)
        {
            //Cross Bow Start
            CrossBowReadyStart(AttackSpeed);
        }
      
    }
    public void AnimationAttackFinish(float AttackSpeed,AttackItem equipped)
    {
        if (equipped == null)
        {
            //Bare Hands Attack Finish
            return;
        }
        if (equipped._WeaponType == AttackWeaponType.Melee)
        {
            //Melee Satart
            AttackMeleeFinish(AttackSpeed);
        }
        else if (equipped._WeaponType == AttackWeaponType.CrossBow)
        {
            //Cross Bow Start
            CrossBowRelease(AttackSpeed);
        }
       
    }







    void CrossBowReadyStart(float AttackSpeed)
    {
        animator.SetFloat("AttackSpeed", AttackSpeed);
        animator.SetBool("CrossBowAttackReady", true);
    }
    void CrossBowRelease(float AttackSpeed)
    {
        animator.SetBool("CrossBowAttackReady", false);
    }
    void AttackMeleeStart(float AttackSpeed)
    {

        animator.SetFloat("AttackSpeed", AttackSpeed);
        animator.SetBool("AttackReadyMelee", true);
    }
    void AttackMeleeFinish(float AttackSpeed)
    {
       
        animator.SetBool("AttackReadyMelee", false);
        
    }

}

public enum AttackWeaponType
{
    Melee = 0, CrossBow = 1,
}
