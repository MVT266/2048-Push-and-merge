using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private Cube cube;

    private void Awake()
    {
        cube = GetComponent<Cube>();      
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null && otherCube.cubeType == 0 && (cube.CubeID > otherCube.CubeID && cube.cubeNumber == otherCube.cubeNumber || cube.cubeType != 0))
        {
            Vector3 contactPoint = collision.contacts[0].point;

            if (cube.cubeType != 2)
            {
                Cube newCube = CubeSpawner.instance.Spawn(otherCube.cubeNumber * 2, contactPoint + Vector3.up);
                newCube.cubeRigidbody.AddForce(new Vector3(0, 1f, 1f), ForceMode.Impulse);
                newCube.cubeRigidbody.AddTorque(Vector3.one * Random.Range(-10f, 10f));
            }

            foreach (Collider coll in Physics.OverlapSphere(contactPoint, 2f))
            {
                Cube collCube = coll.gameObject.GetComponent<Cube>();

                if (collCube != null)
                {
                    if (cube.cubeType == 2) CubeSpawner.instance.DestroyCube(collCube);
                    else coll.attachedRigidbody.AddExplosionForce(200f, contactPoint, 2f);
                }                    
            }

            FX.Instance.PlayCubeExplosionFX(contactPoint, otherCube.cubeColor, cube.cubeType == 2);

            CubeSpawner.instance.DestroyCube(cube);
            CubeSpawner.instance.DestroyCube(otherCube);

            if (cube.cubeType == 0) GameManager.instance.AddScore(otherCube.cubeNumber * 10);

            if (otherCube.cubeNumber < 32) GameManager.instance.PlayAudio(1);
            else if (otherCube.cubeNumber >= 32 && otherCube.cubeNumber < 512) GameManager.instance.PlayAudio(2);
            else GameManager.instance.PlayAudio(3);

            if (PlayerPrefs.GetFloat("vibration") != 0) Handheld.Vibrate();
        }
    }
}
