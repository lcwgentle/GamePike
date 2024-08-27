using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LearnButton : Button
{
    public event Action HighlightedButton;
    public event Action PressedButton;
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        switch (state)
        {
            case SelectionState.Normal:
                break;
            case SelectionState.Highlighted:
                if(HighlightedButton!=null)
                {
                    HighlightedButton();
                }
                break;
            case SelectionState.Pressed:
                if (PressedButton != null)
                    PressedButton();
                break;
            case SelectionState.Selected:
                break;
            case SelectionState.Disabled:
                break;
        }
    }
}
