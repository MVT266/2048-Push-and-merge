using UnityEngine ;

public class FX : MonoBehaviour
{
    public static FX Instance;

    public ParticleSystem cubeExplosionFX;

    private ParticleSystem.MainModule cubeExplosionFXMainModule;

    private void Awake ()
    {
        Instance = this;
    }

    private void Start ()
    {
        cubeExplosionFXMainModule = cubeExplosionFX.main;
    }

    public void PlayCubeExplosionFX (Vector3 position, Color color, bool isExplosion = false)
    {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosionFX.transform.position = position;

        if (isExplosion) cubeExplosionFX.transform.localScale = Vector3.one * 2;
        cubeExplosionFX.Play();
    }
}
