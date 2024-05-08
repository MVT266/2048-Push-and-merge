using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner instance;

    public GameObject cubePrefab;
    public Color[] cubeColors;

    private void Awake()
    {
        instance = this;
    }

    public Cube Spawn(int number, Vector3 position)
    {
        Cube cube = Instantiate(cubePrefab, transform.position, Quaternion.identity, transform).GetComponent<Cube>();

        cube.SetNumber(number);
        cube.cubeType = 0;

        int index = (int)(Mathf.Log(number) / Mathf.Log(2)) - 1;
        if (index < cubeColors.Length) cube.SetColor(cubeColors[index]);
        cube.bomb.SetActive(false);

        cube.transform.position = position;
        cube.gameObject.SetActive(true);

        return cube;
    }

    public Cube SpawnRandom()
    {
        return Spawn((int)Mathf.Pow(2, Random.Range(1, 5)), transform.position);
    }

    public void DestroyCube(Cube cube)
    {
        cube.cubeRigidbody.velocity = Vector3.zero;
        cube.cubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;

        Destroy(cube.gameObject);
    }
}
