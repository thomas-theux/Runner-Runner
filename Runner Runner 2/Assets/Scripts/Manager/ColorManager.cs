﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public static Color32 KeyWhite = new Color32(235, 245, 255, 255);
    public static Color32 KeyBlack = new Color32(35, 45, 55, 255);

    public static Color32 KeyBlack20 = new Color32(35, 45, 55, 50);

    public static Color32 KeyRed = new Color32(255, 0, 0, 255);
    public static Color32 KeyYellow = new Color32(255, 255, 0, 255);

    public static List<Color32> CharacterColors = new List<Color32>(new Color32[] {
        new Color32(0, 128, 255, 255),  // BLUE
        new Color32(255, 128, 0, 255),  // RED
        new Color32(0, 255, 128, 255),  // GREEN
        new Color32(255, 255, 0, 255)   // YELLOW
    });

    // public static Color32 CharacterBlue = new Color32(0, 128, 255, 128);
    // public static Color32 CharacterRed = new Color32(255, 128, 0, 128);
    // public static Color32 CharacterGreen = new Color32(0, 255, 128, 128);
    // public static Color32 CharacterYellow = new Color32(255, 255, 0, 128);

}
