﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ErlIsaCamera : MonoBehaviour {

    Controller2D Erl;
    FlyingController2D Isa;

    public Vector2 erlFocusAreaSize;
    public Vector2 isaFocusAreaSize;

    public float camMaxDistance = 20.0f;
    public float camMinDistance = 10.0f;
    public float dampTime = 0.15f;

    FocusArea erlFocusArea;
    FocusArea isaFocusArea;

    Vector3 distanceBetween;
    float camDistance;
    float camOffset;
    Vector3 midPoint;
    public float bounds = 12.0f;

    Vector3 Midpoint;
    float MidX;
    float MidY;
    float MidZ;

    Vector3 velocity = Vector3.zero;

    new Camera camera;

	// Use this for initialization
	void Start () {
        Erl = GameObject.Find("Erl").GetComponent<Controller2D>();
        Isa = GameObject.Find("Isa").GetComponent<FlyingController2D>();

        erlFocusArea = new FocusArea(Erl.collider.bounds, erlFocusAreaSize);
        isaFocusArea = new FocusArea(Isa.collider.bounds, isaFocusAreaSize);

        camera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        /*
        erlFocusArea.Update(Erl.collider.bounds);
        isaFocusArea.Update(Isa.collider.bounds);
        */

        distanceBetween = Erl.transform.position - Isa.transform.position;
        if (camDistance > camMaxDistance)
        {
            camDistance = camMaxDistance;
        }
        else if (camDistance < camMinDistance)
        {
            camDistance = camMinDistance;
        }

        distanceBetween.x = Mathf.Abs(distanceBetween.x);
        distanceBetween.z = Mathf.Abs(distanceBetween.z);


        if (distanceBetween.x > 15.0f)
        {
            camOffset = distanceBetween.x * 0.3f;
            if (camOffset >= 8.5f)
                camOffset = 8.5f;
        }
        else if (distanceBetween.x < 14.0f)
        {
            camOffset = distanceBetween.x * 0.3f;
        }
        else if (distanceBetween.z < 14.0f)
        {
            camOffset = distanceBetween.x * 0.3f;
        }
        MidX = (Isa.transform.position.x + Erl.transform.position.x) / 2;
        MidY = (Isa.transform.position.y + Erl.transform.position.y) / 2;
        MidZ = (Isa.transform.position.z + Erl.transform.position.z) / 2;
        Midpoint = new Vector3(MidX, MidY, MidZ);

        Vector3 point = camera.WorldToViewportPoint(Midpoint);
        Vector3 delta = Midpoint - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDistance + camOffset)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }

    void OnDrawGizmos()
    {
        /*
        Gizmos.color = new Color(1, 0, 0, .3f);
        Gizmos.DrawCube(erlFocusArea.center, erlFocusAreaSize);

        Gizmos.color = new Color(0, 0, 1, .3f);
        Gizmos.DrawCube(isaFocusArea.center, isaFocusAreaSize);
        */
    }

    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right, top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }

            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }

            top += shiftY;
            bottom += shiftY;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}
