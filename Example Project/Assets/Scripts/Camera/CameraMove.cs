using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private void Update()
    {
        if(GamePlayStatics.LocalPlayer != null)
        {
            Vector3 eye = (Vector3.back * 7f) + (Vector3.up * 7f);
            transform.position = GamePlayStatics.LocalPlayer.transform.position + eye;
            transform.LookAt(GamePlayStatics.LocalPlayer.transform);
        }
    }
}
