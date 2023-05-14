using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Debug = System.Diagnostics.Debug;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.Touch;
using TouchPhase = UnityEngine.TouchPhase;

public class EnableJoystick : MonoBehaviour
{
    [SerializeField] private GameObject joystick;

    private Finger MovementFinger;
    private Vector2 MovementAmount;
    

    
    private void Start()
    {
        
        joystick.SetActive(false);
    }
    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerUp;
    }

    private void HandleFingerDown(Finger touchedFinger)
    {
        if (MovementFinger != null) return;
        Ray ray = Camera.main.ScreenPointToRay(touchedFinger.screenPosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;
        if (hit.collider.CompareTag("Clickable")) return;
        joystick.SetActive(true);
        joystick.transform.position = touchedFinger.screenPosition;
    }
    private void HandleFingerUp(Finger obj)
    {
        joystick.SetActive(false);
    }


    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerUp;

        EnhancedTouchSupport.Disable();
    }

    /*private void Update()
    {
        Debug.Log(Input.touchCount);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log(touch.phase.ToString());

            switch (touch.phase)
            {
                case TouchPhase.Ended:
                    Debug.Log("TouchPhase Ended");
                    joystick.SetActive(false);
                    break;
                default:
                    Debug.Log("TouchPhase Begun");
                    Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (!hit.collider.CompareTag("Clickable"))
                        {
                            joystick.SetActive(true);
                            joystick.transform.position = Input.touches[0].position;

                        }
                    }
                    break;
            }
        }
    }*/
}
            
        




    

    

