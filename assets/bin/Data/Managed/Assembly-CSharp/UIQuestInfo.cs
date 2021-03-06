using UnityEngine;

public class UIQuestInfo : MonoBehaviourSingleton<UIQuestInfo>
{
	[SerializeField]
	protected UILabel questName;

	[SerializeField]
	protected UILabel timeLabel;

	private void Start()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() || QuestManager.IsValidInGameWaveMatch())
		{
			this.get_gameObject().SetActive(false);
		}
		else
		{
			if (QuestManager.IsValidInGame() && !MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				if (questName != null)
				{
					string currentQuestName = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestName();
					if (!string.IsNullOrEmpty(currentQuestName))
					{
						questName.text = currentQuestName;
					}
				}
			}
			else
			{
				this.get_gameObject().SetActive(false);
			}
			if (MonoBehaviourSingleton<QuestManager>.I.IsExplore() || MonoBehaviourSingleton<InGameManager>.I.IsRush())
			{
				this.get_gameObject().SetActive(false);
			}
		}
	}

	private void LateUpdate()
	{
		timeLabel.text = MonoBehaviourSingleton<InGameProgress>.I.GetRemainTime();
	}
}
