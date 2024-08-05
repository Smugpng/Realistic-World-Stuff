using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Float : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBeforeSubmerge;
    public float displacementAmount;
    public int floaters;
    public float waterDrag;
    public float waterAngluarDrag;
    public WaterSurface water;
    WaterSearchParameters search;
    WaterSearchResult searchResult;

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity/floaters, transform.position, ForceMode.Acceleration);

        search.startPosition = transform.position;

        water.FindWaterSurfaceHeight(search, out searchResult);

        if(transform.position.y < searchResult.height)
        {
            float displacementMultiply = Mathf.Clamp01(searchResult.height - transform.position.y/depthBeforeSubmerge)* displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiply, 0f), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiply * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiply * -rb.angularVelocity * waterAngluarDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
