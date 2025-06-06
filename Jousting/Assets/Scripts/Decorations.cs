using UnityEngine;

public class Decorations : MonoBehaviour
{
    private Transform cameraTransform;
    private float scaleAmount;
    private Vector3 defaultScale;

    // Use this for initialization
    void Start()
    {
        cameraTransform = Camera.main.transform;
        defaultScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        scaleAmount = (-Vector3.Distance(transform.position, cameraTransform.position) / 40) + 1.5f;
        scaleAmount = Mathf.Clamp01(scaleAmount);
        transform.localScale = defaultScale * scaleAmount;
    }
}
