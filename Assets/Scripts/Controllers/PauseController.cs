using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseController
{
    static bool isPaused;

    public static void Pause(){
        isPaused = true;
    }

    public static void Unpause(){
        isPaused = false;
    }
}
