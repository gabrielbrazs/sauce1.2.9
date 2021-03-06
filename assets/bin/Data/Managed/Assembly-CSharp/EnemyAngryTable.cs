using UnityEngine;

public class EnemyAngryTable : Singleton<EnemyAngryTable>, IDataTable
{
	public class Data
	{
		public const string NT = "id,condition,value1,value2,value3,value4";

		public uint id;

		public ANGRY_CONDITION condition;

		public int value1;

		public int value2;

		public int value3;

		public int value4;

		public uint actionID;

		public static bool cb(CSVReader csv_reader, Data data, ref uint key)
		{
			data.id = key;
			int value = 0;
			csv_reader.Pop(ref value);
			data.condition = (ANGRY_CONDITION)value;
			csv_reader.Pop(ref data.value1);
			csv_reader.Pop(ref data.value2);
			csv_reader.Pop(ref data.value3);
			csv_reader.Pop(ref data.value4);
			return true;
		}
	}

	private UIntKeyTable<Data> dataTable;

	public void CreateTable(TextAsset textasset)
	{
		CreateTable(textasset.get_text());
	}

	public void CreateTable(string text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<Data>(text, Data.cb, "id,condition,value1,value2,value3,value4", null);
		dataTable.TrimExcess();
	}

	public void AddTable(TextAsset textasset)
	{
		TableUtility.AddUIntKeyTable(dataTable, textasset.get_text(), Data.cb, "id,condition,value1,value2,value3,value4", null);
	}

	public Data GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(id);
	}
}
