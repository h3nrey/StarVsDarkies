using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FootBehaviour : MonoBehaviour
{
    private footGlobeController parent;
    private float moveDistance => parent.data.footDistance;
    private float moveSpeed => parent.data.footSpeed;

    private Vector3 startPos;
    private Rigidbody2D rb;

    private void Awake() {
        parent = transform.parent.GetComponent<footGlobeController>(); 
    }

    private void Start() {
        startPos = transform.localPosition;
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate() {
        // Move the child object back and forth along its local up axis using Rigidbody velocity
        float verticalOffset = Mathf.Sin(Time.time * moveSpeed) * (moveDistance / 10f);
        Vector3 targetVelocity = transform.up * verticalOffset;
        rb.velocity = targetVelocity;
    }
}
