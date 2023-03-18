using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
public class footGlobeController : MonoBehaviour
{
    [Expandable] public FootGlobeData data;

    [SerializeField] public int footAmount;
    [SerializeField] GameObject footPrefab;
    [SerializeField] List<Transform> foots;
    [SerializeField] float footOffset;
    [SerializeField] float footSpeed;

    [SerializeField] Rigidbody2D globeRb;
    [SerializeField] float globeRadius = 5f;


    [Button("Instantiate Foots")]
    private void InstantiateFoots() {
        // Calculate the angle between each child object
        float angleStep = 2f * Mathf.PI / footAmount;

        // Calculate the circumference of the parent circle
        //globeRadius = GetComponent<SpriteRenderer>().size.x / 2f;
        print(globeRadius);
        print($"local scale: {GetComponent<SpriteRenderer>().size.x}");
        float globeCircumference = 2f * Mathf.PI / globeRadius;

        for (int i = 0; i < footAmount; i++) {
            // Calculate position of child element using polar coordinates
            float angle = angleStep  * i;
            float x = Mathf.Cos(angle) * globeRadius;
            float y = Mathf.Sin(angle) * globeRadius;
            Vector3 position = transform.position + new Vector3(x, y, 0f);
            Quaternion rot = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f);

            // Actually instantiate the foot
            GameObject foot = Instantiate(footPrefab, position, rot, this.transform);
            foots.Add(foot.transform);
        }
    }

    [Button("Remove Foots")]
    private void RemoveChilds() {
        while (foots.Count > 0) {
            Transform last = foots[foots.Count - 1];
            foots.Remove(last);
            DestroyImmediate(last.gameObject);
        }
    }

    private void Update() {
        transform.Rotate(data.globeRotSpeed * Time.deltaTime * Vector3.forward);
    }
}