using MsgPack.Serialization;
using UnityEngine;

namespace MsgPack
{
	public class Vector3Serializer : MessagePackSerializer<Vector3>
	{
		public Vector3Serializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

		protected override void PackToCore(Packer packer, Vector3 objectTree)
		{
			packer.PackArrayHeader(3);
			packer.Pack(objectTree.x);
			packer.Pack(objectTree.y);
			packer.Pack(objectTree.z);
		}

		protected override Vector3 UnpackFromCore(Unpacker unpacker)
		{
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			if (!unpacker.IsArrayHeader)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			int itemsCount = UnpackHelpers.GetItemsCount(unpacker);
			if (itemsCount != 3)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			if (!unpacker.IsArrayHeader)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			if (!unpacker.ReadSingle(out float result))
			{
				throw SerializationExceptions.NewMissingItem(0);
			}
			if (!unpacker.ReadSingle(out float result2))
			{
				throw SerializationExceptions.NewMissingItem(1);
			}
			if (!unpacker.ReadSingle(out float result3))
			{
				throw SerializationExceptions.NewMissingItem(2);
			}
			return new Vector3(result, result2, result3);
		}
	}
}
