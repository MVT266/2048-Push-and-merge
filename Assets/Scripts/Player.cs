using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed, pushForce, cubeMaxPosX;
    public TouchSlider touchSlider;
    public GameObject block;

    public TMP_Text[] powerTexts;

    private Cube mainCube;
    private Vector3 cubePos;
    private bool isPointerDown, canMove;

    private void Start()
    {
        UpdatePowerTexts();

        SpawnCube();
        canMove = true;

        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (mainCube.isMainCube && isPointerDown) mainCube.transform.position = Vector3.Lerp(mainCube.transform.position, cubePos, moveSpeed * Time.deltaTime);
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (mainCube.isMainCube && isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }

    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            block.gameObject.SetActive(true);

            isPointerDown = false;
            canMove = false;

            mainCube.cubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
            mainCube.isMainCube = false;

            GameManager.instance.PlayAudio(0);

            Invoke("SpawnNewCube", .5f);
        }
    }

    private void SpawnNewCube()
    {
        mainCube.isMainCube = false;
        
        SpawnCube();
        canMove = true;

        block.gameObject.SetActive(false);
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.instance.SpawnRandom();
        mainCube.isMainCube = true;

        cubePos = mainCube.transform.position;
    }

    private void UpdatePowerTexts()
    {
        powerTexts[0].text = PlayerPrefs.GetInt("power1").ToString();
        powerTexts[1].text = PlayerPrefs.GetInt("power2").ToString();
    }

    public void SetType(int type)
    {
        string pref = "";
        if (type == 1) pref = "power1";
        else if (type == 2) pref = "power2";

        if (PlayerPrefs.GetInt(pref) > 0)
        {
            block.gameObject.SetActive(true);

            PlayerPrefs.SetInt(pref, PlayerPrefs.GetInt(pref) - 1);

            mainCube.cubeType = type;

            UpdatePowerTexts();

            FX.Instance.PlayCubeExplosionFX(mainCube.transform.position, mainCube.cubeColor);

            GameManager.instance.PlayAudio(1);

            if (PlayerPrefs.GetFloat("vibration") != 0) Handheld.Vibrate();
        }    
    }

    private void OnDestroy()
    {
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
