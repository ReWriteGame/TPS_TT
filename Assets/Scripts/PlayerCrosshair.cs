using UnityEngine;

public class PlayerCrosshair : MonoBehaviour
{
    [SerializeField] private float maxDistanceView = 1000;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private QueryTriggerInteraction triggerInteraction;

    private Vector3 targetHitPoint;
    public bool isTargetHit = false;

    public Vector3 TargetHitPoint => targetHitPoint;
    public bool IsTargetHit => isTargetHit;

    private void FixedUpdate() => MoveHitPoint();

    private void MoveHitPoint()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        bool findPoint = Physics.Raycast(ray, out RaycastHit raycastHit, maxDistanceView, collisionLayers, triggerInteraction);
        if (findPoint) targetHitPoint = raycastHit.point;

        isTargetHit = findPoint;
    }

#if UNITY_EDITOR

    [Header("Gizmos")]
    [SerializeField] private DrawGizmosType drawGizmosType;
    [SerializeField] private Color gizmosColor = Color.yellow;
    [SerializeField] private float sphereRadius = 0.2f;
    [SerializeField] private float distanceDrawGizmos = 10;

    private void OnDrawGizmos() => TryDrawGizmos(DrawGizmosType.Always);
    private void OnDrawGizmosSelected() => TryDrawGizmos(DrawGizmosType.OnSelected);

    private void TryDrawGizmos(DrawGizmosType requiredType)
    {
        if (drawGizmosType != requiredType) return;
        Camera camera = Camera.main;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector3 startPoint = camera.ScreenToWorldPoint(screenCenterPoint);
        Vector3 endPoint = isTargetHit ? targetHitPoint :
            camera.transform.position + camera.transform.forward.normalized * distanceDrawGizmos;

        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(endPoint, sphereRadius);
        Gizmos.DrawLine(startPoint, endPoint);
    }

#endif
}
