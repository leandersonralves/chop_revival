using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Helicopter : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    private float m_verticalAxis;
    
    private float m_horizontalAxis;

    [SerializeField]
    private float maxForceHorizontalPropeller;

    [SerializeField]
    private float maxForceMainPropeller = 2f;

    [SerializeField]
    private Vector3 forcePositionMain = new Vector3(0f, 0.6f, 0f);

    [SerializeField]
    private Vector3 centerOfMass = new Vector3(0f, 0.8f, 0f);

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_verticalAxis = Input.GetAxis("Vertical");
        m_horizontalAxis = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(m_verticalAxis, 0f) || !Mathf.Approximately(m_horizontalAxis, 0f))
        {
            m_rigidbody.AddForce(
                Vector3.up * maxForceMainPropeller * m_verticalAxis + 
                Vector3.forward * maxForceHorizontalPropeller * m_horizontalAxis,
                ForceMode.Force
            );
        }
        m_rigidbody.centerOfMass = centerOfMass;
    }

    [SerializeField]
    private float proportionalLineGUI = 0.1f;

    private void OnDrawGizmos() {
        var cacheColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_rigidbody.position, m_rigidbody.position + m_rigidbody.velocity * proportionalLineGUI);
        Gizmos.color = cacheColor;
    }
}
