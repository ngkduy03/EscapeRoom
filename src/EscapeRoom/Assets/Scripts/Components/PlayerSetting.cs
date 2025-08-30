using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject for storing gun settings.
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSetting", order = 1)]
public class PlayerSetting : ScriptableObject
{
    [field: SerializeField]
    public float MaxWalkSpeed { get; private set; }

    [field: SerializeField]
    public float MaxSprintSpeed { get; private set; }

    [field: SerializeField]
    public float WalkAnimValue { get; private set; }

    [field: SerializeField]
    public float SprintAnimValue { get; private set; }

    [field: SerializeField]
    public float SpeedAccel { get; private set; }

    [field: SerializeField]
    public float RotateSpeed { get; private set; }

    [field: SerializeField]
    public float AnimSpeedAccel { get; private set; }

    [field: SerializeField]
    public float JumpForce { get; private set; }

    [field: SerializeField]
    public float BreakForce { get; private set; }

    [field: SerializeField]
    public float GravityForce { get; private set; }
}

