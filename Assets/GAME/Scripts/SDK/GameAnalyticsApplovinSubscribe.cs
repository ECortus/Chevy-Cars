using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsApplovinSubscribe : MonoBehaviour
{
    void Start ()
    {
        GameAnalyticsILRD.SubscribeMaxImpressions();
        
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // Show Mediation Debugger
            MaxSdk.ShowMediationDebugger();
        };
    }
}
