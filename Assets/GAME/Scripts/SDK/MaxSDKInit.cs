using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSDKInit : MonoBehaviour
{
    void Awake()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => 
        {
            // AppLovin SDK is initialized, start loading ads
        };

        MaxSdk.SetSdkKey("5AAhiuFzwRBZXL6NRkfMQIFE9TpJ-fX4qinXb1VVTh4_1ANSv1qJJ3TSWLnV_Jaq1LLcMr7rXCqTMC0FDqZXu6");
        // MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }
}
