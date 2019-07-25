using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMinionPreset", menuName = "Presets/MinionPreset")]
public class MinionPreset: ScriptableObject
{
	public Rank Rank;
	public OnHitParticleFeedback onHitParticleFeedback;
	public float points;
	public long onHitVibrationLength;
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