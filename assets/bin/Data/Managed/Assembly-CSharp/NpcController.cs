using System.Collections;
using UnityEngine;

public class NpcController : ControllerBase
{
	protected IEnumerator mainCoroutine;

	protected bool isStart;

	protected float startWaitTime;

	private Player player
	{
		get;
		set;
	}

	public NpcBrain npcBrain
	{
		get;
		private set;
	}

	public InGameSettingsManager.NpcController parameter
	{
		get;
		private set;
	}

	public InGameSettingsManager.SelfController selfParameter
	{
		get;
		private set;
	}

	private bool isAttack => player != null && player.actionID == Character.ACTION_ID.ATTACK;

	private bool isGuard => player != null && player.actionID == (Character.ACTION_ID)18;

	private bool isMove => player != null && player.actionID == Character.ACTION_ID.MOVE;

	private bool isChangeableAttack => player != null && player.IsChangeableAction(Character.ACTION_ID.ATTACK);

	private bool isChangeableSpecialAction => player != null && player.IsChangeableAction((Character.ACTION_ID)31);

	private StageObject target => (!(base.brain != null)) ? null : base.brain.targetCtrl.GetCurrentTarget();

	protected override void Awake()
	{
		base.Awake();
		player = (character as Player);
		npcBrain = AttachBrain<NpcBrain>();
	}

	protected override void Start()
	{
		base.Start();
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.npcController;
		selfParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController;
		isStart = true;
		if (IsEnableControll())
		{
			OnChangeEnableControll(true);
		}
	}

	protected override void Update()
	{
		base.Update();
		if (IsEnableControll())
		{
			OnDead();
		}
	}

	public override void OnChangeEnableControll(bool enable)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (enable && !CoopStageObjectUtility.CanControll(player))
		{
			Log.Error(LOG.INGAME, "NpcController:OnChangeEnableControll. field block enable. obj={0}", player);
			enable = false;
		}
		base.OnChangeEnableControll(enable);
		if (enable)
		{
			if (isStart && this.get_enabled() && player != null && mainCoroutine == null)
			{
				startWaitTime = parameter.startWaitTime;
				mainCoroutine = AIMain();
				this.StartCoroutine(mainCoroutine);
			}
		}
		else
		{
			if (mainCoroutine != null)
			{
				this.StopAllCoroutines();
				mainCoroutine = null;
			}
			if (isGuard)
			{
				player.ActIdle(false, -1f);
			}
		}
	}

	public override void OnActReaction()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base.OnActReaction();
		if (IsEnableControll() && mainCoroutine != null)
		{
			this.StopAllCoroutines();
			startWaitTime = parameter.afterReactionWaitTime;
			mainCoroutine = AIMain();
			this.StartCoroutine(mainCoroutine);
		}
	}

	private IEnumerator AIMain()
	{
		while (base.brain == null || !base.brain.isInitialized)
		{
			yield return (object)0;
		}
		while (!player.isControllable)
		{
			yield return (object)0;
		}
		if (startWaitTime > 0f)
		{
			yield return (object)new WaitForSeconds(startWaitTime);
		}
		while (this.get_enabled())
		{
			while (player.IsMirror())
			{
				yield return (object)new WaitForSeconds(1f);
			}
			OnMove();
			OnWeapon();
			float time = 0f;
			if (player.packetSender != null)
			{
				time = player.packetSender.GetWaitTime(0f);
			}
			if (time > 0f)
			{
				yield return (object)new WaitForSeconds(time);
			}
			else
			{
				yield return (object)0;
			}
		}
		mainCoroutine = null;
	}

	private void OnDead()
	{
		if (!player.isControllable && player.actionID == (Character.ACTION_ID)22 && !player.IsAutoReviving() && player.rescueTime <= 0f && player.deadStartTime >= 0f && !player.isProgressStop() && !(player is Self))
		{
			player.DestroyObject();
		}
	}

	private void OnMove()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		if (base.brain.moveCtrl.IsAvoid())
		{
			if (!player.IsChangeableAction(Character.ACTION_ID.MAX))
			{
				return;
			}
			OnAvoid(base.brain.moveCtrl.avoidPlace);
			flag = true;
		}
		else if (base.brain.moveCtrl.IsSeek())
		{
			if (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
			{
				return;
			}
			OnMoveStick(base.brain.moveCtrl.stickVec, base.brain.moveCtrl.targetPos);
			flag = true;
		}
		else if (base.brain.moveCtrl.IsStop())
		{
			flag = false;
		}
		if (!flag && isMove)
		{
			character.ActIdle(false, -1f);
		}
	}

	private void OnMoveStick(Vector2 stick_vec, Vector3 target_pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = this.get_transform().get_position();
		Vector3 val = target_pos - position;
		val.y = 0f;
		val.Normalize();
		Vector3 val2 = Quaternion.Euler(0f, 90f, 0f) * val;
		Vector3 val3 = val;
		Vector3 val4 = val2 * stick_vec.x * selfParameter.moveSideSpeed + val3 * stick_vec.y * selfParameter.moveForwardSpeed;
		character.ActMoveVelocity(val4, selfParameter.moveForwardSpeed, Character.MOTION_ID.WALK);
		character.SetLerpRotation(val4);
	}

	private void OnAvoid(PLACE avoid_place)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (!(target == null))
		{
			Vector3 val = target._transform.get_position() - character._transform.get_position();
			val.y = 0f;
			Quaternion val2 = Quaternion.LookRotation(val);
			Vector3 val3 = Vector3.get_zero();
			switch (avoid_place)
			{
			case PLACE.FRONT:
				val3 = val2 * Vector3.get_forward();
				break;
			case PLACE.BACK:
				val3 = val2 * Vector3.get_back();
				break;
			case PLACE.LEFT:
				val3 = val2 * Vector3.get_left();
				break;
			case PLACE.RIGHT:
				val3 = val2 * Vector3.get_right();
				break;
			}
			character.LookAt(character._transform.get_position() + val3);
			player.ActAvoid();
		}
	}

	private void OnWeapon()
	{
		if (base.brain.weaponCtrl.IsAttack())
		{
			if (player.attackMode == Player.ATTACK_MODE.ARROW)
			{
				OnArrowAttack();
			}
			else
			{
				OnAttack();
			}
		}
		else if (base.brain.weaponCtrl.IsSpecial())
		{
			if (player.attackMode == Player.ATTACK_MODE.ARROW)
			{
				OnArrowAttack();
			}
			else
			{
				OnSpecialAttack();
			}
		}
		else if (base.brain.weaponCtrl.IsGuard())
		{
			OnGuard();
		}
		else
		{
			if (player.actionID == (Character.ACTION_ID)18)
			{
				player.ActIdle(true, -1f);
			}
			if (player.enableInputCharge)
			{
				player.SetEnableTap(false);
			}
		}
		if (base.brain.weaponCtrl.changeIndex >= 0)
		{
			OnChangeWeapon();
		}
	}

	private void OnAttack()
	{
		if (player.enableTap)
		{
			player.SetEnableTap(false);
		}
		if (isAttack && player.enableInputCombo)
		{
			if (base.brain.weaponCtrl.beforeAttackId == 0 || base.brain.weaponCtrl.beforeAttackId != player.attackID)
			{
				base.brain.weaponCtrl.ComboOn();
				base.brain.weaponCtrl.SetBeforeAttackId(player.attackID);
				player.InputAttackCombo();
			}
		}
		else if (isChangeableAttack)
		{
			character.ActAttack(0, true, false);
			base.brain.weaponCtrl.ComboOff();
			base.brain.weaponCtrl.SetBeforeAttackId(0);
		}
	}

	private void OnSpecialAttack()
	{
		if (!player.isActSpecialAction && isChangeableSpecialAction)
		{
			player.SetEnableTap(true);
			player.ActSpecialAction(true, true);
			base.brain.weaponCtrl.SetChargeRate(1f);
		}
		else if (player.enableInputCharge && player.GetChargingRate() >= base.brain.weaponCtrl.chargeRate)
		{
			player.SetEnableTap(false);
		}
	}

	private void OnArrowAttack()
	{
		if (player.isControllable)
		{
			player.SetEnableTap(true);
			player.ActAttack(0, true, false);
			if (base.brain.weaponCtrl.IsSpecial())
			{
				base.brain.weaponCtrl.SetChargeRate(1f);
			}
			else
			{
				base.brain.weaponCtrl.SetChargeRate(Random.get_value());
			}
		}
		else if (player.enableInputCharge && player.GetChargingRate() >= base.brain.weaponCtrl.chargeRate)
		{
			player.SetEnableTap(false);
		}
	}

	private void OnGuard()
	{
		if (player.isGuardAttackMode && !player.isActSpecialAction && isChangeableSpecialAction)
		{
			player.ActSpecialAction(true, true);
		}
	}

	private void OnChangeWeapon()
	{
		int changeIndex = base.brain.weaponCtrl.changeIndex;
		if (changeIndex >= 0 && player.equipWeaponList.Count > changeIndex && changeIndex != player.weaponIndex && player.equipWeaponList[changeIndex] != null)
		{
			player.ActChangeWeapon(player.equipWeaponList[changeIndex], changeIndex);
			base.brain.weaponCtrl.ResetChangeIndex();
		}
	}

	private void LateUpdate()
	{
		UpdateRegionTarget();
	}

	private void UpdateRegionTarget()
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		if (!(player == null))
		{
			player.targetingPointList.Clear();
			if (!(player.actionTarget == null))
			{
				Enemy enemy = player.actionTarget as Enemy;
				if (!(enemy == null) && !enemy.isDead && enemy.enableTargetPoint)
				{
					TargetPoint[] targetPoints = enemy.targetPoints;
					if (targetPoints != null && targetPoints.Length != 0)
					{
						TargetPoint targetPoint = null;
						float num = 3.40282347E+38f;
						Vector3 position = player._transform.get_position();
						Vector2 val = position.ToVector2XZ();
						Vector2 forwardXZ = player.forwardXZ;
						forwardXZ.Normalize();
						int i = 0;
						for (int num2 = targetPoints.Length; i < num2; i++)
						{
							TargetPoint targetPoint2 = targetPoints[i];
							if (targetPoint2.get_gameObject().get_activeInHierarchy())
							{
								Vector3 targetPoint3 = targetPoint2.GetTargetPoint();
								Vector2 val2 = targetPoint3.ToVector2XZ();
								Vector2 val3 = val2 - val;
								float sqrMagnitude = val3.get_sqrMagnitude();
								if (targetPoint == null || sqrMagnitude < num)
								{
									targetPoint = targetPoint2;
									num = sqrMagnitude;
								}
							}
						}
						if (targetPoint != null)
						{
							player.targetingPointList.Add(targetPoint);
						}
					}
				}
			}
		}
	}
}
