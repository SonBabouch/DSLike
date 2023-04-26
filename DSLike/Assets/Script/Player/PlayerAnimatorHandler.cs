using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP
{
    public class PlayerAnimatorHandler : MonoBehaviour
    {
        public Animator animPlayer;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            animPlayer = GetComponent<Animator>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float VerticalMovement, float horizontalMovement)
        {
            #region Vertical
            float vert = 0;

            if(VerticalMovement > 0 && VerticalMovement < 0.55f)
            {
                vert = 0.5f;
            }
            else if(VerticalMovement > 0.55f)
            {
                vert = 1;
            }
            else if(VerticalMovement < 0 && VerticalMovement > -0.55f)
            {
                vert = -0.5f;
            }
            else if(VerticalMovement < -0.55f)
            {
                vert = -1;
            }
            else
            {
                vert = 0;
            }
            #endregion

            #region Horizontal
            float hor = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                hor = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                hor = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                hor = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                hor = -1;
            }
            else
            {
                hor = 0;
            }
            #endregion

            animPlayer.SetFloat(vertical, vert, 0.1f, Time.deltaTime);
            animPlayer.SetFloat(horizontal, hor, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }

    }
}

