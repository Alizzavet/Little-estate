using UnityEngine;
using DG.Tweening;

public class ShopWindow : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition; 
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private Vector3 _startPosition;

    private void MoveToTargetPosition(Vector3 position, float duration)
    {
        var tweener = transform.DOLocalMove(position, duration);
        tweener.OnComplete(() => tweener.Kill());
    }

    public void MoveToScene()
    {
        MoveToTargetPosition(_targetPosition, _animationDuration);
    }

    public void MoveToBack()
    {
        MoveToTargetPosition(_startPosition, _animationDuration);
    }
}
