using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CurlEf : MonoBehaviour {

    public Transform _Front;
    public Transform _Mask;
    public Transform _GradOutter;

    public Transform _PosLD;
    public Transform _PosRD;

    Vector3 _Pos;
    
    void LateUpdate()
    {
        transform.position = _Pos;
        transform.eulerAngles = Vector3.zero;

        pivotLD();

        transform.position = _Pos;
        transform.eulerAngles = Vector3.zero;
    }


    void pivotLD()
    {
        //shift pivot
        
        _Pos = _PosLD.position;
        
        
        Vector3 pos = _Front.localPosition;
        float theta = Mathf.Atan2(pos.y, pos.x) * 180.0f / Mathf.PI;

        if (theta <= 0.0f || theta >= 90.0f) return;

        float deg = -(90.0f - theta) * 2.0f;
        _Front.eulerAngles = new Vector3(0.0f, 0.0f, deg);

        _Mask.position = (transform.position + _Front.position) * 0.5f;
        _Mask.eulerAngles = new Vector3(0.0f, 0.0f, deg*0.5f);

        _GradOutter.position = _Mask.position;
        _GradOutter.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f + 90.0f);
    }
    
    
}
