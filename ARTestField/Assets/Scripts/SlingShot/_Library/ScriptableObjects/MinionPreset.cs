using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMinionPreset", menuName = "Presets/MinionPreset")]
public class MinionPreset: ScriptableObject
{
	public Rank Rank;
	public OnHitParticleFeedback onHitParticleFeedback;
	public float points;
	public long onHitVibrationLength;
	public PathingInformation? pathingInformation;
}

public enum Rank
{
	Normal = 1,
	Magic = 2,
	Rare = 3,
};

public enum OnHitParticleFeedback
{
	None = 0,
	HueExplosion = 1
}

public struct PathingInformation
{
	public PathingAlgorithm pathingAlgorithm;
	public string pathName;
	public float speedMultiplier;
}

public enum PathingAlgorithm
{
	AStar = 0
}