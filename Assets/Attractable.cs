using UnityEngine;

public class Attractable : MonoBehaviour
{
    public bool rotateToCenter;
    public bool isAttracted;
    public Attractor currentAttractor;

    Transform m_transform;
    Collider2D m_collider;
    Rigidbody2D m_rigidbody;
    public float AttractableAngle;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rotateToCenter = GetComponent<Player_Movement_1_0_2>().PlanetaryBody_G;
        isAttracted = GetComponent<Player_Movement_1_0_2>().PlanetaryGravity_G;
        if (currentAttractor != null)
        {
            if (!currentAttractor.AttractedObjects.Contains(m_collider)) currentAttractor = null;
            if (rotateToCenter) RotateToCenter();
        }
    }

    public void Attract(Attractor artgra)
    {
        if (isAttracted)
        {
            Vector2 attractionDir = (Vector2)artgra.planetTransform.position - m_rigidbody.position;
            m_rigidbody.AddForce(attractionDir.normalized * -artgra.Gravity * 100 * Time.fixedDeltaTime);

            if (currentAttractor == null)
            {
                currentAttractor = artgra;
            }
        }
    }

    void RotateToCenter()
    {
            Vector2 distanceVector = (Vector2)currentAttractor.planetTransform.position - (Vector2)m_transform.position;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            AttractableAngle = angle;
    }
}
