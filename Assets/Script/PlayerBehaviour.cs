using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{
    // Instance
    public static PlayerBehaviour instance;

    // Data
    [SerializeField]
    [Expandable] private Player data;

    [Header("Inputs")]
    [ReadOnly] public float inputX;
    [ReadOnly] public int impulseSense;
    private float sense;

    public UnityEvent onImpulse;

    #region Data Variables
    private float speed => data.speed;
    private float rotateSpeed => data.rotateSpeed;

    private float impulseSpeed => data.impulseSpeed;
    private float enableMoveTime => data.enableMoveTime;

    #endregion
    
    [SerializeField]
    public bool canMove;
    [SerializeField]
    public bool canImpulse;
    public bool stucked;
    public Transform enemyTarget;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D rb;
    private void Awake() {
        instance = this;
        canMove = true;
        canImpulse = true;
        onImpulse.AddListener(Impulse);
        GettingComponents();
    }

    private void FixedUpdate() {
        Move();
        BeeingDraged();
    }


    private void Move() {
        if (Mathf.Abs(inputX) > 0 && canMove) {
            sense = inputX > 0 ? 1 : -1;
        }

        if (sense == 1) {
            Quaternion disiredRot = Quaternion.AngleAxis(0, Vector2.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, disiredRot, rotateSpeed * Time.deltaTime);
        } else  {
            Quaternion disiredRot = Quaternion.AngleAxis(180, Vector2.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, disiredRot, rotateSpeed * Time.deltaTime);
        }

        if (canMove) {
            rb.AddForce(sense * Vector2.right * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void Impulse() {
        if(canImpulse) {
            canMove = false;
            rb.AddForce(Vector2.up * impulseSense * impulseSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            print("Impulsionou djoido");
            StartCoroutine(EnableMove(enableMoveTime));
        }
    }

    private void BeeingDraged() {
        if(stucked) {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget.position, 1f);
        }
    }

    public void setTarget(Transform target) {
        canMove = false;
        canImpulse = false;
        stucked = true;
        enemyTarget = target;
    }

    IEnumerator EnableMove(float time) {
        yield return new WaitForSeconds(time);
        canMove = true;
        yield break;
    }

    private void GettingComponents() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Win") {
            GameManager.Game.onWinPhase?.Invoke();
        }

        if (other.gameObject.CompareTag("BlackHand")) {
            setTarget(other.gameObject.transform);
            other.gameObject.transform.position = Vector2.MoveTowards(other.gameObject.transform.position, transform.position, 10f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Coroutines.DoAfter(() => {
                GameManager.Game.CallScene(GameManager.Game.actualScene);
            }, 1f, this);
            canMove = false;
            StartCoroutine(BlinkEffect(data.blinkTime, data.blinkColors));

            rb.AddForce(other.GetContact(0).normal * data.knockbackForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private IEnumerator BlinkEffect(int time, Color[] colors) {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        for (int i = 0; i < time; i++) {
            foreach (Color color in colors) {
                sprite.color = color;
                yield return new WaitForSeconds(0.1f);
            }
        }

        sprite.color = Color.white;
        yield break;
    }
}
