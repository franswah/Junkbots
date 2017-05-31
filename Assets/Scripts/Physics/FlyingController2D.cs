﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController2D : RaycastController {
	public CollisionInfo collisions;

    [HideInInspector]
    public Vector2 playerInput;

    public override void Start() {
        base.Start();
    }

    public void Move(Vector3 velocity) {
        Move(velocity, Vector2.zero);
    }

    public void Move(Vector3 velocity, Vector2 input) {
		UpdateRaycastOrigins ();
		collisions.Reset ();
        collisions.velocityOld = velocity;
        playerInput = input;

		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}

		transform.Translate (velocity);
	}

	void HorizontalCollisions(ref Vector3 velocity) {
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;

		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1) ? 
				raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.right * -directionX * rayLength, Color.red);

			if (hit) {
                if (hit.distance == 0) {
                    continue;
                }

                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
			}
		}
	}

	void VerticalCollisions(ref Vector3 velocity) {
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? 
				raycastOrigins.bottomLeft : raycastOrigins.topLeft;

			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = directionY == -1;
				collisions.above = directionY == 1;
			}
		}
	}

    void ClimbSlope(ref Vector3 velocity, float slopeAngle) {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        
        if (velocity.y <= climbVelY) {
            velocity.y = climbVelY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);

            collisions.below = true;
        }
        
    }

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;

        public Vector3 velocityOld;

		public void Reset () {
			above = below = left = right = false;
		}
	}
}