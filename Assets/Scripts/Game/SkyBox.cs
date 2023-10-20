using UnityEngine;

public class SkyBox : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1;
    private float rotate = 0;
    private float startRotate = 0;
    const string ROTATE_VAR = "_Rotation";

    private void Start()
    {
        startRotate = RenderSettings.skybox.GetFloat(ROTATE_VAR);
        rotate = startRotate;
    }

    private void Update()
    {
        LoopRotateSkybox();
    }
  
    private void OnDisable()
    {
        SetRotateSkybox(startRotate);
    }

    private void LoopRotateSkybox()
    {
        rotate += rotateSpeed * Time.deltaTime;
        SetRotateSkybox(rotate);
    }

    public void SetRotateSkybox(float rotate)
    {
        rotate %= 360f;
        RenderSettings.skybox.SetFloat(ROTATE_VAR, rotate);
    }
}
