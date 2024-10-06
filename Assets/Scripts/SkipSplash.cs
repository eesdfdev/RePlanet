using UnityEngine;
using UnityEngine.Rendering;


public class SkipSplash : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void BeforeSplashScreen()
    {
        System.Threading.Tasks.Task.Run(AsyncSkip);
    }
    private static void AsyncSkip()
    {
        SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
    }
}
