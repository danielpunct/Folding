using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CurlEf : MonoBehaviour {

    public Transform _Front;

    public Transform _Mask;
    public Transform _GradOutter;

    public Transform handle;
    
    public Transform _PosLD;
    public Transform _PosRD;
    public Transform _PosLU;
    public Transform _PosRU;

    Vector3 _Pos;
    
    [SerializeField]
    RectTransform _rectTransform;
    [SerializeField]
    RectTransform _FrontRectTransform;

    int safeExit = 0;

    int corner = 2;
    public int StartCorner = 2;

    public Vector3 dragOffset = Vector3.zero;

    void Start()
    {
        corner = StartCorner;
        dragOffset = Vector3.zero;
    }

    bool changedCorner = false;

    void LateUpdate()
    {
        safeExit = 0;
        SetRotations();
    }

    void SetRotations()
    {
        if (safeExit++ > 2)
        {
            return;
        }
        _Front.position = Vector3.Lerp(_Front.position, handle.position + dragOffset, Time.fixedDeltaTime*10);
        _Front.position = handle.position+ dragOffset;        

        transform.position = _Pos;
        transform.eulerAngles = Vector3.zero;

        switch (corner)
        {
            case 1:
                pivotLU();
                break;
            case 2:
                pivotRU();
                break;
            case 3:
                pivotRD();
                break;
            case 4:
                pivotLD();
                break;
        }

        if (changedCorner)
        {
            changedCorner = false;
            SetRotations();
            return;
        }

        transform.position = _Pos;
        transform.eulerAngles = Vector3.zero;
    }


    void pivotLD()
    {
        //shift pivot
        _rectTransform.pivot = new Vector2(0, 0);
        _FrontRectTransform.pivot = new Vector2(0,1);
        _Pos = _PosLD.position;
        
        
        Vector3 pos = _Front.localPosition;
        float theta = Mathf.Atan2(pos.y, pos.x) * 180.0f / Mathf.PI;

        if ( theta >= 90.0f) // to much left
        {
            corner = 3;
            dragOffset += _PosRD.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        if (theta <= 0.0f) //too much down
        {
            corner = 1;
            dragOffset += _PosLU.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        float deg = -(90.0f - theta) * 2.0f;
        _Front.eulerAngles = new Vector3(0.0f, 0.0f, deg);

        _Mask.position = (transform.position + _Front.position) * 0.5f;
        _Mask.eulerAngles = new Vector3(0.0f, 0.0f, deg*0.5f);

        _GradOutter.position = _Mask.position;
        _GradOutter.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f + 90.0f);
    }
    
    void pivotRD()
    {
        //shift pivot
        _rectTransform.pivot = new Vector2(1, 0);
        _FrontRectTransform.pivot = new Vector2(1,1);
        _Pos = _PosRD.position;
        
        
        Vector3 pos = _Front.localPosition;
        float theta = Mathf.Atan2(pos.y, -pos.x) * 180.0f / Mathf.PI;

        if (theta >= 90.0f)
        {
            corner = 4;
            dragOffset -=  _PosRD.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        if (theta <= 0.0f)
        {
            corner = 2;
            dragOffset +=  _PosLU.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        float deg = (90.0f - theta) * 2.0f;
        _Front.eulerAngles = new Vector3(0.0f, 0.0f, deg);

        _Mask.position = (transform.position + _Front.position) * 0.5f;
        _Mask.eulerAngles = new Vector3(0.0f, 0.0f, deg*0.5f);

        _GradOutter.position = _Mask.position;
        _GradOutter.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f + 90.0f);
    }
    
    void pivotLU()
    {
        //shift pivot
        _rectTransform.pivot = new Vector2(0, 1);
        _FrontRectTransform.pivot = new Vector2(0,0);
        _Pos = _PosLU.position;
//        _FrontRectTransform.anchorMin = new Vector2(1,0);
//        _FrontRectTransform.anchorMax = new Vector2(1,0);
        
        
        Vector3 pos = _PosLU.position - _Front.position;
        float theta = Mathf.Atan2(pos.y, -pos.x) * 180.0f / Mathf.PI;
        if (theta >= 90.0f)
        {
          
            corner =2;
            dragOffset +=  _PosRD.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        if (theta <= 0.0f)
        { 
            corner = 4;
            dragOffset -=  _PosLU.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        float deg = (90.0f - theta) * 2.0f;
        _Front.eulerAngles = new Vector3(0.0f, 0.0f, deg);

        _Mask.position = (transform.position + _Front.position) * 0.5f;
        _Mask.eulerAngles = new Vector3(0.0f, 0.0f, deg*0.5f+180);

        _GradOutter.position = _Mask.position;
        _GradOutter.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f - 90.0f);
    }
    
    
    void pivotRU()
    {
        //shift pivot
        _rectTransform.pivot = new Vector2(1, 1);
        _FrontRectTransform.pivot = new Vector2(1,0);
        _Pos = _PosRU.position;
//        _FrontRectTransform.anchorMin = new Vector2(1,0);
//        _FrontRectTransform.anchorMax = new Vector2(1,0);
        
        
        Vector3 pos = _PosRU.position - _Front.position;
        float theta = Mathf.Atan2(pos.y, pos.x) * 180.0f / Mathf.PI;
        Debug.Log(theta);
        if (theta >= 90.0f)
        {
            corner = 1;
            dragOffset -=  _PosRD.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        if (theta <= 0.0f)
        { 
            corner = 3;
            dragOffset -=  _PosLU.position - _PosLD.position;
            changedCorner = true;
            return;
        }

        float deg = -(90.0f - theta) * 2.0f;
        _Front.eulerAngles = new Vector3(0.0f, 0.0f, deg);

        _Mask.position = (transform.position + _Front.position) * 0.5f;
        _Mask.eulerAngles = new Vector3(0.0f, 0.0f, deg*0.5f+180);

        _GradOutter.position = _Mask.position;
        _GradOutter.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f - 90.0f);
    }

}
