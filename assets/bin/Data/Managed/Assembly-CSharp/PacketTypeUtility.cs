using System;

public class PacketTypeUtility
{
	public static Type GetModelType(PACKET_TYPE type)
	{
		switch (type)
		{
		case PACKET_TYPE.REGISTER:
			return typeof(Coop_Model_Register);
		case PACKET_TYPE.REGISTER_ACK:
			return typeof(Coop_Model_RegisterACK);
		case PACKET_TYPE.ACK:
			return typeof(Coop_Model_ACK);
		case PACKET_TYPE.DISCONNECT:
			return typeof(Coop_Model_Disconnect);
		case PACKET_TYPE.ALIVE:
			return typeof(Coop_Model_Alive);
		case PACKET_TYPE.ROOM_ENTRY_CLOSE:
			return typeof(Coop_Model_RoomEntryClose);
		case PACKET_TYPE.ROOM_JOINED:
			return typeof(Coop_Model_RoomJoined);
		case PACKET_TYPE.ROOM_LEAVED:
			return typeof(Coop_Model_RoomLeaved);
		case PACKET_TYPE.ROOM_STAGE_CHANGE:
			return typeof(Coop_Model_RoomStageChange);
		case PACKET_TYPE.ROOM_STAGE_CHANGED:
			return typeof(Coop_Model_RoomStageChanged);
		case PACKET_TYPE.ROOM_STAGE_REQUEST:
			return typeof(Coop_Model_RoomStageRequest);
		case PACKET_TYPE.ROOM_STAGE_REQUESTED:
			return typeof(Coop_Model_RoomStageRequested);
		case PACKET_TYPE.ROOM_STAGE_HOST_CHANGED:
			return typeof(Coop_Model_RoomStageHostChanged);
		case PACKET_TYPE.BATTLE_START:
			return typeof(Coop_Model_BattleStart);
		case PACKET_TYPE.ENEMY_POP:
			return typeof(Coop_Model_EnemyPop);
		case PACKET_TYPE.ENEMY_ATTACK:
			return typeof(Coop_Model_EnemyAttack);
		case PACKET_TYPE.ENEMY_OUT:
			return typeof(Coop_Model_EnemyOut);
		case PACKET_TYPE.ENEMY_DEFEAT:
			return typeof(Coop_Model_EnemyDefeat);
		case PACKET_TYPE.REWARD_GET:
			return typeof(Coop_Model_RewardGet);
		case PACKET_TYPE.REWARD_PICKUP:
			return typeof(Coop_Model_RewardPickup);
		case PACKET_TYPE.ENEMY_EXTERMINATION:
			return typeof(Coop_Model_EnemyExtermination);
		case PACKET_TYPE.UPDATE_BOOST:
			return typeof(Coop_Model_UpdateBoost);
		case PACKET_TYPE.UPDATE_BOOST_COMPLETE:
			return typeof(Coop_Model_UpdateBoostComplete);
		case PACKET_TYPE.ROOM_TIME_CHECK:
			return typeof(Coop_Model_RoomTimeCheck);
		case PACKET_TYPE.ROOM_TIME_UPDATE:
			return typeof(Coop_Model_RoomTimeUpdate);
		case PACKET_TYPE.EVENT_HAPPEN_QUEST:
			return typeof(Coop_Model_EventHappenQuest);
		case PACKET_TYPE.CLIENT_STATUS:
			return typeof(Coop_Model_ClientStatus);
		case PACKET_TYPE.CLIENT_BECAME_HOST:
			return typeof(Coop_Model_ClientBecameHost);
		case PACKET_TYPE.CLIENT_LOADING_PROGRESS:
			return typeof(Coop_Model_ClientLoadingProgress);
		case PACKET_TYPE.CLIENT_CHANGE_EQUIP:
			return typeof(Coop_Model_ClientChangeEquip);
		case PACKET_TYPE.CLIENT_BATTLE_RETIRE:
			return typeof(Coop_Model_ClientBattleRetire);
		case PACKET_TYPE.CLIENT_SERIES_PROGRESS:
			return typeof(Coop_Model_ClientSeriesProgress);
		case PACKET_TYPE.ROOM_UPDATE_PORTAL_POINT:
			return typeof(Coop_Model_RoomUpdatePortalPoint);
		case PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS:
			return typeof(Coop_Model_RoomSyncExploreBoss);
		case PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS_MAP:
			return typeof(Coop_Model_RoomSyncExploreBossMap);
		case PACKET_TYPE.ROOM_EXPLORE_BOSS_DEAD:
			return typeof(Coop_Model_RoomExploreBossDead);
		case PACKET_TYPE.ROOM_SYNC_DEFENSE_BATTLE:
			return typeof(Coop_Model_RoomSyncDefenseBattle);
		case PACKET_TYPE.ROOM_NOTIFY_ENCOUNTER_BOSS:
			return typeof(Coop_Model_RoomNotifyEncounterBoss);
		case PACKET_TYPE.ROOM_SYNC_PLAYER_STATUS:
			return typeof(Coop_Model_RoomSyncPlayerStatus);
		case PACKET_TYPE.ROOM_CHAT_STAMP:
			return typeof(Coop_Model_RoomChatStamp);
		case PACKET_TYPE.ROOM_EXPLORE_BOSS_DAMAGE:
			return typeof(Coop_Model_RoomExploreBossDamage);
		case PACKET_TYPE.ROOM_EXPLORE_ALIVE:
			return typeof(Coop_Model_RoomExploreAlive);
		case PACKET_TYPE.ROOM_EXPLORE_ALIVE_REQUEST:
			return typeof(Coop_Model_RoomExploreAliveRequest);
		case PACKET_TYPE.ROOM_SYNC_ALL_PORTAL_POINT:
			return typeof(Coop_Model_RoomSyncAllPortalPoint);
		case PACKET_TYPE.ROOM_MOVE_FIELD:
			return typeof(Coop_Model_RoomMoveField);
		case PACKET_TYPE.ROOM_RUSH_REQUEST:
			return typeof(Coop_Model_RushRequest);
		case PACKET_TYPE.ROOM_RUSH_REQUESTED:
			return typeof(Coop_Model_RushRequested);
		case PACKET_TYPE.ROOM_NOTIFY_TRACE_BOSS:
			return typeof(Coop_Model_RoomNotifyTraceBoss);
		case PACKET_TYPE.STAGE_REQUEST:
			return typeof(Coop_Model_StageRequest);
		case PACKET_TYPE.STAGE_PLAYER_POP:
			return typeof(Coop_Model_StagePlayerPop);
		case PACKET_TYPE.STAGE_INFO:
			return typeof(Coop_Model_StageInfo);
		case PACKET_TYPE.STAGE_RESPONSE_END:
			return typeof(Coop_Model_StageResponseEnd);
		case PACKET_TYPE.STAGE_QUEST_CLOSE:
			return typeof(Coop_Model_StageQuestClose);
		case PACKET_TYPE.STAGE_TIMEUP:
			return typeof(Coop_Model_StageTimeup);
		case PACKET_TYPE.STAGE_CHAT:
			return typeof(Coop_Model_StageChat);
		case PACKET_TYPE.OBJECT_DESTROY:
			return typeof(Coop_Model_ObjectDestroy);
		case PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER:
			return typeof(Coop_Model_ObjectAttackedHitOwner);
		case PACKET_TYPE.OBJECT_ATTACKED_HIT_FIX:
			return typeof(Coop_Model_ObjectAttackedHitFix);
		case PACKET_TYPE.OBJECT_KEEP_WAITING_PACKET:
			return typeof(Coop_Model_ObjectKeepWaitingPacket);
		case PACKET_TYPE.CHARACTER_ACTION_TARGET:
			return typeof(Coop_Model_CharacterActionTarget);
		case PACKET_TYPE.CHARACTER_UPDATE_ACTION_POSITION:
			return typeof(Coop_Model_CharacterUpdateActionPosition);
		case PACKET_TYPE.CHARACTER_UPDATE_DIRECTION:
			return typeof(Coop_Model_CharacterUpdateDirection);
		case PACKET_TYPE.CHARACTER_PERIODIC_SYNC_ACTION_POSITION:
			return typeof(Coop_Model_CharacterPeriodicSyncActionPosition);
		case PACKET_TYPE.CHARACTER_IDLE:
			return typeof(Coop_Model_CharacterIdle);
		case PACKET_TYPE.CHARACTER_MOVE_VELOCITY:
			return typeof(Coop_Model_CharacterMoveVelocity);
		case PACKET_TYPE.CHARACTER_MOVE_VELOCITY_END:
			return typeof(Coop_Model_CharacterMoveVelocityEnd);
		case PACKET_TYPE.CHARACTER_MOVE_TO_POSITION:
			return typeof(Coop_Model_CharacterMoveToPosition);
		case PACKET_TYPE.CHARACTER_MOVE_HOMING:
			return typeof(Coop_Model_CharacterMoveHoming);
		case PACKET_TYPE.CHARACTER_ROTATE:
			return typeof(Coop_Model_CharacterRotate);
		case PACKET_TYPE.CHARACTER_ROTATE_MOTION:
			return typeof(Coop_Model_CharacterRotateMotion);
		case PACKET_TYPE.CHARACTER_ATTACK:
			return typeof(Coop_Model_CharacterAttack);
		case PACKET_TYPE.CHARACTER_CONTINUS_ATTACK_SYNC:
			return typeof(Coop_Model_CharacterContinusAttackSync);
		case PACKET_TYPE.CHARACTER_BUFFSYNC:
			return typeof(Coop_Model_CharacterBuffSync);
		case PACKET_TYPE.CHARACTER_BUFFRECEIVE:
			return typeof(Coop_Model_CharacterBuffReceive);
		case PACKET_TYPE.CHARACTER_BUFFROUTINE:
			return typeof(Coop_Model_CharacterBuffRoutine);
		case PACKET_TYPE.CHARACTER_REACTION:
			return typeof(Coop_Model_CharacterReaction);
		case PACKET_TYPE.CHARACTER_REACTION_DELAY:
			return typeof(Coop_Model_CharacterReactionDelay);
		case PACKET_TYPE.PLAYER_LOAD_COMPLETE:
			return typeof(Coop_Model_PlayerLoadComplete);
		case PACKET_TYPE.PLAYER_INITIALIZE:
			return typeof(Coop_Model_PlayerInitialize);
		case PACKET_TYPE.PLAYER_ATTACK_COMBO:
			return typeof(Coop_Model_PlayerAttackCombo);
		case PACKET_TYPE.PLAYER_CHARGE_RELEASE:
			return typeof(Coop_Model_PlayerChargeRelease);
		case PACKET_TYPE.PLAYER_RESTRAINT:
			return typeof(Coop_Model_PlayerRestraint);
		case PACKET_TYPE.PLAYER_RESTRAINT_END:
			return typeof(Coop_Model_PlayerRestraintEnd);
		case PACKET_TYPE.PLAYER_AVOID:
			return typeof(Coop_Model_PlayerAvoid);
		case PACKET_TYPE.PLAYER_WARP:
			return typeof(Coop_Model_PlayerWarp);
		case PACKET_TYPE.PLAYER_BLOW_CLEAR:
			return typeof(Coop_Model_PlayerBlowClear);
		case PACKET_TYPE.PLAYER_STUNNED_END:
			return typeof(Coop_Model_PlayerStunnedEnd);
		case PACKET_TYPE.PLAYER_DEAD_COUNT:
			return typeof(Coop_Model_PlayerDeadCount);
		case PACKET_TYPE.PLAYER_DEAD_STANDUP:
			return typeof(Coop_Model_PlayerDeadStandup);
		case PACKET_TYPE.PLAYER_GATHER:
			return typeof(Coop_Model_PlayerGather);
		case PACKET_TYPE.PLAYER_STOP_COUNTER:
			return typeof(Coop_Model_PlayerStopCounter);
		case PACKET_TYPE.PLAYER_SKILL_ACTION:
			return typeof(Coop_Model_PlayerSkillAction);
		case PACKET_TYPE.PLAYER_GET_HEAL:
			return typeof(Coop_Model_PlayerGetHeal);
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION:
			return typeof(Coop_Model_PlayerSpecialAction);
		case PACKET_TYPE.PLAYER_SHOT_ARROW:
			return typeof(Coop_Model_PlayerShotArrow);
		case PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW:
			return typeof(Coop_Model_PlayerShotSoulArrow);
		case PACKET_TYPE.PLAYER_UPDATE_SKILL_INFO:
			return typeof(Coop_Model_PlayerUpdateSkillInfo);
		case PACKET_TYPE.PLAYER_PRAYER_START:
			return typeof(Coop_Model_PlayerPrayerStart);
		case PACKET_TYPE.PLAYER_PRAYER_END:
			return typeof(Coop_Model_PlayerPrayerEnd);
		case PACKET_TYPE.PLAYER_PRAYER_BOOST:
			return typeof(Coop_Model_PlayerPrayerBoost);
		case PACKET_TYPE.PLAYER_CHANGE_WEAPON:
			return typeof(Coop_Model_PlayerChangeWeapon);
		case PACKET_TYPE.PLAYER_APPLY_CHANGE_WEAPON:
			return typeof(Coop_Model_PlayerApplyChangeWeapon);
		case PACKET_TYPE.PLAYER_SETSTATUS:
			return typeof(Coop_Model_PlayerSetStatus);
		case PACKET_TYPE.PLAYER_GET_RAREDROP:
			return typeof(Coop_Model_PlayerGetRareDrop);
		case PACKET_TYPE.ENEMY_LOAD_COMPLETE:
			return typeof(Coop_Model_EnemyLoadComplete);
		case PACKET_TYPE.ENEMY_INITIALIZE:
			return typeof(Coop_Model_EnemyInitialize);
		case PACKET_TYPE.ENEMY_STEP:
			return typeof(Coop_Model_EnemyStep);
		case PACKET_TYPE.ENEMY_ANGRY:
			return typeof(Coop_Model_EnemyAngry);
		case PACKET_TYPE.ENEMY_REVIVE_REGION:
			return typeof(Coop_Model_EnemyReviveRegion);
		case PACKET_TYPE.ENEMY_WARP:
			return typeof(Coop_Model_EnemyWarp);
		case PACKET_TYPE.ENEMY_TARGRTSHOT_EVENT:
			return typeof(Coop_Model_EnemyTargetShotEvent);
		case PACKET_TYPE.ENEMY_RANDOMSHOT_EVENT:
			return typeof(Coop_Model_EnemyRandomShotEvent);
		case PACKET_TYPE.ENEMY_UPDATE_BLEED_DAMAGE:
			return typeof(Coop_Model_EnemyUpdateBleedDamage);
		case PACKET_TYPE.ENEMY_UPDATE_SHADOWSEALING:
			return typeof(Coop_Model_EnemyUpdateShadowSealing);
		case PACKET_TYPE.ENEMY_SYNC_TARGET:
			return typeof(Coop_Model_EnemySyncTarget);
		case PACKET_TYPE.CHARACTER_DEAD:
			return typeof(Coop_Model_CharacterDead);
		case PACKET_TYPE.STAGE_REQUEST_POP:
			return typeof(Coop_Model_StageRequestPop);
		case PACKET_TYPE.CHAT_MESSAGE:
			return typeof(Coop_Model_StageChatMessage);
		case PACKET_TYPE.STAGE_CHAT_STAMP:
			return typeof(Coop_Model_StageChatStamp);
		case PACKET_TYPE.PARTY_REGISTER:
			return typeof(Party_Model_Register);
		case PACKET_TYPE.PARTY_REGISTER_ACK:
			return typeof(Party_Model_RegisterACK);
		case PACKET_TYPE.PARTY_ROOM_JOINED:
			return typeof(Party_Model_RoomJoined);
		case PACKET_TYPE.PARTY_ROOM_LEAVED:
			return typeof(Party_Model_RoomLeaved);
		case PACKET_TYPE.PLAYER_GRABBED:
			return typeof(Coop_Model_PlayerGrabbed);
		case PACKET_TYPE.PLAYER_GRABBED_END:
			return typeof(Coop_Model_PlayerGrabbedEnd);
		case PACKET_TYPE.ENEMY_RELEASE_GRABBED_PLAYER:
			return typeof(Coop_Model_EnemyReleasedGrabbedPlayer);
		case PACKET_TYPE.ENEMY_SHOT:
			return typeof(Coop_Model_EnemyShot);
		case PACKET_TYPE.CREATE_ICE_FLOOR:
			return typeof(Coop_Model_CreateIceFloor);
		case PACKET_TYPE.ACTION_MINE:
			return typeof(Coop_Model_ActionMine);
		case PACKET_TYPE.STAGE_SYNC_PLAYER_RECORD:
			return typeof(Coop_Model_StageSyncPlayerRecord);
		case PACKET_TYPE.ENEMY_RECOVER_HP:
			return typeof(Coop_Model_EnemyRecoverHp);
		case PACKET_TYPE.CHARACTER_MOVE_SIDEWAYS:
			return typeof(Coop_Model_CharacterMoveSideways);
		case PACKET_TYPE.ENEMY_TURN_UP:
			return typeof(Coop_Model_EnemyTurnUp);
		case PACKET_TYPE.PLAYER_SET_PRESENT_BULLET:
			return typeof(Coop_Model_PlayerSetPresentBullet);
		case PACKET_TYPE.PLAYER_PICK_PRESENT_BULLET:
			return typeof(Coop_Model_PlayerPickPresentBullet);
		case PACKET_TYPE.PLAYER_SHOT_ZONE_BULLET:
			return typeof(Coop_Model_PlayerShotZoneBullet);
		case PACKET_TYPE.PLAYER_SHOT_DECOY_BULLET:
			return typeof(Coop_Model_PlayerShotDecoyBullet);
		case PACKET_TYPE.PLAYER_EXPLODE_DECOY_BULLET:
			return typeof(Coop_Model_PlayerExplodeDecoyBullet);
		case PACKET_TYPE.CHARACTER_MOVE_POINT:
			return typeof(Coop_Model_CharacterMovePoint);
		case PACKET_TYPE.CHARACTER_MOVE_LOOKAT:
			return typeof(Coop_Model_CharacterMoveLookAt);
		case PACKET_TYPE.PLAYER_CANNON_STANDBY:
			return typeof(Coop_Model_PlayerCannonStandby);
		case PACKET_TYPE.PLAYER_CANNON_SHOT:
			return typeof(Coop_Model_PlayerCannonShot);
		case PACKET_TYPE.PLAYER_CANNON_ROTATE:
			return typeof(Coop_Model_PlayerCannonRotate);
		case PACKET_TYPE.LOUNGE_REGISTER:
			return typeof(Lounge_Model_Register);
		case PACKET_TYPE.LOUNGE_REGISTER_ACK:
			return typeof(Lounge_Model_RegisterACK);
		case PACKET_TYPE.LOUNGE_ROOM_JOINED:
			return typeof(Lounge_Model_RoomJoined);
		case PACKET_TYPE.LOUNGE_ROOM_LEAVED:
			return typeof(Lounge_Model_RoomLeaved);
		case PACKET_TYPE.LOUNGE_ROOM_ENTRY_CLOSE:
			return typeof(Lounge_Model_RoomEntryClose);
		case PACKET_TYPE.LOUNGE_ROOM_HOST_CHANGED:
			return typeof(Lounge_Model_RoomHostChanged);
		case PACKET_TYPE.LOUNGE_ROOM_KICK:
			return typeof(Lounge_Model_RoomKick);
		case PACKET_TYPE.LOUNGE_ROOM_AFK_KICK:
			return typeof(Lounge_Model_AFK_Kick);
		case PACKET_TYPE.LOUNGE_ROOM_MOVE:
			return typeof(Lounge_Model_RoomMove);
		case PACKET_TYPE.LOUNGE_ROOM_POSITION:
			return typeof(Lounge_Model_RoomPosition);
		case PACKET_TYPE.LOUNGE_ROOM_ACTION:
			return typeof(Lounge_Model_RoomAction);
		case PACKET_TYPE.LOUNGE_MEMBER_LOUNGE:
			return typeof(Lounge_Model_MemberLounge);
		case PACKET_TYPE.LOUNGE_MEMBER_FIELD:
			return typeof(Lounge_Model_MemberField);
		case PACKET_TYPE.LOUNGE_MEMBER_QUEST:
			return typeof(Lounge_Model_MemberQuest);
		case PACKET_TYPE.LOUNGE_MEMBER_ARENA:
			return typeof(Lounge_Model_MemberArena);
		case PACKET_TYPE.PLAYER_GET_CHARGE_SKILLGAUGE:
			return typeof(Coop_Model_PlayerGetChargeSkillGauge);
		case PACKET_TYPE.PLAYER_RESURRECT:
			return typeof(Coop_Model_PlayerResurrect);
		case PACKET_TYPE.PLAYER_GET_RESURRECT:
			return typeof(Coop_Model_PlayerGetResurrect);
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE:
			return typeof(Coop_Model_PlayerSpecialActionContinue);
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_GAUGE_SYNC:
			return typeof(Coop_Model_PlayerSpecialActionGaugeSync);
		case PACKET_TYPE.PLAYER_CHARGE_EXPAND_RELEASE:
			return typeof(Coop_Model_PlayerChargeExpandRelease);
		case PACKET_TYPE.PLAYER_JUMP_RIZE:
			return typeof(Coop_Model_PlayerJumpRize);
		case PACKET_TYPE.PLAYER_JUMP_END:
			return typeof(Coop_Model_PlayerJumpEnd);
		case PACKET_TYPE.PLAYER_SOUL_BOOST:
			return typeof(Coop_Model_PlayerSoulBoost);
		case PACKET_TYPE.PLAYER_EVOLVE_ACTION_SYNC:
			return typeof(Coop_Model_PlayerEvolveActionSync);
		case PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION:
			return typeof(Coop_Model_PlayerEvolveSpecialAction);
		case PACKET_TYPE.ENEMY_BOSS_POP:
			return typeof(Coop_Model_EnemyBossPop);
		case PACKET_TYPE.ENEMY_BOSS_ESCAPE:
			return typeof(Coop_Model_EnemyBossEscape);
		case PACKET_TYPE.ENEMY_BOSS_ALIVE_REQUEST:
			return typeof(Coop_Model_EnemyBossAliveRequest);
		case PACKET_TYPE.ENEMY_BOSS_ALIVE_REQUESTED:
			return typeof(Coop_Model_EnemyBossAliveRequested);
		case PACKET_TYPE.PLAYER_SNATCH_POS:
			return typeof(Coop_Model_PlayerSnatchPos);
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_START:
			return typeof(Coop_Model_PlayerSnatchMoveStart);
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_END:
			return typeof(Coop_Model_PlayerSnatchMoveEnd);
		case PACKET_TYPE.WAVEMATCH_INFO:
			return typeof(Coop_Model_WaveMatchInfo);
		case PACKET_TYPE.PLAYER_PAIR_SWORDS_LASER_END:
			return typeof(Coop_Model_PlayerPairSwordsLaserEnd);
		case PACKET_TYPE.ENEMY_REGION_NODE_ACTIVATE:
			return typeof(Coop_Model_EnemyRegionNodeActivate);
		case PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING:
			return typeof(Coop_Model_PlayerShotHealingHoming);
		case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_SET:
			return typeof(Coop_Model_ObjectBulletObservableSet);
		case PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_BROKEN:
			return typeof(Coop_Model_ObjectBulletObservableBroken);
		default:
			if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
			{
				Log.Warning(LOG.WEBSOCK, "not found packet type. " + type);
			}
			return typeof(Coop_Model_Base);
		}
	}
}
