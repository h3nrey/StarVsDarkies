using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Player")]
public class Player : ScriptableObject {
    [Header("Move")]
    public float speed;
    public float rotateSpeed;

    [Header("Impulse")]
    public float impulseSpeed;
    public int impulseSense;
    public float enableMoveTime;

    [Header("Knockback")]
    public float knockbackForce;

    [Header("Blink Effect")]
    public Color[] blinkColors;
    public int blinkTime;
}
