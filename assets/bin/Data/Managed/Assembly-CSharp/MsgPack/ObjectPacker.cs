using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

namespace MsgPack
{
	public class ObjectPacker
	{
		private delegate void PackDelegate(ObjectPacker packer, MsgPackWriter writer, object o);

		private delegate object UnpackDelegate(ObjectPacker packer, MsgPackReader reader);

		private byte[] _buf = new byte[64];

		private static Dictionary<Type, PackDelegate> PackerMapping;

		private static Dictionary<Type, UnpackDelegate> UnpackerMapping;

		static ObjectPacker()
		{
			PackerMapping = new Dictionary<Type, PackDelegate>();
			UnpackerMapping = new Dictionary<Type, UnpackDelegate>();
			PackerMapping.Add(typeof(string), StringPacker);
			UnpackerMapping.Add(typeof(string), StringUnpacker);
			PackerMapping.Add(typeof(DateTime), DateTimePacker);
			UnpackerMapping.Add(typeof(DateTime), DateTimeUnpacker);
			PackerMapping.Add(typeof(XorInt), XorIntPacker);
			UnpackerMapping.Add(typeof(XorInt), XorIntUnpacker);
			PackerMapping.Add(typeof(XorUInt), XorUIntPacker);
			UnpackerMapping.Add(typeof(XorUInt), XorUIntUnpacker);
			PackerMapping.Add(typeof(XorFloat), XorFloatPacker);
			UnpackerMapping.Add(typeof(XorFloat), XorFloatUnpacker);
		}

		public byte[] Pack(object o)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Pack(memoryStream, o);
				return memoryStream.ToArray();
				IL_001a:
				byte[] result;
				return result;
			}
		}

		public void Pack(Stream strm, object o)
		{
			if (o != null && o.GetType().IsPrimitive)
			{
				throw new NotSupportedException();
			}
			MsgPackWriter writer = new MsgPackWriter(strm);
			Pack(writer, o, null);
		}

		private void Pack(MsgPackWriter writer, object o, Type typeHint = null)
		{
			if (o == null)
			{
				if (typeHint == typeof(XorInt))
				{
					Pack(writer, 0, null);
				}
				else if (typeHint == typeof(XorUInt))
				{
					Pack(writer, 0u, null);
				}
				else if (typeHint == typeof(XorFloat))
				{
					Pack(writer, 0f, null);
				}
				else
				{
					writer.WriteNil();
				}
			}
			else
			{
				Type type = o.GetType();
				PackDelegate value;
				if (type.IsPrimitive)
				{
					if (type.Equals(typeof(int)))
					{
						writer.Write((int)o);
					}
					else if (type.Equals(typeof(uint)))
					{
						writer.Write((uint)o);
					}
					else if (type.Equals(typeof(float)))
					{
						writer.Write((float)o);
					}
					else if (type.Equals(typeof(double)))
					{
						writer.Write((double)o);
					}
					else if (type.Equals(typeof(long)))
					{
						writer.Write((long)o);
					}
					else if (type.Equals(typeof(ulong)))
					{
						writer.Write((ulong)o);
					}
					else if (type.Equals(typeof(bool)))
					{
						writer.Write((bool)o);
					}
					else if (type.Equals(typeof(byte)))
					{
						writer.Write((byte)o);
					}
					else if (type.Equals(typeof(sbyte)))
					{
						writer.Write((sbyte)o);
					}
					else if (type.Equals(typeof(short)))
					{
						writer.Write((short)o);
					}
					else if (type.Equals(typeof(ushort)))
					{
						writer.Write((ushort)o);
					}
					else
					{
						if (!type.Equals(typeof(char)))
						{
							throw new NotSupportedException();
						}
						writer.Write((ushort)(char)o);
					}
				}
				else if (PackerMapping.TryGetValue(type, out value))
				{
					value(this, writer, o);
				}
				else if (type.IsArray)
				{
					Array array = (Array)o;
					writer.WriteArrayHeader(array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						Pack(writer, array.GetValue(i), null);
					}
				}
				else if (type.IsEnum)
				{
					writer.Write((int)o);
				}
				else
				{
					ReflectionCacheEntry reflectionCacheEntry = ReflectionCache.Lookup(type);
					writer.WriteMapHeader(reflectionCacheEntry.FieldMap.Count);
					foreach (KeyValuePair<string, FieldInfo> item in reflectionCacheEntry.FieldMap)
					{
						writer.Write(item.Key, _buf);
						object value2 = item.Value.GetValue(o);
						if (item.Value.FieldType.IsInterface && value2 != null)
						{
							writer.WriteArrayHeader(2);
							writer.Write(value2.GetType().FullName);
						}
						Pack(writer, value2, item.Value.FieldType);
					}
				}
			}
		}

		public T Unpack<T>(byte[] buf)
		{
			return Unpack<T>(buf, 0, buf.Length);
		}

		public T Unpack<T>(byte[] buf, int offset, int size)
		{
			using (MemoryStream strm = new MemoryStream(buf, offset, size))
			{
				return Unpack<T>(strm);
				IL_0016:
				T result;
				return result;
			}
		}

		public T Unpack<T>(Stream strm)
		{
			if (typeof(T).IsPrimitive)
			{
				throw new NotSupportedException();
			}
			MsgPackReader reader = new MsgPackReader(strm);
			return (T)Unpack(reader, typeof(T));
		}

		public object Unpack(Type type, byte[] buf)
		{
			return Unpack(type, buf, 0, buf.Length);
		}

		public object Unpack(Type type, byte[] buf, int offset, int size)
		{
			using (MemoryStream strm = new MemoryStream(buf, offset, size))
			{
				return Unpack(type, strm);
				IL_0018:
				object result;
				return result;
			}
		}

		public object Unpack(Type type, Stream strm)
		{
			if (type.IsPrimitive)
			{
				throw new NotSupportedException();
			}
			MsgPackReader reader = new MsgPackReader(strm);
			return Unpack(reader, type);
		}

		private object Unpack(MsgPackReader reader, Type t)
		{
			//IL_0591: Unknown result type (might be due to invalid IL or missing references)
			//IL_0596: Expected O, but got Unknown
			if (t.IsPrimitive)
			{
				if (!reader.Read())
				{
					throw new FormatException();
				}
				if (t.Equals(typeof(int)) && reader.IsSigned())
				{
					return reader.ValueSigned;
				}
				if (t.Equals(typeof(int)) && reader.IsUnsigned())
				{
					return (int)reader.ValueUnsigned;
				}
				if (t.Equals(typeof(uint)) && reader.IsUnsigned())
				{
					return reader.ValueUnsigned;
				}
				if (t.Equals(typeof(float)))
				{
					if (reader.Type == TypePrefixes.Float)
					{
						return reader.ValueFloat;
					}
					if (reader.Type == TypePrefixes.Double)
					{
						return (float)reader.ValueDouble;
					}
					if (reader.IsUnsigned())
					{
						return (float)(double)reader.ValueUnsigned;
					}
					if (reader.IsSigned())
					{
						return (float)reader.ValueSigned;
					}
				}
				else
				{
					if (t.Equals(typeof(double)) && reader.Type == TypePrefixes.Double)
					{
						return reader.ValueDouble;
					}
					if (t.Equals(typeof(long)))
					{
						if (reader.IsSigned64())
						{
							return reader.ValueSigned64;
						}
						if (reader.IsSigned())
						{
							return (long)reader.ValueSigned;
						}
						if (reader.IsUnsigned64())
						{
							return (long)reader.ValueUnsigned64;
						}
						if (reader.IsUnsigned())
						{
							return (long)reader.ValueUnsigned;
						}
					}
					else
					{
						if (!t.Equals(typeof(ulong)))
						{
							if (t.Equals(typeof(bool)) && reader.IsBoolean())
							{
								return reader.Type == TypePrefixes.True;
							}
							if (t.Equals(typeof(byte)) && reader.IsUnsigned())
							{
								return (byte)reader.ValueUnsigned;
							}
							if (t.Equals(typeof(sbyte)) && reader.IsSigned())
							{
								return (sbyte)reader.ValueSigned;
							}
							if (t.Equals(typeof(short)) && reader.IsSigned())
							{
								return (short)reader.ValueSigned;
							}
							if (t.Equals(typeof(ushort)) && reader.IsUnsigned())
							{
								return (ushort)reader.ValueUnsigned;
							}
							if (t.Equals(typeof(char)) && reader.IsUnsigned())
							{
								return (char)reader.ValueUnsigned;
							}
							throw new NotSupportedException();
						}
						if (reader.IsUnsigned64())
						{
							return reader.ValueUnsigned64;
						}
						if (reader.IsUnsigned())
						{
							return (ulong)reader.ValueUnsigned;
						}
					}
				}
			}
			if (UnpackerMapping.TryGetValue(t, out UnpackDelegate value))
			{
				return value(this, reader);
			}
			if (t.IsArray)
			{
				if (!reader.Read() || (!reader.IsArray() && reader.Type != TypePrefixes.Nil))
				{
					throw new FormatException();
				}
				if (reader.Type == TypePrefixes.Nil)
				{
					return null;
				}
				Type elementType = t.GetElementType();
				Array array = Array.CreateInstance(elementType, (int)reader.Length);
				for (int i = 0; i < array.Length; i++)
				{
					array.SetValue(Unpack(reader, elementType), i);
				}
				return array;
			}
			if (t.IsEnum)
			{
				if (!reader.Read())
				{
					throw new FormatException();
				}
				if (reader.IsSigned())
				{
					return Enum.ToObject(t, reader.ValueSigned);
				}
				if (reader.IsSigned64())
				{
					return Enum.ToObject(t, reader.ValueSigned64);
				}
				if (reader.IsUnsigned())
				{
					return Enum.ToObject(t, reader.ValueUnsigned);
				}
				if (reader.IsUnsigned64())
				{
					return Enum.ToObject(t, reader.ValueUnsigned64);
				}
				if (reader.IsRaw())
				{
					CheckBufferSize((int)reader.Length);
					reader.ReadValueRaw(_buf, 0, (int)reader.Length);
					string @string = Encoding.UTF8.GetString(_buf, 0, (int)reader.Length);
					return Enum.Parse(t, @string);
				}
				throw new FormatException();
			}
			if (!reader.Read())
			{
				throw new FormatException();
			}
			if (reader.Type == TypePrefixes.Nil)
			{
				return null;
			}
			if (t.IsInterface)
			{
				if (reader.Type != TypePrefixes.FixArray && reader.Length != 2)
				{
					throw new FormatException();
				}
				if (!reader.Read() || !reader.IsRaw())
				{
					throw new FormatException();
				}
				CheckBufferSize((int)reader.Length);
				reader.ReadValueRaw(_buf, 0, (int)reader.Length);
				t = Type.GetType(Encoding.UTF8.GetString(_buf, 0, (int)reader.Length));
				if (!reader.Read() || reader.Type == TypePrefixes.Nil)
				{
					throw new FormatException();
				}
			}
			if (!reader.IsMap())
			{
				throw new FormatException();
			}
			object obj = (!typeof(ScriptableObject).IsAssignableFrom(t)) ? FormatterServices.GetUninitializedObject(t) : ((object)ScriptableObject.CreateInstance(t));
			ReflectionCacheEntry reflectionCacheEntry = ReflectionCache.Lookup(t);
			int length = (int)reader.Length;
			for (int j = 0; j < length; j++)
			{
				if (!reader.Read() || !reader.IsRaw())
				{
					throw new FormatException();
				}
				CheckBufferSize((int)reader.Length);
				reader.ReadValueRaw(_buf, 0, (int)reader.Length);
				string string2 = Encoding.UTF8.GetString(_buf, 0, (int)reader.Length);
				if (!reflectionCacheEntry.FieldMap.TryGetValue(string2, out FieldInfo value2))
				{
					new BoxingPacker().Unpack(reader);
				}
				else
				{
					value2.SetValue(obj, Unpack(reader, value2.FieldType));
				}
			}
			(obj as IDeserializationCallback)?.OnDeserialization(this);
			return obj;
		}

		private void CheckBufferSize(int size)
		{
			if (_buf.Length < size)
			{
				Array.Resize(ref _buf, size);
			}
		}

		private static void StringPacker(ObjectPacker packer, MsgPackWriter writer, object o)
		{
			writer.Write(Encoding.UTF8.GetBytes((string)o));
		}

		private static object StringUnpacker(ObjectPacker packer, MsgPackReader reader)
		{
			if (!reader.Read())
			{
				throw new FormatException();
			}
			if (reader.Type == TypePrefixes.Nil)
			{
				return null;
			}
			if (!reader.IsRaw())
			{
				throw new FormatException();
			}
			packer.CheckBufferSize((int)reader.Length);
			reader.ReadValueRaw(packer._buf, 0, (int)reader.Length);
			return Encoding.UTF8.GetString(packer._buf, 0, (int)reader.Length);
		}

		private static void DateTimePacker(ObjectPacker packer, MsgPackWriter writer, object o)
		{
			DateTime d = new DateTime(1970, 1, 1).ToLocalTime();
			writer.Write((long)((DateTime)o - d).TotalSeconds);
		}

		private static object DateTimeUnpacker(ObjectPacker packer, MsgPackReader reader)
		{
			if (!reader.Read())
			{
				throw new FormatException();
			}
			if (reader.Type == TypePrefixes.Nil)
			{
				return null;
			}
			if (!reader.IsUnsigned())
			{
				throw new FormatException();
			}
			return new DateTime(1970, 1, 1).ToLocalTime().AddSeconds((double)reader.ValueUnsigned);
		}

		private static void XorIntPacker(ObjectPacker packer, MsgPackWriter writer, object o)
		{
			packer.Pack(writer, (int)(XorInt)o, typeof(int));
		}

		private static object XorIntUnpacker(ObjectPacker packer, MsgPackReader reader)
		{
			return new XorInt((int)packer.Unpack(reader, typeof(int)));
		}

		private static void XorUIntPacker(ObjectPacker packer, MsgPackWriter writer, object o)
		{
			packer.Pack(writer, (uint)(XorUInt)o, typeof(uint));
		}

		private static object XorUIntUnpacker(ObjectPacker packer, MsgPackReader reader)
		{
			return new XorUInt((uint)packer.Unpack(reader, typeof(uint)));
		}

		private static void XorFloatPacker(ObjectPacker packer, MsgPackWriter writer, object o)
		{
			packer.Pack(writer, (float)(XorFloat)o, typeof(float));
		}

		private static object XorFloatUnpacker(ObjectPacker packer, MsgPackReader reader)
		{
			return new XorFloat((float)packer.Unpack(reader, typeof(float)));
		}
	}
}
