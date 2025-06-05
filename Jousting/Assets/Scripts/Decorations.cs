using UnityEngine;

public class Decorations : MonoBehaviour
{
    private Transform cameraTransform;
    private float scaleAmount;

    // Use this for initialization
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        scaleAmount = (-Vector3.Distance(transform.position, cameraTransform.position) / 40) + 1.5f;
        scaleAmount = Mathf.Clamp01(scaleAmount);
        transform.localScale = new Vector3 (scaleAmount, scaleAmount, scaleAmount);
    }
}
