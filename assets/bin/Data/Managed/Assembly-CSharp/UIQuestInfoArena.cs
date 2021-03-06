using UnityEngine;

public class UIQuestInfoArena : MonoBehaviourSingleton<UIQuestInfoArena>
{
	[SerializeField]
	protected UILabel timeArenaText;

	[SerializeField]
	protected UILabel waveMax;

	[SerializeField]
	protected UILabel waveNow;

	private InGameManager m_inGameMgr;

	private InGameProgress m_inGameProgress;

	private int m_wave;

	private bool m_isTimeAttack;

	private void Start()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (!IsArena())
		{
			this.get_gameObject().SetActive(false);
		}
		else
		{
			this.get_gameObject().SetActive(true);
			m_inGameMgr = MonoBehaviourSingleton<InGameManager>.I;
			m_inGameProgress = MonoBehaviourSingleton<InGameProgress>.I;
			waveMax.text = $"/{m_inGameMgr.GetArenaWaveMax()}";
			m_wave = m_inGameMgr.GetCurrentArenaWaveNum();
			SetWaveNow(m_wave);
			m_isTimeAttack = m_inGameMgr.IsArenaTimeAttack();
		}
	}

	private void LateUpdate()
	{
		string text = (!m_isTimeAttack) ? m_inGameProgress.GetArenaRemainTimeToString() : m_inGameProgress.GetArenaElapseTimeToString();
		timeArenaText.text = text;
		if (m_wave != m_inGameMgr.GetCurrentArenaWaveNum())
		{
			m_wave = m_inGameMgr.GetCurrentArenaWaveNum();
			SetWaveNow(m_wave);
		}
	}

	private bool IsArena()
	{
		return QuestManager.IsValidInGame() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo();
	}

	public void SetWaveNow(int num)
	{
		waveNow.text = num.ToString();
	}
}
