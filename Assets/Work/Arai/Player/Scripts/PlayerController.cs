﻿using UnityEngine;
using Leap;

public class PlayerController : MonoBehaviour
{


    #region vertical property
    #region advance property
    public bool isInputForwardMovement
    {
        get
        {
            if (leap_contoller_.IsConnected) return isBothHandsFront;
            return Input.GetAxis("Vertical") <= 0.0f;
        }
    }
    #endregion

    #region reverse property
    public bool isInputBackwardMovement
    {
        get
        {
            if (leap_contoller_.IsConnected) return isBothHandsBack;
            return Input.GetAxis("Vertical") >= 0.0f;
        }
    }

    #endregion

    public float getInputVerticalValue
    {
        get
        {
            if (!leap_contoller_.IsConnected) return Input.GetAxis("Vertical");

            if (isBothHandsFront)
            {
                return Mathf.Max(left_hand_input_.getVerticalValue, right_hand_input_.getVerticalValue);
            }
            else if (isBothHandsBack)
            {
                return Mathf.Min(left_hand_input_.getVerticalValue, right_hand_input_.getVerticalValue);
            }
            else
            {
                return 0.0f;
            }

        }
    }
    #endregion

    #region horizontal property
    #region rightmove property
    public bool isInputRightMovement
    {
        get
        {
            if (leap_contoller_.IsConnected) return isBothHandsRight;
            return Input.GetAxis("Horizontal") <= 0.0f;
        }
    }

    #endregion

    #region leftmove property
    public bool isInputLeftMovement
    {
        get
        {
            if (leap_contoller_.IsConnected) return isBothHandsLeft;
            return Input.GetAxis("Horizontal") <= 0.0f;
        }
    }

    #endregion

    public float getInputHorizontalValue
    {
        get
        {
            if (!leap_contoller_.IsConnected) return Input.GetAxis("Horizontal");

            if (isBothHandsRight)
            {
                return Mathf.Max(left_hand_input_.getHorizaontalValue, right_hand_input_.getHorizaontalValue);
            }
            else if (isBothHandsLeft)
            {
                return Mathf.Min(left_hand_input_.getHorizaontalValue, right_hand_input_.getHorizaontalValue);
            }
            else
            {
                return 0.0f;
            }
        }
    }
    #endregion

    #region rotate property

    public float getRotateValue
    {
        get
        {

            if (!leap_contoller_.IsConnected) return Input.GetAxis("Rotate");
            if (NullCheck()) return 0.0f;

            if (left_hand_input_.isFront && right_hand_input_.isBack)
            {
                return left_hand_input_.getVerticalValue;
            }
            else if (left_hand_input_.isBack && right_hand_input_.isFront)
            {
                return -right_hand_input_.getVerticalValue;
            }
            else
            {
                return 0.0f;
            }

        }
    }

    #endregion

    #region jump property

    public bool isInputJump
    {
        get
        {

            if (!leap_contoller_.IsConnected) return Input.GetKeyDown(KeyCode.Space);
            if (NullCheck()) return false;
            if (!(right_hand_input_.isRight && left_hand_input_.isLeft)) return false;

            if (!(right_hand_input_.getHorizaontalValue > 0.3f &&
                left_hand_input_.getHorizaontalValue < -0.3f)) return false;
            return true;
        }
    }

    public float getJumpValue
    {
        get
        {
            return Input.GetAxis("Jump");
        }
    }

    #endregion

    #region fire property

    #region right

    public bool isInputRightAttack
    {
        get
        {
            if (!leap_contoller_.IsConnected) return Input.GetMouseButton(1);
            if (right_hand_input_ == null) return false;
            return right_hand_input_.isGrabed;
        }
    }

    #endregion

    #region left

    public bool isInputLeftAttack
    {
        get
        {
            if (!leap_contoller_.IsConnected) return Input.GetMouseButton(0);
            if (left_hand_input_ == null) return false;
            return left_hand_input_.isGrabed;
        }
    }

    #endregion

    #endregion

    #region boost property

    public bool isInputBoost
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
    }

    #endregion


    public void SetLeftHandInput(LeftHandInput left_hand_input)
    {
        left_hand_input_ = left_hand_input;
    }

    public void SetRightHandInput(RightHandInput right_hand_input)
    {
        right_hand_input_ = right_hand_input;
    }

    void Start()
    {
        leap_contoller_ = FindObjectOfType<HandController>().GetLeapController();
    }

    bool NullCheck()
    {
        return right_hand_input_ == null || left_hand_input_ == null;
    }

    bool isBothHandsFront
    {
        get
        {
            if (NullCheck()) return false;
            return left_hand_input_.isFront && right_hand_input_.isFront;
        }
    }

    bool isBothHandsBack
    {
        get
        {
            if (NullCheck()) return false;
            return left_hand_input_.isBack && right_hand_input_.isBack;
        }
    }

    bool isBothHandsRight
    {
        get
        {
            if (NullCheck()) return false;
            return left_hand_input_.isRight && right_hand_input_.isRight;
        }
    }

    bool isBothHandsLeft
    {
        get
        {
            if (NullCheck()) return false;
            return left_hand_input_.isLeft && right_hand_input_.isLeft;
        }
    }

    Controller leap_contoller_ = null;
    LeftHandInput left_hand_input_ = null;
    RightHandInput right_hand_input_ = null;
}