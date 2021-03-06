using System.Collections.Generic;
using UnityEngine;

public class ObjectPacketReceiver : PacketReceiver
{
	public enum FILTER_MODE
	{
		NONE,
		WAIT_INITIALIZE
	}

	private static List<bool> forceFlags;

	public StageObject owner
	{
		get;
		protected set;
	}

	public FILTER_MODE filterMode
	{
		get;
		protected set;
	}

	public static ObjectPacketReceiver SetupComponent(StageObject set_object)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (set_object is Enemy)
		{
			return set_object.get_gameObject().AddComponent<EnemyPacketReceiver>();
		}
		if (set_object is Player)
		{
			return set_object.get_gameObject().AddComponent<PlayerPacketReceiver>();
		}
		if (set_object is Character)
		{
			return set_object.get_gameObject().AddComponent<CharacterPacketReceiver>();
		}
		return set_object.get_gameObject().AddComponent<ObjectPacketReceiver>();
	}

	protected virtual void Awake()
	{
		owner = this.GetComponent<StageObject>();
	}

	public override void SetStopPacketUpdate(bool is_stop)
	{
		base.SetStopPacketUpdate(is_stop);
	}

	public override void Set(CoopPacket packet)
	{
		base.Set(packet);
		packet.GetModel<Coop_Model_ObjectBase>()?.SetReceiveTime(Time.get_time());
	}

	protected override void PacketUpdate()
	{
		if (!base.stopPacketUpdate)
		{
			if (forceFlags == null)
			{
				forceFlags = new List<bool>(base.packets.Count);
			}
			else
			{
				forceFlags.Clear();
				if (forceFlags.Capacity < base.packets.Count)
				{
					forceFlags.Capacity = base.packets.Count;
				}
			}
			int i = 0;
			for (int count = base.packets.Count; i < count; i++)
			{
				CoopPacket coopPacket = base.packets[i];
				bool item = false;
				Coop_Model_ObjectBase model = coopPacket.GetModel<Coop_Model_ObjectBase>();
				if (model != null)
				{
					item = model.IsForceHandleBefore(owner);
				}
				forceFlags.Add(item);
			}
			int j = 0;
			for (int count2 = base.packets.Count; j < count2; j++)
			{
				CoopPacket coopPacket2 = base.packets[j];
				if (!CheckFilterPacket(coopPacket2))
				{
					AddDeleteQueue(coopPacket2);
				}
				else
				{
					bool flag = true;
					Coop_Model_ObjectBase model2 = coopPacket2.GetModel<Coop_Model_ObjectBase>();
					if (model2 != null)
					{
						bool flag2 = false;
						for (int k = j + 1; k < count2; k++)
						{
							if (forceFlags[k])
							{
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							float num = 0f;
							if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
							{
								num = MonoBehaviourSingleton<InGameSettingsManager>.I.stageObject.packetHandleMarginTime;
							}
							if (Time.get_time() > model2.GetReceiveTime() + num)
							{
								flag = true;
								if (!model2.IsHandleable(owner))
								{
									int num2 = -1;
									Character character = owner as Character;
									if (character != null)
									{
										num2 = (int)character.actionID;
									}
									Log.Warning(LOG.COOP, "ObjectPacketReceiver::PacketUpdate() Err. ( Over packetHandleMarginTime. ) type : " + coopPacket2.packetType + ", action_id : " + num2);
								}
							}
							else
							{
								flag = model2.IsHandleable(owner);
							}
						}
					}
					if (!flag || !HandleCoopEvent(coopPacket2))
					{
						if (Time.get_time() > model2.GetReceiveTime() + 20f)
						{
							Log.Warning(LOG.COOP, "ObjectPacketReceiver::PacketUpdate() Err. ( Over 20 Second. ) type : " + coopPacket2.packetType);
						}
						break;
					}
					AddDeleteQueue(coopPacket2);
					if (base.stopPacketUpdate)
					{
						break;
					}
				}
			}
			EraseUsedPacket();
		}
	}

	public virtual void SetFilterMode(FILTER_MODE filter_mode)
	{
		filterMode = filter_mode;
		if (filterMode != 0)
		{
			int i = 0;
			for (int count = base.packets.Count; i < count; i++)
			{
				CoopPacket packet = base.packets[i];
				if (!CheckFilterPacket(packet))
				{
					AddDeleteQueue(packet);
				}
			}
			EraseUsedPacket();
		}
	}

	protected virtual bool CheckFilterPacket(CoopPacket packet)
	{
		if (filterMode == FILTER_MODE.NONE)
		{
			return true;
		}
		if (packet.packetType == PACKET_TYPE.OBJECT_DESTROY)
		{
			return true;
		}
		return false;
	}

	public virtual bool GetPredictivePosition(out Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		pos = Vector3.get_zero();
		for (int num = base.packets.Count - 1; num >= 0; num--)
		{
			CoopPacket coopPacket = base.packets[num];
			Coop_Model_ObjectBase model = coopPacket.GetModel<Coop_Model_ObjectBase>();
			if (model != null && model.IsHaveObjectPosition())
			{
				pos = model.GetObjectPosition();
				return true;
			}
		}
		return false;
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		switch (packet.packetType)
		{
		case PACKET_TYPE.OBJECT_DESTROY:
			if (owner is Self)
			{
				return true;
			}
			return owner.DestroyObject();
		case PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER:
		{
			Coop_Model_ObjectAttackedHitOwner model5 = packet.GetModel<Coop_Model_ObjectAttackedHitOwner>();
			model5.CopyAttackedHitStatus(out AttackedHitStatusOwner status2);
			if (owner.IsEnableAttackedHitOwner())
			{
				owner.OnAttackedHitOwner(status2);
				AttackedHitStatusFix status3 = new AttackedHitStatusFix(status2.origin);
				owner.OnAttackedHitFix(status3);
				if (owner.packetSender != null)
				{
					owner.packetSender.OnAttackedHitFix(status3);
				}
			}
			break;
		}
		case PACKET_TYPE.OBJECT_ATTACKED_HIT_FIX:
		{
			Coop_Model_ObjectAttackedHitFix model4 = packet.GetModel<Coop_Model_ObjectAttackedHitFix>();
			model4.CopyAttackedHitStatus(out AttackedHitStatusFix status);
			owner.OnAttackedHitFix(status);
			break;
		}
		case PACKET_TYPE.OBJECT_KEEP_WAITING_PACKET:
		{
			Coop_Model_ObjectKeepWaitingPacket model3 = packet.GetModel<Coop_Model_ObjectKeepWaitingPacket>();
			owner.KeepWaitingPacket((StageObject.WAITING_PACKET)model3.type);
			break;
		}
		case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_SET:
		{
			Coop_Model_ObjectBulletObservableSet model2 = packet.GetModel<Coop_Model_ObjectBulletObservableSet>();
			owner.RegisterObservableID(model2.observedID);
			break;
		}
		case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_BROKEN:
		{
			Coop_Model_ObjectBulletObservableBroken model = packet.GetModel<Coop_Model_ObjectBulletObservableBroken>();
			owner.OnBreak(model.observedID);
			break;
		}
		default:
			Log.Warning(LOG.COOP, "not valid packet");
			return true;
		}
		return true;
	}
}
