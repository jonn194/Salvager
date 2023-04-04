using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoreOfMoontastic : MonoBehaviour
{
    public string m_appID_IOS = "840066184";
    public string m_appID_Android = "com.orangenose.noone";


    #if UNITY_IPHONE
	    [DllImport ("__Internal")] private static extern void _OpenAppInStore(int appID);
    #endif

    #if UNITY_ANDROID
        private static AndroidJavaObject jo;
    #endif

    void Awake()
    {
        if (!Application.isEditor)
        {
            #if UNITY_ANDROID
                jo = new AndroidJavaObject("com.moontastic.nativeappstore.NativeAppstore");
            #endif

        }
        else
        {
            Debug.Log("AppstoreHandler:: Cannot open Appstore in Editor.");
        }
    }
    public void OnButtonClick()
    {
        if (!Application.isEditor)
        {
            #if UNITY_IPHONE
                OpenAppInStore(m_appID_IOS);
            #endif

            #if UNITY_ANDROID
                OpenAppInStore(m_appID_Android);
            #endif
        }
        else
        {
            Debug.Log("AppstoreTestScene::Cannot view app in Editor.");
        }
    }

    void OpenAppInStore(string appID)
    {
        if (!Application.isEditor)
        {
            #if UNITY_IPHONE
			    int appIDIOS;

			    if(int.TryParse(appID, out appIDIOS))
			    {	
                    _OpenAppInStore(appIDIOS);
			    }
            #endif

            #if UNITY_ANDROID
                jo.Call("OpenInAppStore", "market://details?id=" + appID);
            #endif
        }
        else
        {
            Debug.Log("AppstoreHandler:: Cannot open Appstore in Editor.");
        }
    }
}
