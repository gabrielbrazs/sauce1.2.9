using System;
using UnityEngine;

public class CoopStagePacketSender
{
	private CoopStage coopStage
	{
		get;
		set;
	}

	public CoopStagePacketSender()
		: this()
	{
	}

	protected virtual void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		coopStage = this.get_gameObject().GetComponent<CoopStage>();
	}

	protected virtual void Start()
	{
	}

	private int Send<T>(T model, bool promise = true, int to_client_id = 0, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null) where T : Coop_Model_Base
	{
		if (to_client_id == 0)
		{
			return MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcastInStage(model, promise, onReceiveAck, onPreResend);
		}
		return MonoBehaviourSingleton<CoopNetworkManager>.I.SendToInStage(to_client_id, model, promise, onReceiveAck, onPreResend);
	}

	public void SendStageRequest(int to_client_id = 0)
	{
		Coop_Model_StageRequest coop_Model_StageRequest = new Coop_Model_StageRequest();
		coop_Model_StageRequest.id = 1002;
		coop_Model_StageRequest.series_index = 0;
		if (QuestManager.IsValidInGame())
		{
			coop_Model_StageRequest.series_index = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
		}
		Send(coop_Model_StageRequest, true, to_client_id, null, null);
	}

	public void SendStagePlayerPop(Player player, int to_client_id = 0)
	{
		if (!(player == null))
		{
			if (player.IsCoopNone())
			{
				player.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
			}
			Coop_Model_StagePlayerPop coop_Model_StagePlayerPop = new Coop_Model_StagePlayerPop();
			coop_Model_StagePlayerPop.id = 1002;
			coop_Model_StagePlayerPop.sid = player.id;
			coop_Model_StagePlayerPop.isSelf = (player is Self);
			NonPlayer nonPlayer = player as NonPlayer;
			if (player.createInfo != null)
			{
				if (nonPlayer == null)
				{
					coop_Model_StagePlayerPop.charaInfo = player.createInfo.charaInfo;
				}
				coop_Model_StagePlayerPop.extentionInfo = player.createInfo.extentionInfo;
			}
			coop_Model_StagePlayerPop.transferInfo = player.CreateTransferInfo();
			Send(coop_Model_StagePlayerPop, true, to_client_id, null, null);
		}
	}

	public void SendStageInfo(int to_client_id = 0)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_StageInfo model = new Coop_Model_StageInfo();
		model.id = 1002;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			model.elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle())
		{
			model.isInFieldEnemyBossBattle = true;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.gimmickList.ForEach(delegate(StageObject o)
			{
				//IL_0027: Unknown result type (might be due to invalid IL or missing references)
				if (o.IsCoopNone())
				{
					o.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
				}
				Coop_Model_StageInfo.GimmickInfo item = new Coop_Model_StageInfo.GimmickInfo
				{
					id = o.id,
					enable = o.get_gameObject().get_activeSelf()
				};
				model.gimmicks.Add(item);
			});
			model.enemyPos = Vector3.get_zero();
			if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
			{
				model.enemyPos = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.get_position();
			}
		}
		if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle())
		{
			model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
		}
		Send(model, true, to_client_id, null, delegate(Coop_Model_Base send_model)
		{
			Coop_Model_StageInfo coop_Model_StageInfo = send_model as Coop_Model_StageInfo;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				coop_Model_StageInfo.elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
			}
			return true;
		});
	}

	public void SendStageResponseEnd(CoopStage.STAGE_REQUEST_ERROR error_id = CoopStage.STAGE_REQUEST_ERROR.NONE, int to_client_id = 0)
	{
		Coop_Model_StageResponseEnd coop_Model_StageResponseEnd = new Coop_Model_StageResponseEnd();
		coop_Model_StageResponseEnd.id = 1002;
		coop_Model_StageResponseEnd.error_id = (int)error_id;
		Send(coop_Model_StageResponseEnd, true, to_client_id, null, null);
	}

	public void SendStageQuestClose(bool is_succeed)
	{
		Coop_Model_StageQuestClose coop_Model_StageQuestClose = new Coop_Model_StageQuestClose();
		coop_Model_StageQuestClose.id = 1002;
		coop_Model_StageQuestClose.is_succeed = is_succeed;
		Send(coop_Model_StageQuestClose, true, 0, null, null);
	}

	public void SendStageTimeup()
	{
		Coop_Model_StageTimeup coop_Model_StageTimeup = new Coop_Model_StageTimeup();
		coop_Model_StageTimeup.id = 1002;
		Send(coop_Model_StageTimeup, true, 0, null, null);
	}

	public void SendStageChat(int chara_id, int chat_id)
	{
		Coop_Model_StageChat coop_Model_StageChat = new Coop_Model_StageChat();
		coop_Model_StageChat.id = 1002;
		coop_Model_StageChat.chara_id = chara_id;
		coop_Model_StageChat.chat_id = chat_id;
		Send(coop_Model_StageChat, false, 0, null, null);
	}

	public void SendChatMessage(int chara_id, string message)
	{
		Coop_Model_StageChatMessage coop_Model_StageChatMessage = new Coop_Model_StageChatMessage();
		coop_Model_StageChatMessage.id = 1002;
		coop_Model_StageChatMessage.chara_id = chara_id;
		coop_Model_StageChatMessage.text = message;
		coop_Model_StageChatMessage.user_id = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			coop_Model_StageChatMessage.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		}
		Send(coop_Model_StageChatMessage, false, 0, null, null);
	}

	public void SendChatStamp(int chara_id, int stamp_id)
	{
		Coop_Model_StageChatStamp coop_Model_StageChatStamp = new Coop_Model_StageChatStamp();
		coop_Model_StageChatStamp.id = 1002;
		coop_Model_StageChatStamp.chara_id = chara_id;
		coop_Model_StageChatStamp.stamp_id = stamp_id;
		coop_Model_StageChatStamp.user_id = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			coop_Model_StageChatStamp.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		}
		Send(coop_Model_StageChatStamp, false, 0, null, null);
	}

	public void SendRequestPop(int to_client_id, bool is_player, bool is_self, bool promise = false)
	{
		Coop_Model_StageRequestPop coop_Model_StageRequestPop = new Coop_Model_StageRequestPop();
		coop_Model_StageRequestPop.id = 1002;
		coop_Model_StageRequestPop.isPlayer = is_player;
		coop_Model_StageRequestPop.isSelf = is_self;
		Send(coop_Model_StageRequestPop, promise, to_client_id, null, null);
	}

	public void SendSyncPlayerRecord(InGameRecorder.PlayerRecordSyncHost record, int to_client_id, bool promise, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		Coop_Model_StageSyncPlayerRecord coop_Model_StageSyncPlayerRecord = new Coop_Model_StageSyncPlayerRecord();
		coop_Model_StageSyncPlayerRecord.id = 1002;
		coop_Model_StageSyncPlayerRecord.rec = record;
		Send(coop_Model_StageSyncPlayerRecord, promise, to_client_id, null, onPreResend);
	}

	public void SendEnemyBossEscape(int sid, bool promise)
	{
		Coop_Model_EnemyBossEscape coop_Model_EnemyBossEscape = new Coop_Model_EnemyBossEscape();
		coop_Model_EnemyBossEscape.sid = sid;
		coop_Model_EnemyBossEscape.id = 1002;
		Send(coop_Model_EnemyBossEscape, promise, 0, null, null);
	}

	public void SendEnemyBossAliveRequest()
	{
		Coop_Model_EnemyBossAliveRequest coop_Model_EnemyBossAliveRequest = new Coop_Model_EnemyBossAliveRequest();
		coop_Model_EnemyBossAliveRequest.id = 1002;
		Send(coop_Model_EnemyBossAliveRequest, false, 0, null, null);
	}

	public void SendEnemyBossAliveRequested(int toClientId)
	{
		Coop_Model_EnemyBossAliveRequested coop_Model_EnemyBossAliveRequested = new Coop_Model_EnemyBossAliveRequested();
		coop_Model_EnemyBossAliveRequested.id = 1002;
		Send(coop_Model_EnemyBossAliveRequested, false, toClientId, null, null);
	}
}
