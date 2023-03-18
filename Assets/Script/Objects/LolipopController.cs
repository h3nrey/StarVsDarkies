using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LolipopController : MonoBehaviour
{
    [SerializeField] private Animator chocolateBarAnim;

    [SerializeField] private GameObject chocolateBarPrefab;

    [Header("press")]
    [SerializeField] private float pressSpeed;
    [SerializeField] private float startAngle, endAngle;
    [ReadOnly] public bool pressed;


    [Button("Create Chocolate Bar")]
    
    private void CreateChocolateBar() {
        GameObject instance = Instantiate(chocolateBarPrefab, transform.position, Quaternion.identity) as GameObject;

        if(instance.GetComponent<Animator>()) {
            chocolateBarAnim = instance.GetComponent<Animator>();
        }
    }

    private void Start() {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, startAngle);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            chocolateBarAnim.SetTrigger("Open");
            pressed = true;
        }
    }

    private void Update() {
        if(pressed)
            RotateLolipop();
    }

    private void RotateLolipop() {
        Vector3 angles = transform.eulerAngles;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(angles.x, angles.y, endAngle));

        if(transform.rotation == targetRotation) {
            pressed = false;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, pressSpeed);
    }

}
