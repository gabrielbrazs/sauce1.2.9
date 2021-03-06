using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestTable : Singleton<QuestTable>
{
	public class QuestTableData : IUIntKeyBinaryTableData
	{
		public const string NT = "questId,questType,questStyle,rarity,level,getType,eventId,grade,difficulty,sortPriority,locationNum,questNum,name,appearQuestId,appearDeliveryId,rushId,rushIconId,mapId,stage1Name,enemy1Id,enemy1Lv,bgm1Id,time,mission1Id,mission2Id,mission3Id,cantSell,forcedefeat,storyId";

		public uint questID;

		public QUEST_TYPE questType;

		public QUEST_STYLE questStyle;

		public RARITY_TYPE rarity;

		public int level;

		public GET_TYPE getType;

		public int eventId;

		public int grade;

		public DIFFICULTY_TYPE difficulty;

		public int sortPriority;

		public string locationNumber;

		public string questNumber;

		public string questText;

		public uint appearQuestId;

		public uint appearDeliveryId;

		public uint rushId;

		public uint rushIconId;

		public uint mapId;

		public string[] stageName = new string[1];

		public int[] enemyID = new int[1];

		public int[] enemyLv = new int[1];

		public int[] bgmID = new int[1];

		public float limitTime;

		public uint[] missionID = new uint[3];

		public bool cantSale;

		public bool forceDefeat;

		public int storyId;

		public int seriesNum;

		public QuestTableData()
		{
			for (int i = 0; i < 1; i++)
			{
				stageName[i] = string.Empty;
			}
		}

		public static bool cb(CSVReader csv_reader, QuestTableData data, ref uint key)
		{
			data.questID = key;
			csv_reader.Pop(ref data.questType);
			csv_reader.Pop(ref data.questStyle);
			csv_reader.PopEnum(ref data.rarity, RARITY_TYPE.D);
			csv_reader.Pop(ref data.level);
			csv_reader.Pop(ref data.getType);
			csv_reader.Pop(ref data.eventId);
			csv_reader.Pop(ref data.grade);
			csv_reader.Pop(ref data.difficulty);
			csv_reader.Pop(ref data.sortPriority);
			csv_reader.Pop(ref data.locationNumber);
			csv_reader.Pop(ref data.questNumber);
			csv_reader.Pop(ref data.questText);
			csv_reader.Pop(ref data.appearQuestId);
			csv_reader.Pop(ref data.appearDeliveryId);
			csv_reader.Pop(ref data.rushId);
			csv_reader.Pop(ref data.rushIconId);
			csv_reader.Pop(ref data.mapId);
			for (int i = 0; i < 1; i++)
			{
				csv_reader.Pop(ref data.stageName[i]);
			}
			for (int j = 0; j < 1; j++)
			{
				csv_reader.Pop(ref data.enemyID[j]);
				csv_reader.Pop(ref data.enemyLv[j]);
			}
			for (int k = 0; k < 1; k++)
			{
				csv_reader.Pop(ref data.bgmID[k]);
			}
			csv_reader.Pop(ref data.limitTime);
			csv_reader.Pop(ref data.missionID[0]);
			csv_reader.Pop(ref data.missionID[1]);
			csv_reader.Pop(ref data.missionID[2]);
			csv_reader.Pop(ref data.cantSale);
			csv_reader.Pop(ref data.forceDefeat);
			csv_reader.Pop(ref data.storyId);
			if (data.sortPriority == 0)
			{
				data.sortPriority = (int)key;
			}
			if (string.IsNullOrEmpty(data.locationNumber))
			{
				data.locationNumber = (data.questID / 100u % 1000u).ToString();
			}
			if (string.IsNullOrEmpty(data.questNumber))
			{
				data.questNumber = (data.questID % 100u).ToString();
			}
			data.seriesNum = 0;
			for (int l = 0; l < 1 && data.enemyID[l] != 0; l++)
			{
				data.seriesNum++;
			}
			return true;
		}

		public bool IsMissionExist()
		{
			if (missionID == null)
			{
				return false;
			}
			int i = 0;
			for (int num = missionID.Length; i < num; i++)
			{
				if (missionID[i] == 0)
				{
					return false;
				}
			}
			return true;
		}

		public int GetMainEnemyID()
		{
			if (seriesNum <= 0)
			{
				return 0;
			}
			return enemyID[seriesNum - 1];
		}

		public int GetMainEnemyLv()
		{
			if (seriesNum <= 0)
			{
				return 0;
			}
			int num = enemyLv[seriesNum - 1];
			if (num == 0 && Singleton<EnemyTable>.IsValid())
			{
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)GetMainEnemyID());
				if (enemyData != null)
				{
					num = enemyData.level;
				}
			}
			return num;
		}

		public string GetFoundationName()
		{
			return ResourceName.GetFoundationName(stageName[seriesNum - 1]);
		}

		public void LoadFromBinary(BinaryTableReader reader, ref uint key)
		{
			questID = key;
			questType = (QUEST_TYPE)reader.ReadInt32(0);
			questStyle = (QUEST_STYLE)reader.ReadInt32(0);
			rarity = (RARITY_TYPE)reader.ReadInt32(0);
			getType = (GET_TYPE)reader.ReadInt32(0);
			eventId = reader.ReadInt32(0);
			grade = reader.ReadInt32(0);
			difficulty = (DIFFICULTY_TYPE)reader.ReadInt32(0);
			sortPriority = reader.ReadInt32(0);
			locationNumber = reader.ReadString(string.Empty);
			questNumber = reader.ReadString(string.Empty);
			questText = reader.ReadString(string.Empty);
			appearQuestId = reader.ReadUInt32(0u);
			appearDeliveryId = reader.ReadUInt32(0u);
			rushId = reader.ReadUInt32(0u);
			mapId = reader.ReadUInt32(0u);
			for (int i = 0; i < 1; i++)
			{
				stageName[i] = reader.ReadString(string.Empty);
			}
			for (int j = 0; j < 1; j++)
			{
				enemyID[j] = reader.ReadInt32(0);
				enemyLv[j] = reader.ReadInt32(0);
			}
			for (int k = 0; k < 1; k++)
			{
				bgmID[k] = reader.ReadInt32(0);
			}
			limitTime = reader.ReadSingle(0f);
			missionID[0] = reader.ReadUInt32(0u);
			missionID[1] = reader.ReadUInt32(0u);
			missionID[2] = reader.ReadUInt32(0u);
			cantSale = reader.ReadBoolean(false);
			forceDefeat = reader.ReadBoolean(false);
			storyId = reader.ReadInt32(0);
			if (sortPriority == 0)
			{
				sortPriority = (int)key;
			}
			if (string.IsNullOrEmpty(locationNumber))
			{
				locationNumber = (questID / 100u % 1000u).ToString();
			}
			if (string.IsNullOrEmpty(questNumber))
			{
				questNumber = (questID % 100u).ToString();
			}
			seriesNum = 0;
			for (int l = 0; l < 1 && enemyID[l] != 0; l++)
			{
				seriesNum++;
			}
		}

		public void DumpBinary(BinaryWriter writer)
		{
			writer.Write((int)questType);
			writer.Write((int)questStyle);
			writer.Write((int)rarity);
			writer.Write((int)getType);
			writer.Write(eventId);
			writer.Write(grade);
			writer.Write((int)difficulty);
			writer.Write(sortPriority);
			writer.Write(locationNumber);
			writer.Write(questNumber);
			writer.Write(questText);
			writer.Write(appearQuestId);
			writer.Write(appearDeliveryId);
			writer.Write(mapId);
			for (int i = 0; i < 1; i++)
			{
				writer.Write(stageName[i]);
			}
			for (int j = 0; j < 1; j++)
			{
				writer.Write(enemyID[j]);
				writer.Write(enemyLv[j]);
			}
			for (int k = 0; k < 1; k++)
			{
				writer.Write(bgmID[k]);
			}
			writer.Write(limitTime);
			writer.Write(missionID[0]);
			writer.Write(missionID[1]);
			writer.Write(missionID[2]);
			writer.Write(cantSale);
			writer.Write(forceDefeat);
			writer.Write(storyId);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			QuestTableData questTableData = obj as QuestTableData;
			if (questTableData == null)
			{
				return false;
			}
			bool flag = questID == questTableData.questID && questType == questTableData.questType && questStyle == questTableData.questStyle && rarity == questTableData.rarity && getType == questTableData.getType && eventId == questTableData.eventId && grade == questTableData.grade && difficulty == questTableData.difficulty && sortPriority == questTableData.sortPriority && locationNumber == questTableData.locationNumber && questNumber == questTableData.questNumber && questText == questTableData.questText && appearQuestId == questTableData.appearQuestId && appearDeliveryId == questTableData.appearDeliveryId && mapId == questTableData.mapId && limitTime == questTableData.limitTime && cantSale == questTableData.cantSale && forceDefeat == questTableData.forceDefeat && storyId == questTableData.storyId && seriesNum == questTableData.seriesNum;
			for (int i = 0; i < stageName.Length; i++)
			{
				flag = (flag && stageName[i] == questTableData.stageName[i]);
			}
			for (int j = 0; j < enemyID.Length; j++)
			{
				flag = (flag && enemyID[j] == questTableData.enemyID[j]);
			}
			for (int k = 0; k < enemyLv.Length; k++)
			{
				flag = (flag && enemyLv[k] == questTableData.enemyLv[k]);
			}
			for (int l = 0; l < bgmID.Length; l++)
			{
				flag = (flag && bgmID[l] == questTableData.bgmID[l]);
			}
			for (int m = 0; m < missionID.Length; m++)
			{
				flag = (flag && missionID[m] == questTableData.missionID[m]);
			}
			return flag;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "questID:" + questID + ", questType:" + questType + ", questStyle:" + questStyle + ", rarity:" + rarity + ", getType:" + getType + ", eventId:" + eventId + ", grade:" + grade + ", difficulty:" + difficulty + ", sortPriority:" + sortPriority + ", locationNumber:" + locationNumber + ", questNumber:" + questNumber + ", questText:" + questText + ", appearQuestId:" + appearQuestId + ", appearDeliveryId:" + appearDeliveryId + ", mapId:" + mapId + ", limitTime:" + limitTime + ", cantSale:" + cantSale + ", forceDefeat:" + forceDefeat + ", storyId:" + storyId + ", seriesNum:" + seriesNum;
		}
	}

	public class MissionTableData
	{
		public const string NT = "missionId,name,type,require,param0";

		public uint missionID;

		public string missionText;

		public MISSION_TYPE missionType;

		public MISSION_REQUIRE missionRequire;

		public int missionParam;

		public static bool cb(CSVReader csv_reader, MissionTableData data, ref uint key)
		{
			data.missionID = key;
			csv_reader.Pop(ref data.missionText);
			csv_reader.Pop(ref data.missionType);
			csv_reader.Pop(ref data.missionRequire);
			csv_reader.Pop(ref data.missionParam);
			return true;
		}
	}

	public const int SERIES_NUM_MAX = 1;

	public const int MISSION_NUM_MAX = 3;

	private UIntKeyTable<QuestTableData> questTable;

	private UIntKeyTable<MissionTableData> missionTable;

	public static UIntKeyTable<QuestTableData> CreateQuestTableCSV(string csv_text)
	{
		return TableUtility.CreateUIntKeyTable<QuestTableData>(csv_text, QuestTableData.cb, "questId,questType,questStyle,rarity,level,getType,eventId,grade,difficulty,sortPriority,locationNum,questNum,name,appearQuestId,appearDeliveryId,rushId,rushIconId,mapId,stage1Name,enemy1Id,enemy1Lv,bgm1Id,time,mission1Id,mission2Id,mission3Id,cantSell,forcedefeat,storyId", null);
	}

	public void CreateQuestTable(string csv_text)
	{
		questTable = CreateQuestTableCSV(csv_text);
	}

	public void AddQuestTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(questTable, csv_text, QuestTableData.cb, "questId,questType,questStyle,rarity,level,getType,eventId,grade,difficulty,sortPriority,locationNum,questNum,name,appearQuestId,appearDeliveryId,rushId,rushIconId,mapId,stage1Name,enemy1Id,enemy1Lv,bgm1Id,time,mission1Id,mission2Id,mission3Id,cantSell,forcedefeat,storyId", null);
	}

	public void CreateMissionTable(string csv_text)
	{
		missionTable = TableUtility.CreateUIntKeyTable<MissionTableData>(csv_text, MissionTableData.cb, "missionId,name,type,require,param0", null);
	}

	public static UIntKeyTable<QuestTableData> CreateQuestTableBinary(byte[] bytes)
	{
		return TableUtility.CreateUIntKeyTableFromBinary<QuestTableData>(bytes);
	}

	public void CreateQuestTable(byte[] bytes)
	{
		questTable = CreateQuestTableBinary(bytes);
	}

	public void CreateQuestTable(MemoryStream stream)
	{
		questTable = TableUtility.CreateUIntKeyTableFromBinary<QuestTableData>(stream);
	}

	public void InitQuestDependencyData()
	{
		QuestCollection collection = MonoBehaviourSingleton<QuestManager>.I.questCollection;
		if (collection != null)
		{
			questTable.ForEach(delegate(QuestTableData data)
			{
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)data.GetMainEnemyID());
				if (enemyData != null)
				{
					collection.Collect(enemyData.type, data.questType);
				}
			});
			collection.Sort();
		}
	}

	public void AllQuestData(Action<QuestTableData> call_back)
	{
		if (questTable != null && call_back != null)
		{
			questTable.ForEach(delegate(QuestTableData data)
			{
				call_back(data);
			});
		}
	}

	public QuestTableData GetQuestData(uint id)
	{
		if (!Singleton<QuestTable>.IsValid())
		{
			return null;
		}
		QuestTableData questTableData = questTable.Get(id);
		if (questTableData == null)
		{
			Log.TableError(this, id);
			questTableData = new QuestTableData();
			questTableData.questText = Log.NON_DATA_NAME;
		}
		return questTableData;
	}

	public static int GetQuestNum(QuestTableData q)
	{
		if (q.questType != QUEST_TYPE.ORDER)
		{
			return -1;
		}
		QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(q.questID);
		if (questItem == null)
		{
			return 0;
		}
		return GetQuestNum(questItem);
	}

	public static int GetQuestNum(QuestItemInfo quest_item)
	{
		int num = quest_item.infoData.questData.num;
		int num2 = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			uint questId = quest_item.infoData.questData.tableData.questID;
			num2 = (from g in MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList
			where g.questId == (int)questId
			select g).Count();
		}
		int num3 = num - num2;
		return Mathf.Max(num3, 0);
	}

	public MissionTableData GetMissionData(uint id)
	{
		if (!Singleton<QuestTable>.IsValid())
		{
			return null;
		}
		return missionTable.Get(id);
	}

	public MissionTableData[] GetMissionData(uint[] mission_id)
	{
		MissionTableData[] array = new MissionTableData[mission_id.Length];
		for (int i = 0; i < mission_id.Length; i++)
		{
			array[i] = Singleton<QuestTable>.I.GetMissionData(mission_id[i]);
		}
		return array;
	}

	public bool FindConditionQuest(QUEST_TYPE?[] type, DIFFICULTY_TYPE? difficulty, ENEMY_TYPE enemy)
	{
		bool is_find = false;
		questTable.ForEach(delegate(QuestTableData table)
		{
			if (!is_find)
			{
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)table.GetMainEnemyID());
				if (enemyData != null && enemyData.type == enemy && (!difficulty.HasValue || (difficulty.GetValueOrDefault() == table.difficulty && difficulty.HasValue)))
				{
					int num = 0;
					int num2 = type.Length;
					while (true)
					{
						if (num >= num2)
						{
							return;
						}
						QUEST_TYPE? nullable = type[num];
						if (nullable.HasValue && table.questType == type[num])
						{
							break;
						}
						num++;
					}
					is_find = true;
				}
			}
		});
		return is_find;
	}

	public bool ContainEnemy(int enemy_id)
	{
		bool is_find = false;
		questTable.ForEach(delegate(QuestTableData table)
		{
			if (!is_find && table.enemyID != null)
			{
				int num = 0;
				int num2 = table.enemyID.Length;
				while (true)
				{
					if (num >= num2)
					{
						return;
					}
					if (table.enemyID[num] == enemy_id)
					{
						break;
					}
					num++;
				}
				is_find = true;
			}
		});
		return is_find;
	}

	public IEnumerable<QuestTableData> GetEnemyAppearQuestData(uint enemy_id)
	{
		List<QuestTableData> enemyTable = new List<QuestTableData>();
		questTable.ForEach(delegate(QuestTableData table)
		{
			if (table.enemyID != null && Array.IndexOf(table.enemyID, (int)((_003CGetEnemyAppearQuestData_003Ec__Iterator246)/*Error near IL_0039: stateMachine*/).enemy_id) >= 0)
			{
				((_003CGetEnemyAppearQuestData_003Ec__Iterator246)/*Error near IL_0039: stateMachine*/)._003CenemyTable_003E__0.Add(table);
			}
		});
		foreach (QuestTableData item in enemyTable)
		{
			yield return item;
		}
	}

	public static List<QuestTableData> GetSameRushQuestData(uint searchId)
	{
		List<QuestTableData> list = new List<QuestTableData>();
		Singleton<QuestTable>.I.questTable.ForEach(delegate(QuestTableData x)
		{
			if (x.rushId == searchId)
			{
				list.Add(x);
			}
		});
		return list;
	}
}
