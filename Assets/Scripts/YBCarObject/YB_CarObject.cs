using imady.NebuEvent;
using imady.NebuUI;
using UnityEngine;
using YBCarRental3D;

public class YB_CarObject : NebuEventUnityObjectBase, INebuObserver<NebuDataMessage<string>>
{
    public Light LeftLight;
    public Light RightLight;


    public void OnNext(NebuDataMessage<string> message)
    {
        try
        {
            var lights = this.gameObject.GetComponentsInChildren<Light>();
            foreach (var light in lights)
                light.intensity = 0.5f;

        }
        catch { }
    }
}