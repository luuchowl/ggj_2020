using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Hitbox : MonoBehaviour
{
    public enum HitboxShape { Box, Sphere }
    public enum ColliderState { Closed, Open, Colliding }

    [Header("Collision")]
    public LayerMask whatToHit;
    public HitboxShape shape;
    [ShowIf("RectangleSelected")] public Vector3 boxSize = new Vector3(1, 1, 1);
    [ShowIf("CircleSelected")] public float sphereRadius = .5f;
    public Transform customPivot;
    [Header("Properties")]
    public bool isSweetSpot;
    public int priority;
    [Header("Debug")]
    public Color inactiveColor = new Color(.5f, .5f, .5f, .5f);
    public Color activeColor = new Color(0, 1, 0, .5f);
    public Color collidingColor = new Color(1, 0, 0, .5f);

    private ColliderState currentState;

    #region Shape Property Conditions
    private bool RectangleSelected()
    {
        return shape == HitboxShape.Box;
    }

    private bool CircleSelected()
    {
        return shape == HitboxShape.Sphere;
    }
    #endregion

    private void Awake()
    {
        if(customPivot == null)
        {
            customPivot = transform;
        }
    }

    public void OpenCollision()
    {
        currentState = ColliderState.Open;
    }

    public void CloseCollision()
    {
        currentState = ColliderState.Closed;
    }

    private void Update()
    {
        UpdateHitBox();
    }

    public Collider[] UpdateHitBox()
    {
        if(currentState == ColliderState.Closed || !gameObject.activeInHierarchy)
        {
            return null;
        }

        Collider[] hits = null;

        switch (shape)
        {
            case HitboxShape.Box:
                hits = Physics.OverlapBox(customPivot.position, boxSize, customPivot.rotation, whatToHit);
                break;
            case HitboxShape.Sphere:
                hits = Physics.OverlapSphere(customPivot.position, sphereRadius, whatToHit);
                break;
            default:
                break;
        }

        currentState = hits.Length > 0 ? ColliderState.Colliding : ColliderState.Open;
        return hits;
    }

    private void OnDrawGizmos()
    {
        if(customPivot == null)
        {
            customPivot = transform;
        }

        Gizmos.matrix = Matrix4x4.TRS(customPivot.position, customPivot.rotation, customPivot.localScale);

        //Color
        switch (currentState)
        {
            case ColliderState.Open:
                Gizmos.color = activeColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
            default:
                Gizmos.color = inactiveColor;
                break;
        }

        //Shape
        switch (shape)
        {
            case HitboxShape.Box:
                Gizmos.DrawCube(Vector3.zero, boxSize);
                break;
            case HitboxShape.Sphere:
                Gizmos.DrawSphere(Vector3.zero, sphereRadius);
                break;
            default:
                break;
        }
    }
}
