using UnityEngine;

[CreateAssetMenu(fileName = "NewMinionPreset", menuName = "Presets/MinionPreset")]
public class MinionPreset: ScriptableObject
{
	public Rank rank;
	public OnHitParticleFeedback onHitParticleFeedback;
	public long onHitVibrationLength;
}

public enum Rank
{
	None = 0,
	Normal = 1,
	Magic = 2,
	Rare = 3,
};

public enum OnHitParticleFeedback
{
	None = 0,
	HueExplosion = 1
}
