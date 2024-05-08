using UnityEngine;

public class RedZone : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerStay (Collider other) {
        Cube cube = other.GetComponent<Cube>() ;
        if (cube != null && !cube.isMainCube && cube.cubeRigidbody.velocity.magnitude < .1f)
        {
            GameManager.instance.SetTimeScale(0);
            GameManager.instance.l.SetActive(true);

            GameManager.instance.audioSource.Stop();
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.audioClips[5]);

            GameManager.instance.isAd = true;
        }
    }
}
