using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Float : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBefSup;
    public float displacementAmt;
    public int floaterAmt;

    public float waterDrag;
    public float waterAngleDrag;
    public WaterSurface waterSurface;

    WaterSearchParameters Search;
    WaterSearchResult searchResult;
    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterAmt, transform.position, ForceMode.Acceleration);

        Search.startPositionWS = transform.position;

        waterSurface.ProjectPointOnWaterSurface(Search, out searchResult);

        if(transform.position.y < searchResult.projectedPositionWS.y)
        {
            float displacementMulti = Mathf.Clamp01((searchResult.projectedPositionWS.y - transform.position.y) / depthBefSup) * displacementAmt;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMulti * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMulti * -rb.angularVelocity * waterAngleDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
