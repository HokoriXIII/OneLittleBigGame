# CHARACTER (Unique Id="character")
## DYNAMICS SPECS OF CHARACTER
- Biting Volume: cm3 (double) (Represent the character Mouth Space) Unique Id="bitingVolume"
- Hand Space: cm (double) (For Holding; it means character can carry and hold items smaller than Hand Space) Unique Id="handSpace"
- Arm Strong: gr (double) (For Carrying Items In Hands, it means character can lift and carry object max this weight) Unique Id="armStrong"

## FUNCTIONS
**Hold and Carry item Function**

- bool CanLiftHoldCarryAsWeapon= ((handSpace-item.width)>0.0 && (armStrong-item.weight)>0.0)