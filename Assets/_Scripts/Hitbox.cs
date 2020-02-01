using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Hitbox : MonoBehaviour
{
    public enum HitboxShape { Rectangle, Circle }
    public enum ColliderState { Closed, Open, Colliding }

    [Header("Collision")]
    public LayerMask whatToHit;
    public HitboxShape shape;
    [ShowIf("RectangleSelected")] public Vector2 boxSize = new Vector2(1, 1);
    [ShowIf("CircleSelected")] public float circleRadius = .5f;
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
        return shape == HitboxShape.Rectangle;
    }

    private bool CircleSelected()
    {
        return shape == HitboxShape.Circle;
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

    public Collider2D[] UpdateHitBox()
    {
        if(currentState == ColliderState.Closed || !gameObject.activeInHierarchy)
        {
            return null;
        }

        Collider2D[] hits = null;

        switch (shape)
        {
            case HitboxShape.Rectangle:
                hits = Physics2D.OverlapBoxAll(customPivot.position, boxSize, customPivot.rotation.eulerAngles.z, whatToHit);
                break;
            case HitboxShape.Circle:
                hits = Physics2D.OverlapCircleAll(customPivot.position, circleRadius, whatToHit);
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
            case HitboxShape.Rectangle:
                Gizmos.DrawCube(Vector3.zero, boxSize);
                break;
            case HitboxShape.Circle:
                Gizmos.DrawSphere(Vector3.zero, circleRadius);
                break;
            default:
                break;
        }
    }
}
