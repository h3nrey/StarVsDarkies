using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Utils;

public class ColumnController : MonoBehaviour
{
    [SerializeField] float startingSpeed, endingSpeed, returningSpeed;
    [SerializeField] Transform startPoint, midPoint, endPoint;

    [SerializeField] Transform targetPoint;
    [SerializeField] float actualSpeed;

    [SerializeField] private float startDelay;
    [ReadOnly] public bool canMove;

    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D columnCol;

    private void Start() {
        transform.position = startPoint.position;

        actualSpeed = startingSpeed;
        targetPoint = midPoint;

        Coroutines.DoAfter(() => canMove = true, startDelay, this);
    }

    private void Update() {
        if (transform.position == targetPoint.position) {
            if (targetPoint.position == startPoint.position) {
                targetPoint = midPoint;
                actualSpeed = startingSpeed;
            }
            else if (targetPoint.position == midPoint.position) {
                actualSpeed = endingSpeed;
                targetPoint = endPoint;
            }
            else if (targetPoint.position == endPoint.position) {
                actualSpeed = returningSpeed;
                targetPoint = startPoint;
            }
        }
    }

    private void FixedUpdate() {
        if(canMove) {
            rb.position = Vector2.MoveTowards(rb.position, targetPoint.position, actualSpeed * Time.fixedDeltaTime);
        }
    }

}
