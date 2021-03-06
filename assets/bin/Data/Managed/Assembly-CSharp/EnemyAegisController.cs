using System.Collections.Generic;
using UnityEngine;

public class EnemyAegisController
{
	public class SetupParam
	{
		public int maxNum;

		public int maxHp;

		public int generateSeId;

		public int breakSeId;

		public int allBreakSeId;

		public Vector3 offset;

		public float scale;

		public string effectName = string.Empty;

		public string nodeName = string.Empty;

		public int nowNum;

		public int nowHp;
	}

	public class SyncParam
	{
		public bool isChange;

		public int nowNum;

		public int nowHp;

		public void Copy(SyncParam sync)
		{
			isChange = sync.isChange;
			nowNum = sync.nowNum;
			nowHp = sync.nowHp;
		}

		public bool Equal(SyncParam s)
		{
			return nowNum == s.nowNum && nowHp == s.nowHp;
		}
	}

	public class AegisParam
	{
		public int hp;

		public Transform effect;
	}

	private const float kAngularVelocity = 50f;

	private SetupParam _setupParam = new SetupParam();

	private SyncParam _syncParam = new SyncParam();

	private List<AegisParam> aegisParams = new List<AegisParam>();

	private Enemy owner;

	private Transform cachedTransform;

	private List<Transform> effectParentList = new List<Transform>();

	private bool isNeedUpdate;

	private float nowAngle;

	public SyncParam syncParam => _syncParam;

	public EnemyAegisController()
		: this()
	{
	}

	public void Init(Enemy enemy)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (!object.ReferenceEquals(enemy, null))
		{
			owner = enemy;
			cachedTransform = this.get_transform();
			isNeedUpdate = false;
			nowAngle = 0f;
			cachedTransform.set_localRotation(Quaternion.get_identity());
		}
	}

	public void Generate(AnimEventData.EventData data)
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		if (!object.ReferenceEquals(owner, null))
		{
			_setupParam.maxNum = data.intArgs[0];
			_setupParam.maxHp = (int)((float)data.intArgs[1] * 0.01f * (float)owner.hpMax);
			_setupParam.generateSeId = ((data.intArgs.Length >= 3) ? data.intArgs[2] : 0);
			_setupParam.breakSeId = ((data.intArgs.Length >= 4) ? data.intArgs[3] : 0);
			_setupParam.allBreakSeId = ((data.intArgs.Length >= 5) ? data.intArgs[4] : 0);
			_setupParam.offset = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			_setupParam.scale = data.floatArgs[3];
			_setupParam.effectName = data.stringArgs[0];
			_setupParam.nodeName = ((data.stringArgs.Length <= 1) ? string.Empty : data.stringArgs[1]);
			_setupParam.nowNum = _setupParam.maxNum;
			_setupParam.nowHp = _setupParam.maxHp;
			Setup(_setupParam, true);
		}
	}

	public void Setup(SetupParam param, bool announce)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		_setupParam = param;
		if (_setupParam.maxNum > 0)
		{
			for (int i = 0; i < aegisParams.Count; i++)
			{
				AegisParam aegisParam = aegisParams[i];
				_ReleaseEffect(ref aegisParam.effect, true);
			}
			aegisParams.Clear();
			Transform val = owner.FindNode(_setupParam.nodeName);
			cachedTransform.SetParent(val);
			cachedTransform.set_localPosition(Vector3.get_zero());
			cachedTransform.set_localScale(Vector3.get_one());
			float num = 360f / (float)_setupParam.maxNum;
			float num2 = (float)(_setupParam.maxNum - _setupParam.nowNum);
			bool flag = true;
			for (int j = 0; j < _setupParam.maxNum; j++)
			{
				Transform val3;
				if (effectParentList.Count <= j)
				{
					GameObject val2 = new GameObject("Child");
					val3 = val2.get_transform();
					val3.SetParent(cachedTransform);
					val3.set_localPosition(Vector3.get_zero());
					val3.set_localScale(Vector3.get_one());
					val3.set_localRotation(Quaternion.get_identity());
					effectParentList.Add(val3);
				}
				else
				{
					val3 = effectParentList[j];
				}
				val3.set_localRotation(Quaternion.AngleAxis(num * (float)j, Vector3.get_up()));
				if (!((float)j < num2))
				{
					AegisParam aegisParam2 = new AegisParam();
					aegisParam2.hp = ((!flag) ? _setupParam.maxHp : _setupParam.nowHp);
					aegisParam2.effect = EffectManager.GetEffect(_setupParam.effectName, val3);
					aegisParam2.effect.set_localPosition(_setupParam.offset);
					aegisParam2.effect.set_localScale(new Vector3(_setupParam.scale, _setupParam.scale, _setupParam.scale));
					aegisParam2.effect.set_localRotation(Quaternion.get_identity());
					aegisParams.Add(aegisParam2);
					flag = false;
				}
			}
			isNeedUpdate = true;
			_syncParam.nowNum = _setupParam.nowNum;
			_syncParam.nowHp = _setupParam.nowHp;
			_UpdateGaugeUI();
			if (announce && MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(owner.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 2u);
				if (_setupParam.generateSeId > 0 && MonoBehaviourSingleton<SoundManager>.IsValid())
				{
					SoundManager.PlayOneShotSE(_setupParam.generateSeId, null, null);
				}
			}
		}
	}

	public SetupParam GetSetupParam()
	{
		_setupParam.nowNum = _syncParam.nowNum;
		_setupParam.nowHp = _syncParam.nowHp;
		return _setupParam;
	}

	public bool Damage(int damage)
	{
		if (!IsValid())
		{
			return false;
		}
		if (damage <= 0)
		{
			return false;
		}
		AegisParam aegisParam = aegisParams[0];
		aegisParam.hp -= damage;
		if (aegisParam.hp <= 0)
		{
			_ReleaseEffect(ref aegisParam.effect, true);
			aegisParams.RemoveAt(0);
			_syncParam.nowNum = aegisParams.Count;
			_syncParam.nowHp = ((_syncParam.nowNum != 0) ? aegisParams[0].hp : 0);
			_UpdateGaugeUI();
			if (MonoBehaviourSingleton<SoundManager>.IsValid())
			{
				if (_setupParam.allBreakSeId > 0 && !IsValid())
				{
					SoundManager.PlayOneShotSE(_setupParam.allBreakSeId, null, null);
				}
				else if (_setupParam.breakSeId > 0)
				{
					SoundManager.PlayOneShotSE(_setupParam.breakSeId, null, null);
				}
			}
		}
		else
		{
			_syncParam.nowHp = aegisParam.hp;
		}
		_syncParam.isChange = true;
		if (!IsValid())
		{
			isNeedUpdate = false;
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(owner.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 3u);
			}
		}
		return true;
	}

	public void Sync(SyncParam sync)
	{
		if (sync.isChange && !_syncParam.Equal(sync))
		{
			int num = _syncParam.nowNum - sync.nowNum;
			_syncParam.isChange = false;
			_syncParam.nowNum = sync.nowNum;
			_syncParam.nowHp = sync.nowHp;
			for (int i = 0; i < num; i++)
			{
				if (aegisParams.Count > 0)
				{
					_ReleaseEffect(ref aegisParams[0].effect, true);
					aegisParams.RemoveAt(0);
					_UpdateGaugeUI();
					if (MonoBehaviourSingleton<SoundManager>.IsValid())
					{
						if (_setupParam.allBreakSeId > 0 && !IsValid())
						{
							SoundManager.PlayOneShotSE(_setupParam.allBreakSeId, null, null);
						}
						else if (_setupParam.breakSeId > 0)
						{
							SoundManager.PlayOneShotSE(_setupParam.breakSeId, null, null);
						}
					}
				}
			}
			if (aegisParams.Count > 0)
			{
				aegisParams[0].hp = sync.nowHp;
			}
			else
			{
				isNeedUpdate = false;
				if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
				{
					MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(owner.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 3u);
				}
			}
		}
	}

	public bool IsValid()
	{
		return aegisParams.Count > 0;
	}

	public void FlagReset()
	{
		_syncParam.isChange = false;
	}

	public float GetPercent()
	{
		if (_setupParam.maxNum <= 0)
		{
			return 0f;
		}
		if (_syncParam.nowNum == 0)
		{
			return 0f;
		}
		return (float)_syncParam.nowNum / (float)_setupParam.maxNum;
	}

	private void Update()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (isNeedUpdate)
		{
			nowAngle += 50f * Time.get_deltaTime();
			if (nowAngle >= 360f)
			{
				nowAngle -= 360f;
			}
			cachedTransform.set_localRotation(Quaternion.AngleAxis(nowAngle, Vector3.get_up()));
		}
	}

	private void OnDestroy()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		_setupParam = null;
		_syncParam = null;
		isNeedUpdate = false;
		for (int i = 0; i < aegisParams.Count; i++)
		{
			AegisParam aegisParam = aegisParams[i];
			_ReleaseEffect(ref aegisParam.effect, true);
		}
		aegisParams.Clear();
		aegisParams = null;
		if (!object.ReferenceEquals(effectParentList, null))
		{
			for (int j = 0; j < effectParentList.Count; j++)
			{
				Object.Destroy(effectParentList[j].get_gameObject());
			}
			effectParentList.Clear();
		}
		effectParentList = null;
	}

	private void _UpdateGaugeUI()
	{
		if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && _setupParam.maxNum > 0)
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.SetAegisBarPercent(GetPercent());
		}
	}

	private void _ReleaseEffect(ref Transform t, bool isPlayEndAnimation = true)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && !object.ReferenceEquals(t, null))
		{
			EffectManager.ReleaseEffect(t.get_gameObject(), isPlayEndAnimation, false);
			t = null;
		}
	}
}
