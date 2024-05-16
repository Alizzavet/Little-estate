using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    private Transform _player;
    private Transform _currentFollow;

    [SerializeField] private Camera _Camera;

    public static CameraFollowController Instance;

    [SerializeField] private float Smoothing = 1.5f;

    private float targetZPosition;
    private bool isMovingToPoint = false;



    private float maxX;
    private float minX;

    public void SetX(float x)
    {
        maxX = x;
    }
    public void SetMinX(float x)
    {
        minX = x;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        targetZPosition = _Camera.transform.position.z;
    }

    private Vector3 _offset;
    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void SetNewPos(Transform otherPos)
    {
        _currentFollow = otherPos;
        isMovingToPoint = true;
    }

    public void MoveToPlayer()
    {
        _offset = _Camera.transform.position - _player.position;
        _offset.y += 2f;
        _currentFollow = _player;
        isMovingToPoint = false;
    }
    
    private void Update()
    {
        if (_currentFollow == null)
            return;

        Vector3 targetCamPos;
        if (!isMovingToPoint)
        {
            targetCamPos = _currentFollow.position + _offset;
            targetCamPos.z = targetZPosition;
        
            

            targetCamPos.x = Mathf.Clamp(targetCamPos.x, minX, maxX);
        }
        else
            targetCamPos = _currentFollow.transform.position;
        
        _Camera.transform.position = Vector3.Lerp(_Camera.transform.position, targetCamPos, Smoothing * Time.deltaTime); 
    }
}