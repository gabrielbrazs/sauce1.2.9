using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushResultPoint : GameSection
{
	private enum UI
	{
		OBJ_TITLE,
		LBL_QUEST_NAME,
		SPR_TITLE,
		OBJ_RUSH_POINT,
		OBJ_GET_POINT,
		LBL_GET_POINT,
		GRD_POINT_DETAIL,
		OBJ_POINT_DETAIL_ITEM,
		LBL_POINT,
		LBL_POINT_NAME,
		STR_POINT_NEXT,
		OBJ_TOTAL_POINT,
		LBL_TOTAL_POINT,
		OBJ_NEXT_REWARD,
		GRD_NEXT_ITEM_ROOT,
		OBJ_NEXT_ITEM_ROOT,
		BTN_NEXT,
		BTN_SKIP_FULL_SCREEN,
		BTN_SKIP_IN_SCROLL,
		OBJ_GET_REWARD_ROOT,
		GRD_ITEM_ROOT,
		OBJ_ITEM_ROOT
	}

	private enum AUDIO
	{
		CATEGORY = 40000228,
		COUNTUP = 40000012,
		POINTREWARD = 40000230
	}

	private enum RESULT_ANIM_STATE
	{
		IDLE,
		POINT,
		COUNT_UP,
		NEXT_REWARD,
		REWARD,
		END
	}

	private const float COUNT_ANIM_SPEED = 4f;

	private bool is_skip;

	private PointEventCurrentData allPointEvents;

	private RESULT_ANIM_STATE animState;

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(LoadSE());
		base.Initialize();
	}

	private IEnumerator LoadSE()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		int[] ids = (int[])Enum.GetValues(typeof(AUDIO));
		int[] array = ids;
		foreach (int id in array)
		{
			load_queue.CacheSE(id, null);
		}
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
	}

	public override void UpdateUI()
	{
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		allPointEvents = new PointEventCurrentData();
		allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
		for (int j = 0; j < MonoBehaviourSingleton<InGameManager>.I.rushPointEvents.Count; j++)
		{
			PointEventCurrentData pointEventCurrentData = MonoBehaviourSingleton<InGameManager>.I.rushPointEvents[j];
			if (j == 0)
			{
				allPointEvents.pointRankingData.userPoint = pointEventCurrentData.pointRankingData.userPoint;
			}
			AddPointEventData(pointEventCurrentData);
		}
		PointEventCurrentData.PointResultData d = allPointEvents.pointRankingData;
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		SetLabelText((Enum)UI.LBL_QUEST_NAME, questData.questText);
		SetFontStyle((Enum)UI.LBL_GET_POINT, 2);
		SetLabelText((Enum)UI.LBL_GET_POINT, "0pt");
		SetFontStyle((Enum)UI.LBL_TOTAL_POINT, 2);
		SetLabelText((Enum)UI.LBL_TOTAL_POINT, d.userPoint.ToString("N0") + "pt");
		SetGrid(UI.GRD_POINT_DETAIL, "RushResultPointDetailItem", d.bonusPoint.Count, true, delegate(int i, Transform t, bool is_recycle)
		{
			UILabel component = FindCtrl(t, UI.LBL_POINT).GetComponent<UILabel>();
			component.alpha = 1f;
			component.text = d.bonusPoint[i].point.ToString("N0");
			component.fontStyle = 2;
			UILabel component2 = FindCtrl(t, UI.LBL_POINT_NAME).GetComponent<UILabel>();
			component2.alpha = 1f;
			component2.text = d.bonusPoint[i].name;
			component2.fontStyle = 2;
		});
		if (d.nextReward != null)
		{
			List<PointEventCurrentData.Reward> reward = d.nextReward.reward;
			SetAllRewardItem(UI.GRD_NEXT_ITEM_ROOT, reward);
			SetPoint(UI.OBJ_NEXT_REWARD, d.nextReward.point - (d.userPoint + d.getPoint));
		}
		else
		{
			SetActive((Enum)UI.STR_POINT_NEXT, false);
			SetFontStyle(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, 2);
			SetLabelText(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, "なし");
		}
		List<PointEventCurrentData.Reward> list = new List<PointEventCurrentData.Reward>();
		foreach (PointEventCurrentData.PointRewardData item in d.getReward)
		{
			list.AddRange(item.reward);
		}
		SetAllRewardItem(UI.GRD_ITEM_ROOT, list);
		this.StartCoroutine(PlayAnimation());
	}

	private void SetAllRewardItem(UI targetGrid, List<PointEventCurrentData.Reward> rewardList)
	{
		SetGrid(targetGrid, "ItemIconReward", rewardList.Count, true, delegate(int i, Transform t, bool is_recycle)
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			PointEventCurrentData.Reward reward = rewardList[i];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, t, reward.num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			if (itemIcon != null)
			{
				itemIcon.SetEnableCollider(false);
			}
			t.FindChild("itemNum").GetComponent<UILabel>().text = "×" + rewardList[i].num;
			if (targetGrid == UI.GRD_NEXT_ITEM_ROOT)
			{
				t.set_localScale(new Vector3(0.7f, 0.7f, 1f));
				if (i > 2)
				{
					itemIcon.VisibleIcon(false, true);
				}
			}
		});
	}

	private void SetPoint(UI parent, int point)
	{
		SetFontStyle(GetCtrl(parent), UI.LBL_POINT, 2);
		SetLabelText(GetCtrl(parent), UI.LBL_POINT, point.ToString("N0") + "pt");
	}

	private void AddPointEventData(PointEventCurrentData add_data)
	{
		if (add_data != null && Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).eventId == add_data.eventId)
		{
			allPointEvents.pointRankingData.getPoint += add_data.pointRankingData.getPoint;
			using (List<PointEventCurrentData.BonusPointData>.Enumerator enumerator = add_data.pointRankingData.bonusPoint.GetEnumerator())
			{
				PointEventCurrentData.BonusPointData add_bonus;
				while (enumerator.MoveNext())
				{
					add_bonus = enumerator.Current;
					PointEventCurrentData.BonusPointData bonusPointData = allPointEvents.pointRankingData.bonusPoint.Find((PointEventCurrentData.BonusPointData bp) => bp.name == add_bonus.name);
					if (bonusPointData == null)
					{
						allPointEvents.pointRankingData.bonusPoint.Add(add_bonus);
					}
					else
					{
						bonusPointData.point += add_bonus.point;
					}
				}
			}
			allPointEvents.pointRankingData.nextReward = add_data.pointRankingData.nextReward;
			allPointEvents.pointRankingData.getReward.AddRange(add_data.pointRankingData.getReward);
		}
	}

	private IEnumerator PlayAnimation()
	{
		is_skip = false;
		PlayTween((Enum)UI.OBJ_TITLE, true, (EventDelegate.Callback)null, true, 0);
		SkipTween((Enum)UI.OBJ_TITLE, true, 0);
		animState = RESULT_ANIM_STATE.POINT;
		PlayTween((Enum)UI.OBJ_RUSH_POINT, true, (EventDelegate.Callback)delegate
		{
			SoundManager.PlayOneShotUISE(40000228);
			((_003CPlayAnimation_003Ec__Iterator11C)/*Error near IL_0072: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
		}, false, 0);
		while (animState != 0 && !is_skip)
		{
			yield return (object)null;
		}
		animState = RESULT_ANIM_STATE.COUNT_UP;
		this.StartCoroutine(GetPointAnimation(delegate
		{
			((_003CPlayAnimation_003Ec__Iterator11C)/*Error near IL_00d5: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
		}));
		while (animState != 0 && !is_skip)
		{
			yield return (object)null;
		}
		animState = RESULT_ANIM_STATE.NEXT_REWARD;
		PlayTween((Enum)UI.OBJ_NEXT_REWARD, true, (EventDelegate.Callback)delegate
		{
			((_003CPlayAnimation_003Ec__Iterator11C)/*Error near IL_013e: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
		}, false, 0);
		SoundManager.PlayOneShotUISE(40000228);
		if (allPointEvents.pointRankingData.getReward.Count > 0)
		{
			animState = RESULT_ANIM_STATE.REWARD;
			PlayTween((Enum)UI.OBJ_GET_REWARD_ROOT, true, (EventDelegate.Callback)delegate
			{
				((_003CPlayAnimation_003Ec__Iterator11C)/*Error near IL_0195: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
			}, false, 0);
		}
		animState = RESULT_ANIM_STATE.END;
		VisibleEndButton();
	}

	private IEnumerator GetPointAnimation(Action callback)
	{
		int getPoint = allPointEvents.pointRankingData.getPoint;
		int userPoint = allPointEvents.pointRankingData.userPoint;
		int totalPoint = userPoint + getPoint;
		SetFontStyle((Enum)UI.LBL_GET_POINT, 2);
		yield return (object)this.StartCoroutine(CountUpAnimation(0f, getPoint, UI.LBL_GET_POINT));
		SetFontStyle((Enum)UI.LBL_TOTAL_POINT, 2);
		yield return (object)this.StartCoroutine(CountUpAnimation((float)userPoint, totalPoint, UI.LBL_TOTAL_POINT));
		callback();
	}

	private IEnumerator CountUpAnimation(float currentPoint, int targetPoint, UI targetUI)
	{
		while (currentPoint < (float)targetPoint)
		{
			yield return (object)0;
			if (is_skip)
			{
				currentPoint = (float)targetPoint;
			}
			int before = Mathf.FloorToInt(currentPoint);
			float addingPoint = Mathf.Max(((float)targetPoint - currentPoint) * CountDownCube(Time.get_deltaTime() * 4f), 1f);
			currentPoint += addingPoint;
			currentPoint = Mathf.Min(currentPoint, (float)targetPoint);
			if (before < Mathf.FloorToInt(currentPoint))
			{
				SoundManager.PlayOneShotUISE(40000012);
			}
			SetLabelText((Enum)targetUI, Mathf.FloorToInt(currentPoint).ToString("N0") + "pt");
		}
	}

	private static float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	private void VisibleEndButton()
	{
		SetActive((Enum)UI.BTN_NEXT, animState == RESULT_ANIM_STATE.END);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, animState != RESULT_ANIM_STATE.END);
		SetActive((Enum)UI.BTN_SKIP_IN_SCROLL, animState != RESULT_ANIM_STATE.END);
	}

	private void OnQuery_SKIP()
	{
		is_skip = true;
		switch (animState)
		{
		case RESULT_ANIM_STATE.POINT:
		case RESULT_ANIM_STATE.COUNT_UP:
		case RESULT_ANIM_STATE.NEXT_REWARD:
			SkipTween((Enum)UI.OBJ_RUSH_POINT, true, 0);
			SkipTween((Enum)UI.OBJ_NEXT_REWARD, true, 0);
			break;
		case RESULT_ANIM_STATE.REWARD:
			SkipTween((Enum)UI.OBJ_GET_REWARD_ROOT, true, 0);
			break;
		}
	}

	private void OnQuery_NEXT()
	{
		if (MonoBehaviourSingleton<QuestManager>.I.compData == null)
		{
			GameSection.ChangeEvent("FAILED", null);
		}
	}
}
