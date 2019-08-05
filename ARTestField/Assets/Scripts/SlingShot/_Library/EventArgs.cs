 using System;
using GoogleARCore;
using UnityEngine;

public class PlaneSelectedEventArgs : EventArgs
{
    public TrackableHit TrackableHit;
    public DetectedPlane DetectedPlane;

    public PlaneSelectedEventArgs(TrackableHit trackableHit, DetectedPlane detectedPlane)
    {
        TrackableHit = trackableHit;
        DetectedPlane = detectedPlane;
    }
}

public class UserTouchEventArgs : EventArgs
{
    public Touch Touch;
    public UserTouchEventArgs(Touch touch)
    {
        Touch = touch;
    }
}

public class MinionOnHitEventArgs : EventArgs
{
	public int MinionValue;
	public MinionModule MinionModule;
	public MinionOnHitEventArgs(int minionValue, MinionModule minionModule)
	{
		MinionValue = minionValue;
		MinionModule = minionModule;
	}
}

public class NodeConnectionsGeneratedEventArgs : EventArgs
{
	public bool Finished;
	public NodeConnectionsGeneratedEventArgs(bool finished)
	{
		Finished = finished;
	}
}
