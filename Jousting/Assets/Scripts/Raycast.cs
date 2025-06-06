using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    public LayerMask enemyHorses;
    private Vector3 raycastPos;
    public Transform stickTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward); //create raycast from attached obj
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, enemyHorses)) //extend ray whatever range is, layermask to only hit the enemies
        {
            lastHit = hit.transform.gameObject;//last hit enemy
            Debug.Log(hit.transform.gameObject);
            collision = hit.point;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, radius:0.2f);
    }

}
