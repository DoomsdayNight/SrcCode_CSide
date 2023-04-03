using System;
using System.IO;
using Cs.Engine.Network.Buffer;
using NKC;

namespace Cs.Protocol
{
	// Token: 0x020010C6 RID: 4294
	public static class SerializerExt
	{
		// Token: 0x06009DDC RID: 40412 RVA: 0x00339BC8 File Offset: 0x00337DC8
		public static T DeepCopy<T>(this T source) where T : class, ISerializable, new()
		{
			ZeroCopyBuffer zeroCopyBuffer = PacketWriter.ToBufferWithoutNullBit(source);
			T result;
			using (zeroCopyBuffer.Hold())
			{
				using (PacketReader packetReader = new PacketReader(zeroCopyBuffer.GetReader()))
				{
					T t = NKCPacketObjectPool.OpenObject<T>();
					t.Serialize(packetReader);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x06009DDD RID: 40413 RVA: 0x00339C3C File Offset: 0x00337E3C
		public static void DeepCopyFrom<T>(this T copied, T source) where T : class, ISerializable
		{
			ZeroCopyBuffer zeroCopyBuffer = PacketWriter.ToBufferWithoutNullBit(source);
			using (zeroCopyBuffer.Hold())
			{
				using (PacketReader packetReader = new PacketReader(zeroCopyBuffer.GetReader()))
				{
					copied.Serialize(packetReader);
				}
			}
		}

		// Token: 0x06009DDE RID: 40414 RVA: 0x00339CA8 File Offset: 0x00337EA8
		public static void SaveToFile<T>(this T data, string filePath, string fileName) where T : class, ISerializable
		{
			ZeroCopyBuffer zeroCopyBuffer = PacketWriter.ToBufferWithoutNullBit(data);
			using (zeroCopyBuffer.Hold())
			{
				zeroCopyBuffer.WriteToFile(filePath, fileName);
			}
		}

		// Token: 0x06009DDF RID: 40415 RVA: 0x00339CF0 File Offset: 0x00337EF0
		public static void ReadFromFile<T>(this T data, string fullFilePath) where T : class, ISerializable
		{
			if (File.Exists(fullFilePath))
			{
				using (PacketReader packetReader = new PacketReader(File.ReadAllBytes(fullFilePath)))
				{
					packetReader.GetWithoutNullBit(data);
				}
			}
		}

		// Token: 0x06009DE0 RID: 40416 RVA: 0x00339D3C File Offset: 0x00337F3C
		public static string ToBase64<T>(this T data) where T : class, ISerializable
		{
			return PacketWriter.ToBufferWithoutNullBit(data).ToBase64();
		}

		// Token: 0x06009DE1 RID: 40417 RVA: 0x00339D50 File Offset: 0x00337F50
		public static bool FromBase64<T>(this T data, string base64) where T : class, ISerializable
		{
			try
			{
				using (PacketReader packetReader = new PacketReader(Convert.FromBase64String(base64)))
				{
					packetReader.GetWithoutNullBit(data);
				}
				return true;
			}
			catch
			{
			}
			return false;
		}
	}
}
