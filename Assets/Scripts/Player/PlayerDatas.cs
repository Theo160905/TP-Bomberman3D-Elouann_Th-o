using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerDatas : ScriptableObject
{
    public float WalkTopSpeed;
    public float WalkAcceleration;
    public float WalkDeceleration;
    public float WalkVelPower;
    public float FrictionAmount;

    //public PlayerDatas(float walkTopSpeed, float walkAcceleration, float walkDeceleration, float walkVelPower, float frictionAmount)
    //{
    //    WalkTopSpeed = walkTopSpeed;
    //    WalkAcceleration = walkAcceleration;
    //    WalkDeceleration = walkDeceleration;
    //    WalkVelPower = walkVelPower;
    //    FrictionAmount = frictionAmount;
    //}
}
