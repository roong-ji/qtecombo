using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float ShakeAmount;
    public float ShakeTime;
    private Vector3 _originPosition;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _originPosition = _camera.transform.localPosition;
    }

    public void ShakeForTime(float amount, float time)
    {
        ShakeAmount = amount;
        ShakeTime = time;
    }

    private void LateUpdate()
    {

        if(ShakeTime > 0)
        {
            Vector2 shakeVector = Random.insideUnitSphere * ShakeAmount;
            _camera.transform.localPosition = _camera.transform.rotation * shakeVector + _originPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0f;
            _camera.transform.localPosition = _originPosition;
        }
    }
}
