using UnityEngine;

public class BillboardScript : MonoBehaviour
{
        private Transform cameraTransform;

        // Use this for initialization
        void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.forward = cameraTransform.forward;
        }
 }
