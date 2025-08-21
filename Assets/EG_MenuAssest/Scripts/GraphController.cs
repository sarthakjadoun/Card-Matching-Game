using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettingsController : MonoBehaviour
{
    public void VeryLow()
    {
        QualitySettings.SetQualityLevel(0, true); // Very Low
    }

    public void Low()
    {
        QualitySettings.SetQualityLevel(1, true); // Low
    }

    public void Medium()
    {
        QualitySettings.SetQualityLevel(2, true); // Medium
    }

    public void High()
    {
        QualitySettings.SetQualityLevel(3, true); // High
    }

    public void VeryHigh()
    {
        QualitySettings.SetQualityLevel(4, true); // Very High
    }

    public void Ultra()
    {
        QualitySettings.SetQualityLevel(5, true); // Ultra
    }
}
