using System.Collections.Generic;
using UnityEngine;

public class OpponentMemory
{
	public class Counter
	{
		public int viewNum;

		public int frontNum;

		public int backNum;

		public int rightNum;

		public int leftNum;

		public int nearNum;

		public void Clear()
		{
			viewNum = 0;
			frontNum = 0;
			backNum = 0;
			rightNum = 0;
			leftNum = 0;
			nearNum = 0;
		}
	}

	public class RecordData
	{
		public float distance;

		public float distanceFront;

		public DISTANCE distanceType;

		public bool isView;

		public float rootAngle;

		public float frontAngle;

		public PLACE place;

		public bool isNearPlace;

		public float attackPosDistance;

		public float moveLength;

		public PLACE placeOfOpponent;

		public bool isDamaged;

		public Vector3 pos
		{
			get;
			set;
		}

		public Vector3 attackPos
		{
			get;
			set;
		}

		public override string ToString()
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			string empty = string.Empty;
			empty = string.Concat((object)empty, (object)"pos=", (object)pos);
			empty = empty + ", len=" + distance;
			empty = empty + ", front=" + distanceFront;
			empty = empty + ", D=" + distanceType;
			empty = empty + ", P=" + place;
			empty = empty + ", angle=" + rootAngle;
			empty = empty + ", frontAngle=" + frontAngle;
			empty = empty + ", View=" + isView;
			empty = empty + ", Near=" + isNearPlace;
			return empty + ", moveLength=" + moveLength;
		}
	}

	public class OpponentRecord
	{
		public StageObject obj;

		public RecordData record = new RecordData();

		public Hate hate = new Hate();

		public OpponentRecord(StageObject obj)
		{
			this.obj = obj;
		}

		public bool IsAlive()
		{
			return !(obj != null) || !(obj is Character) || !(obj as Character).isDead;
		}
	}

	private Brain brain;

	public Counter counter = new Counter();

	public UIntKeyTable<OpponentRecord> opponentRecords = new UIntKeyTable<OpponentRecord>();

	private OpponentRecord emptyOpponent = new OpponentRecord(null);

	public HateParam hateParam
	{
		get;
		private set;
	}

	public int turnCountForHateCycle
	{
		get;
		private set;
	}

	public bool haveHateControl => hateParam != null;

	public OpponentMemory(Brain brain)
	{
		this.brain = brain;
	}

	public OpponentRecord Find(StageObject obj)
	{
		if (obj == null)
		{
			return null;
		}
		return opponentRecords.Get((uint)obj.id);
	}

	public OpponentRecord FindOrEmpty(StageObject obj)
	{
		OpponentRecord opponentRecord = Find(obj);
		return (opponentRecord == null) ? emptyOpponent : opponentRecord;
	}

	public OpponentRecord FindOrRegist(StageObject obj)
	{
		if (obj == null)
		{
			return emptyOpponent;
		}
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			opponentRecord = new OpponentRecord(obj);
			opponentRecords.Add((uint)obj.id, opponentRecord);
		}
		return opponentRecord;
	}

	public void Remove(StageObject obj)
	{
		opponentRecords.Remove((uint)obj.id);
	}

	public List<OpponentRecord> GetListOfSensedOpponent()
	{
		List<OpponentRecord> list = new List<OpponentRecord>();
		opponentRecords.ForEach(delegate(OpponentRecord o)
		{
			if (o.IsAlive())
			{
				list.Add(o);
			}
		});
		return list;
	}

	public void Update()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		BrainParam.SensorParam sensorParam = brain.param.sensorParam;
		Character owner = brain.owner;
		Vector2 owner_pos2 = owner.positionXZ;
		Vector2 owner_forward2 = owner.forwardXZ;
		counter.Clear();
		bool isAutoMode = false;
		TargetPoint actionTargetPoint = null;
		if (brain.owner is Self)
		{
			Self self = brain.owner as Self;
			isAutoMode = self.isAutoMode;
			if (isAutoMode)
			{
				AutoSelfController autoSelfController = self.controller as AutoSelfController;
				actionTargetPoint = autoSelfController.actionTargetPoint;
			}
		}
		List<StageObject> targetObjectList = brain.GetTargetObjectList();
		targetObjectList.ForEach(delegate(StageObject opponent_obj)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0351: Unknown result type (might be due to invalid IL or missing references)
			//IL_0356: Unknown result type (might be due to invalid IL or missing references)
			//IL_0383: Unknown result type (might be due to invalid IL or missing references)
			//IL_0388: Unknown result type (might be due to invalid IL or missing references)
			//IL_038a: Unknown result type (might be due to invalid IL or missing references)
			//IL_038c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0392: Unknown result type (might be due to invalid IL or missing references)
			//IL_0397: Unknown result type (might be due to invalid IL or missing references)
			//IL_039c: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0408: Unknown result type (might be due to invalid IL or missing references)
			//IL_042c: Unknown result type (might be due to invalid IL or missing references)
			//IL_042f: Unknown result type (might be due to invalid IL or missing references)
			//IL_043f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0444: Unknown result type (might be due to invalid IL or missing references)
			//IL_0446: Unknown result type (might be due to invalid IL or missing references)
			//IL_044b: Unknown result type (might be due to invalid IL or missing references)
			//IL_044e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0453: Unknown result type (might be due to invalid IL or missing references)
			Character character = opponent_obj as Character;
			if (!(character != null) || !character.isDead)
			{
				OpponentRecord opponentRecord = FindOrRegist(opponent_obj);
				bool flag = opponentRecord.record != null;
				Vector3 client_pos;
				if (flag)
				{
					client_pos = opponentRecord.record.pos;
				}
				else
				{
					client_pos = Vector3.get_zero();
					opponentRecord.record = new RecordData();
				}
				RecordData record = opponentRecord.record;
				record.pos = owner.GetTargetPosition(opponent_obj);
				Vector2 val = record.pos.ToVector2XZ();
				Vector2 p = val - owner_pos2;
				Vector2 p2 = val - brain.frontPositionXZ;
				record.distance = p.get_magnitude();
				record.distanceFront = p2.get_magnitude();
				record.rootAngle = Utility.Angle360(owner_forward2, p);
				record.frontAngle = Utility.Angle360(owner_forward2, p2);
				record.isView = false;
				if (record.distanceFront <= sensorParam.viewDistance)
				{
					float num = (record.frontAngle + sensorParam.viewAngle / 2f) % 360f;
					if (num < sensorParam.viewAngle)
					{
						record.isView = true;
						counter.viewNum++;
					}
				}
				record.place = AIUtility.GetPlaceOfAngle360(record.rootAngle);
				switch (record.place)
				{
				case PLACE.RIGHT:
					counter.rightNum++;
					break;
				case PLACE.LEFT:
					counter.leftNum++;
					break;
				case PLACE.FRONT:
					counter.frontNum++;
					break;
				case PLACE.BACK:
					counter.backNum++;
					break;
				}
				record.isNearPlace = false;
				if (record.distance <= sensorParam.nearCheckDistance)
				{
					record.isNearPlace = true;
					counter.nearNum++;
				}
				float num2 = record.distance - brain.rootInternalRedius;
				switch (record.place)
				{
				case PLACE.FRONT:
					num2 -= brain.rootFrontDistance;
					break;
				case PLACE.BACK:
					num2 -= brain.rootBackDistance;
					break;
				}
				if (num2 < sensorParam.shortDistance)
				{
					record.distanceType = DISTANCE.SHORT_SHORT;
				}
				else if (num2 >= sensorParam.shortDistance && num2 < sensorParam.middleDistance)
				{
					record.distanceType = DISTANCE.SHORT;
				}
				else if (num2 >= sensorParam.middleDistance && num2 < sensorParam.longDistance)
				{
					record.distanceType = DISTANCE.MIDDLE;
				}
				else
				{
					record.distanceType = DISTANCE.LONG;
				}
				Vector3 val2 = record.pos;
				float attackPosDistance = record.distance;
				if (isAutoMode)
				{
					if (actionTargetPoint != null)
					{
						val2 = actionTargetPoint.GetTargetPoint();
						Vector2 val3 = val2.ToVector2XZ() - owner_pos2;
						attackPosDistance = val3.get_magnitude();
					}
				}
				else if (owner is Player)
				{
					Player player = owner as Player;
					if (player.targetingPoint != null)
					{
						val2 = player.targetingPoint.GetTargetPoint();
						Vector2 val4 = val2.ToVector2XZ() - owner_pos2;
						attackPosDistance = val4.get_magnitude();
					}
				}
				record.attackPos = val2;
				record.attackPosDistance = attackPosDistance;
				record.moveLength = 0f;
				if (flag)
				{
					record.moveLength = AIUtility.GetLengthWithBetweenPosition(client_pos, record.pos);
				}
				Vector2 p3 = owner_pos2 - val;
				float angle = Utility.Angle360(opponent_obj.forwardXZ, p3);
				record.placeOfOpponent = AIUtility.GetPlaceOfAngle360(angle);
			}
		});
	}

	public DISTANCE GetDistance(float distanceSqr)
	{
		BrainParam.SensorParam sensorParam = brain.param.sensorParam;
		float num = sensorParam.shortDistance * sensorParam.shortDistance;
		float num2 = sensorParam.middleDistance * sensorParam.middleDistance;
		float num3 = sensorParam.longDistance * sensorParam.longDistance;
		if (distanceSqr < num)
		{
			return DISTANCE.SHORT_SHORT;
		}
		if (distanceSqr >= num && distanceSqr < num2)
		{
			return DISTANCE.SHORT;
		}
		if (distanceSqr >= num2 && distanceSqr < num3)
		{
			return DISTANCE.MIDDLE;
		}
		return DISTANCE.LONG;
	}

	public void OnTargetOpponent(StageObject now_target, StageObject prev_target)
	{
		Hate hate = GetHate(now_target);
		hate.cycleLockCount++;
		hate.totalLockCount++;
		if (now_target == prev_target)
		{
			hate.continuousLockCount++;
		}
		else
		{
			GetHate(prev_target).continuousLockCount = 0;
		}
	}

	public bool IsPlaceOpponent(StageObject obj, PLACE place)
	{
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return false;
		}
		return opponentRecord.record.place == place;
	}

	public bool IsAttackableOpponent(StageObject obj)
	{
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return false;
		}
		return opponentRecord.record.attackPosDistance < brain.weaponCtrl.GetAttackReach();
	}

	public bool IsSpecialAttackableOpponent(StageObject obj)
	{
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return false;
		}
		return opponentRecord.record.attackPosDistance < brain.weaponCtrl.GetSpecialReach();
	}

	public bool IsAvoidAttackableOpponent(StageObject obj)
	{
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return false;
		}
		return opponentRecord.record.attackPosDistance < brain.weaponCtrl.GetAvoidAttackReach();
	}

	public bool IsArrivalPosition(StageObject obj)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return false;
		}
		return brain.owner.IsArrivalPosition(opponentRecord.record.pos, 0f);
	}

	public bool IsArrivalAttackPosition(StageObject obj)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return false;
		}
		return brain.owner.IsArrivalPosition(opponentRecord.record.attackPos, 0f);
	}

	public float GetLengthWithAttackPos(StageObject obj, Vector3 check_pos)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return 0f;
		}
		return AIUtility.GetLengthWithBetweenPosition(opponentRecord.record.attackPos, check_pos);
	}

	public bool IsOverMoveLength(StageObject obj, float len)
	{
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return false;
		}
		return len <= opponentRecord.record.moveLength;
	}

	public void SetHateParam(uint id)
	{
		EnemyPersonalityTable.Data data = Singleton<EnemyPersonalityTable>.I.GetData(id);
		if (data != null)
		{
			SetHateParam(data.param);
		}
	}

	public void SetHateParam(HateParam data)
	{
		hateParam = data;
	}

	public bool IsHateCycleLastTurn()
	{
		if (!haveHateControl)
		{
			return false;
		}
		return turnCountForHateCycle == hateParam.cycleTurnMax;
	}

	public Hate GetHate(StageObject obj)
	{
		OpponentRecord opponentRecord = FindOrRegist(obj);
		return opponentRecord.hate;
	}

	public void AddHate(StageObject obj, int hate_val, Hate.TYPE type)
	{
		Hate hate = GetHate(obj);
		hate.val[(int)type] = Mathf.Min(hate.val[(int)type] + hate_val, 1000);
		if (hate.val[(int)type] < 0)
		{
			hate.val[(int)type] = 0;
		}
		hate.turnVal += hate_val;
		if (hate.turnVal < 0)
		{
			hate.turnVal = 0;
		}
	}

	public void UpdateHate()
	{
		if (haveHateControl)
		{
			bool flag = false;
			turnCountForHateCycle++;
			if (turnCountForHateCycle > hateParam.cycleTurnMax)
			{
				turnCountForHateCycle = 0;
				flag = true;
			}
			List<OpponentRecord> listOfSensedOpponent = GetListOfSensedOpponent();
			for (int i = 0; i < listOfSensedOpponent.Count; i++)
			{
				OpponentRecord opponentRecord = listOfSensedOpponent[i];
				if (opponentRecord.IsAlive())
				{
					opponentRecord.hate.val[0] = hateParam.distanceHateParams[(int)opponentRecord.record.distanceType];
					Player player = opponentRecord.obj as Player;
					if (player != null)
					{
						int num = (int)Mathf.Lerp(1000f, 0f, (float)player.hp / (float)player.hpMax);
						opponentRecord.hate.val[1] = num;
					}
					if (opponentRecord.obj == brain.targetCtrl.GetCurrentTarget())
					{
						for (int j = 2; j < 7; j++)
						{
							if (opponentRecord.record.isDamaged)
							{
								opponentRecord.hate.val[j] = (int)((float)opponentRecord.hate.val[j] * hateParam.categoryParam[j].atackedVolatizeRate);
								opponentRecord.record.isDamaged = false;
							}
							else
							{
								opponentRecord.hate.val[j] = (int)((float)opponentRecord.hate.val[j] * hateParam.categoryParam[j].volatilizeRate);
							}
						}
					}
					opponentRecord.hate.turnVal = 0;
					if (flag)
					{
						opponentRecord.hate.cycleLockCount = 0;
					}
				}
			}
		}
	}

	public OpponentRecord GetOpponentWithNotTargetInHateCycle()
	{
		OpponentRecord opponent = null;
		opponentRecords.ForEach(delegate(OpponentRecord o)
		{
			if (o.IsAlive() && o.hate.cycleLockCount <= 0)
			{
				opponent = o;
			}
		});
		return opponent;
	}

	public OpponentRecord GetOpponentWithHigherHate()
	{
		OpponentRecord higher = null;
		opponentRecords.ForEach(delegate(OpponentRecord o)
		{
			if (o.IsAlive() && (higher == null || o.hate.CalcTotalHate(hateParam) > higher.hate.CalcTotalHate(hateParam)))
			{
				higher = o;
			}
		});
		return higher;
	}

	public OpponentRecord[] GetOpponentWithRankingHate(bool isIncludeDead, int minimumNum = 4)
	{
		List<OpponentRecord> ranking = new List<OpponentRecord>();
		opponentRecords.ForEach(delegate(OpponentRecord o)
		{
			if (!(o.obj == null))
			{
				Character character = o.obj as Character;
				if (!(character == null) && (isIncludeDead || !character.isDead))
				{
					ranking.Add(o);
				}
			}
		});
		if (ranking.Count == 0)
		{
			return null;
		}
		ranking.Sort((OpponentRecord a, OpponentRecord b) => a.hate.CalcTotalHate(hateParam) - b.hate.CalcTotalHate(hateParam));
		int i = ranking.Count;
		int count = ranking.Count;
		for (; i < minimumNum; i++)
		{
			int index = (i - count) % count;
			ranking.Add(ranking[index]);
		}
		return ranking.ToArray();
	}

	public bool IsOpponentInterestLoseOfHate(StageObject obj)
	{
		if (!haveHateControl)
		{
			return true;
		}
		OpponentRecord opponentRecord = Find(obj);
		if (opponentRecord == null)
		{
			return true;
		}
		if (!opponentRecord.IsAlive())
		{
			return true;
		}
		if (!opponentRecord.record.isView && opponentRecord.hate.CalcTotalHate(hateParam) <= 0)
		{
			return true;
		}
		if (IsHateCycleLastTurn())
		{
			return true;
		}
		if (hateParam.missTargetFromContinuousLockNum > 0 && opponentRecord.hate.continuousLockCount >= hateParam.missTargetFromContinuousLockNum)
		{
			return true;
		}
		int num = (int)((float)brain.owner.hpMax * hateParam.missTargetUnderStockPerMaxHp);
		if (opponentRecord.hate.turnVal <= num)
		{
			return true;
		}
		if (obj.objectType != StageObject.OBJECT_TYPE.DECOY && IsExistDecoy())
		{
			return true;
		}
		return false;
	}

	private bool IsExistDecoy()
	{
		bool bExist = false;
		opponentRecords.ForEach(delegate(OpponentRecord o)
		{
			if (o.obj != null && o.obj.objectType == StageObject.OBJECT_TYPE.DECOY)
			{
				bExist = true;
			}
		});
		return bExist;
	}
}
