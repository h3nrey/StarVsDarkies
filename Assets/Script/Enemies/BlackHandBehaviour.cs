using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class BlackHandBehaviour : MonoBehaviour
{
    enum HandDirections {
        top, 
        bottom,
        left,
        right,
    }

    [Header("Movement")]
    [SerializeField] Vector2 startPos;
    // Declare a target position for the object to move towards
    [SerializeField] Transform target;

    // Declare a speed for the object to move at
    [SerializeField] float frequency = 1.0f;

    // Declare an amplitude for the sinusoidal movement
    [SerializeField] float amplitude = 1.0f;

    // Declare a phase offset for the sinusoidal movement
    [SerializeField] float phaseOffset = 0.0f;

    [Header("Check Player")]
    public Collider2D playerIsCloser;
    [SerializeField] float maxPlayerRadius = 0.5f;
    [SerializeField] LayerMask playerLayer;


    [Header("Line Renderer")]
    [SerializeField] LineRenderer line;
    [SerializeField] float targetOffset;
    [SerializeField] float startPointOffset;
    [SerializeField][EnumFlags] HandDirections handDir; 

    private void Start() {
        startPos = transform.position;
        transform.up = target.up;

        switch (handDir) {
            case HandDirections.top:
                line.SetPosition(0, new Vector2(startPos.x, startPos.y + startPointOffset));
                break;
            case HandDirections.bottom:
                line.SetPosition(0, new Vector2(startPos.x, startPos.y - startPointOffset));
                break;
            case HandDirections.left:
                line.SetPosition(0, new Vector2(startPos.x - startPointOffset, startPos.y));
                break;
            case HandDirections.right:
                line.SetPosition(0, new Vector2(startPos.x + startPointOffset, startPos.y));
                break;
        }

    }

    void Update() {
        Movement();
    }

    private void Movement() {
        line.SetPosition(1, new Vector2(transform.position.x, transform.position.y));
        float distance = Vector3.Distance(transform.position, target.position);

        // Use Mathf.SmoothStep to create a smooth easing effect as the object approaches the target position
        float t = Mathf.SmoothStep(0, 1, distance / frequency);
        transform.position = Vector2.Lerp(startPos, target.position, Mathf.Sin(Time.time * frequency) * amplitude);
    }

    private void FixedUpdate() {
        playerIsCloser = Physics2D.OverlapCircle(transform.position, maxPlayerRadius, playerLayer);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxPlayerRadius);
    }

}
