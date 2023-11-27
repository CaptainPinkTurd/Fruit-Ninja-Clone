using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    [SerializeField] GameObject blade;
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            blade.SetActive(true);
            SetBladeToMouse();
        }
        else
        {
            blade.SetActive(false);
        }    
    }
    private void SetBladeToMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 15;
        blade.transform.localPosition = Camera.main.ScreenToWorldPoint(mousePos);    
    }
}
