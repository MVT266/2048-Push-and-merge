using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform target;
    public float speed, offset;  

    private void Update()
    {
        if (speed > 0)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(target.position.x * speed + offset, pos.y, pos.z);
        }
    }
}
