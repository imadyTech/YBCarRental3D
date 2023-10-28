using imady.NebuEvent;
using imady.NebuUI;
using UnityEngine;
using YBCarRental3D;
using Newtonsoft.Json;


public class YBCameraObject : NebuEventUnityObjectBase, 
    INebuObserver<YBCarSelectedMessage>,
    INebuObserver<YBCarDeSelectedMessage>
{
    public Vector3 _demoPosition = new Vector3(100.040001f, 100.209999f, 100.559998f);
    public GameObject _camera;


    public void OnNext(YBCarSelectedMessage message)
    {
        try
        {
            this._camera.SetActive(true);
        }
        catch { }
    }

    public void OnNext(YBCarDeSelectedMessage message)
    {
        this._camera.SetActive(false);
    }
}