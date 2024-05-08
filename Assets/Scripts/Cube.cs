using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    static int staticID = 0;

    public TMP_Text[] numbersText;
    public GameObject number, bomb;

    [HideInInspector] public int CubeID;

    [HideInInspector] public int cubeNumber, cubeType;
    [HideInInspector] public Color cubeColor;

    [HideInInspector] public Rigidbody cubeRigidbody;    
    [HideInInspector] public bool isMainCube;

    private MeshRenderer cubeMeshRenderer;

    private void Awake()
    {
        CubeID = staticID++;

        cubeRigidbody = GetComponent<Rigidbody>();

        cubeMeshRenderer = GetComponent<MeshRenderer>();     
    }

    private void Update()
    {
        if (cubeType == 1)
        {
            number.SetActive(false);
            cubeMeshRenderer.material.SetColor("_Color", HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time, 1), 1, 1)));
        }
        else if (cubeType == 2)
        {
            number.SetActive(false);
            bomb.SetActive(true);
        }
        else
        {
            number.SetActive(true);
        }
    }

    public void SetColor(Color color)
    {
        cubeColor = color;
        cubeMeshRenderer.material.color = color;
    }

    public void SetNumber(int number)
    {
        cubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            numbersText[i].text = number.ToString();
        }
    }
}
