#if UNITY_STANDALONE_WIN

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XInputDotNetPure;
using System;
using System.IO;
using UnityEditor;

//[InitializeOnLoad]
public class XGamepadManager : GamepadManager
{
	XGamepadDevice[] devices = new XGamepadDevice[4];
	GamepadLayout layout;

    
    public XGamepadManager(GamepadLayout layout)
	{
		this.layout = layout;

        var currentPath = Environment.GetEnvironmentVariable("PATH",
       EnvironmentVariableTarget.Process);
#if UNITY_EDITOR_32
    var dllPath = Application.dataPath
        + Path.DirectorySeparatorChar + "SomePath"
        + Path.DirectorySeparatorChar + "Plugins"
        + Path.DirectorySeparatorChar + "x86";
#elif UNITY_EDITOR_64
        var dllPath = Application.dataPath
            + '\\' + "Plugins"
            + '\\' + "x86_64";
        Debug.Log(dllPath);
#else // Player
    var dllPath = Application.dataPath
        + Path.DirectorySeparatorChar + "Plugins";

#endif
        if (currentPath != null && currentPath.Contains(dllPath) == false)
            Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator
                + dllPath, EnvironmentVariableTarget.Process);

    }

	public override void Init ()
	{
		Update ();
	}
	
	public override void Update ()
	{
		for(int i = 0; i < 4; i++)
		{
			GamePadState state = GamePad.GetState((PlayerIndex)i);
			if( state.IsConnected )
			{
				if( devices[i] == null )
				{
					XGamepadDevice device = new XGamepadDevice(layout);
					device.deviceId = i;

					devices[i] = device;

					AddDevice(device);
				}
			}
			else
			{
				if( devices[i] != null )
				{
					RemoveDevice(devices[i]);
					devices[i] = null;
				}
			}
		}

		for(int i = 0; i < devices.Length; i++)
		{
			if( devices[i] == null )
				continue;

			devices[i].Update();
		}
	}	
}
#endif