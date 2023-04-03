using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cs.Engine.Network.Buffer;
using Cs.Engine.Util;
using Cs.Protocol.Detail;

namespace Cs.Protocol
{
	// Token: 0x020010C5 RID: 4293
	public sealed class PacketWriter : IDisposable, IPacketStream
	{
		// Token: 0x06009D95 RID: 40341 RVA: 0x00338CDB File Offset: 0x00336EDB
		public PacketWriter(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06009D96 RID: 40342 RVA: 0x00338CEC File Offset: 0x00336EEC
		public static ZeroCopyBuffer ToBufferWithoutNullBit(ISerializable data)
		{
			ZeroCopyBuffer zeroCopyBuffer = new ZeroCopyBuffer();
			using (PacketWriter packetWriter = new PacketWriter(zeroCopyBuffer.GetWriter()))
			{
				packetWriter.PutWithoutNullBit(data);
			}
			return zeroCopyBuffer;
		}

		// Token: 0x06009D97 RID: 40343 RVA: 0x00338D30 File Offset: 0x00336F30
		public void Dispose()
		{
			this.writer.Dispose();
		}

		// Token: 0x06009D98 RID: 40344 RVA: 0x00338D3D File Offset: 0x00336F3D
		public void PutOrGet(ref bool data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009D99 RID: 40345 RVA: 0x00338D4C File Offset: 0x00336F4C
		public void PutOrGet(ref sbyte data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009D9A RID: 40346 RVA: 0x00338D5B File Offset: 0x00336F5B
		public void PutOrGet(ref byte data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009D9B RID: 40347 RVA: 0x00338D6A File Offset: 0x00336F6A
		public void PutOrGet(ref short data)
		{
			this.WriteRawVarint32(ZigZag.Encode32((int)data));
		}

		// Token: 0x06009D9C RID: 40348 RVA: 0x00338D79 File Offset: 0x00336F79
		public void PutOrGet(ref ushort data)
		{
			this.WriteRawVarint32((uint)data);
		}

		// Token: 0x06009D9D RID: 40349 RVA: 0x00338D83 File Offset: 0x00336F83
		public void PutOrGet(ref int data)
		{
			this.WriteRawVarint32(ZigZag.Encode32(data));
		}

		// Token: 0x06009D9E RID: 40350 RVA: 0x00338D92 File Offset: 0x00336F92
		public void PutOrGet(ref uint data)
		{
			this.WriteRawVarint32(data);
		}

		// Token: 0x06009D9F RID: 40351 RVA: 0x00338D9C File Offset: 0x00336F9C
		public void PutOrGet(ref long data)
		{
			this.WriteRawVarint64(ZigZag.Encode64(data));
		}

		// Token: 0x06009DA0 RID: 40352 RVA: 0x00338DAB File Offset: 0x00336FAB
		public void PutOrGet(ref ulong data)
		{
			this.WriteRawVarint64(data);
		}

		// Token: 0x06009DA1 RID: 40353 RVA: 0x00338DB5 File Offset: 0x00336FB5
		public void PutOrGet(ref float data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DA2 RID: 40354 RVA: 0x00338DC4 File Offset: 0x00336FC4
		public void PutOrGet(ref double data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DA3 RID: 40355 RVA: 0x00338DD3 File Offset: 0x00336FD3
		public void PutOrGet(ref string data)
		{
			this.PutString(data);
		}

		// Token: 0x06009DA4 RID: 40356 RVA: 0x00338DE0 File Offset: 0x00336FE0
		public void AsHalf(ref float data)
		{
			uint num = data.FloatToLow();
			this.PutOrGet(ref num);
		}

		// Token: 0x06009DA5 RID: 40357 RVA: 0x00338DFD File Offset: 0x00336FFD
		public void PutOrGet(ref byte[] data)
		{
			if (data == null)
			{
				this.PutInt(0);
				return;
			}
			this.PutInt(data.Length);
			this.writer.Write(data);
		}

		// Token: 0x06009DA6 RID: 40358 RVA: 0x00338E24 File Offset: 0x00337024
		public void PutOrGet(ref BitArray data)
		{
			byte[] array = data.ToByteArray();
			this.PutUshort((ushort)array.Length);
			this.writer.Write(array);
		}

		// Token: 0x06009DA7 RID: 40359 RVA: 0x00338E4F File Offset: 0x0033704F
		public void PutOrGet(ref DateTime data)
		{
			this.PutRawLong(data.ToBinary());
		}

		// Token: 0x06009DA8 RID: 40360 RVA: 0x00338E5D File Offset: 0x0033705D
		public void PutOrGet(ref TimeSpan data)
		{
			this.PutRawLong(data.Ticks);
		}

		// Token: 0x06009DA9 RID: 40361 RVA: 0x00338E6C File Offset: 0x0033706C
		public void PutOrGet(ref bool[] data)
		{
			this.PutUshort((ushort)data.Length);
			for (int i = 0; i < data.Length; i++)
			{
				this.PutBool(data[i]);
			}
		}

		// Token: 0x06009DAA RID: 40362 RVA: 0x00338EA0 File Offset: 0x003370A0
		public void PutOrGet(ref int[] data)
		{
			this.PutUshort((ushort)data.Length);
			for (int i = 0; i < data.Length; i++)
			{
				this.PutInt(data[i]);
			}
		}

		// Token: 0x06009DAB RID: 40363 RVA: 0x00338ED4 File Offset: 0x003370D4
		public void PutOrGet(ref long[] data)
		{
			this.PutUshort((ushort)data.Length);
			for (int i = 0; i < data.Length; i++)
			{
				this.PutLong(data[i]);
			}
		}

		// Token: 0x06009DAC RID: 40364 RVA: 0x00338F08 File Offset: 0x00337108
		public void PutOrGet<T>(ref T[] data) where T : ISerializable
		{
			this.PutUshort((ushort)data.Length);
			for (int i = 0; i < data.Length; i++)
			{
				this.PutMessage(data[i]);
			}
		}

		// Token: 0x06009DAD RID: 40365 RVA: 0x00338F44 File Offset: 0x00337144
		public void PutOrGet(ref List<bool> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (bool data2 in data)
			{
				this.PutBool(data2);
			}
		}

		// Token: 0x06009DAE RID: 40366 RVA: 0x00338FA4 File Offset: 0x003371A4
		public void PutOrGet(ref List<byte> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (byte data2 in data)
			{
				this.PutByte(data2);
			}
		}

		// Token: 0x06009DAF RID: 40367 RVA: 0x00339004 File Offset: 0x00337204
		public void PutOrGet(ref List<short> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (short data2 in data)
			{
				this.PutShort(data2);
			}
		}

		// Token: 0x06009DB0 RID: 40368 RVA: 0x00339064 File Offset: 0x00337264
		public void PutOrGet(ref List<int> data)
		{
			if (data == null)
			{
				this.PutUshort(0);
				return;
			}
			this.PutUshort((ushort)data.Count);
			foreach (int data2 in data)
			{
				this.PutInt(data2);
			}
		}

		// Token: 0x06009DB1 RID: 40369 RVA: 0x003390D0 File Offset: 0x003372D0
		public void PutOrGet(ref List<float> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (float data2 in data)
			{
				this.PutFloat(data2);
			}
		}

		// Token: 0x06009DB2 RID: 40370 RVA: 0x00339130 File Offset: 0x00337330
		public void PutOrGet(ref List<long> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (long data2 in data)
			{
				this.PutLong(data2);
			}
		}

		// Token: 0x06009DB3 RID: 40371 RVA: 0x00339190 File Offset: 0x00337390
		public void PutOrGet(ref List<string> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (string data2 in data)
			{
				this.PutString(data2);
			}
		}

		// Token: 0x06009DB4 RID: 40372 RVA: 0x003391F0 File Offset: 0x003373F0
		public void PutOrGet<T>(ref List<T> data) where T : ISerializable
		{
			if (data == null)
			{
				this.PutUshort(0);
				return;
			}
			this.PutUshort((ushort)data.Count);
			foreach (T t in data)
			{
				this.PutMessage(t);
			}
		}

		// Token: 0x06009DB5 RID: 40373 RVA: 0x00339260 File Offset: 0x00337460
		public void PutOrGet(ref HashSet<short> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (short data2 in data)
			{
				this.PutShort(data2);
			}
		}

		// Token: 0x06009DB6 RID: 40374 RVA: 0x003392C0 File Offset: 0x003374C0
		public void PutOrGet(ref HashSet<int> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (int data2 in data)
			{
				this.PutInt(data2);
			}
		}

		// Token: 0x06009DB7 RID: 40375 RVA: 0x00339320 File Offset: 0x00337520
		public void PutOrGet(ref HashSet<string> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (string data2 in data)
			{
				this.PutString(data2);
			}
		}

		// Token: 0x06009DB8 RID: 40376 RVA: 0x00339380 File Offset: 0x00337580
		public void PutOrGet(ref HashSet<long> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (long data2 in data)
			{
				this.PutLong(data2);
			}
		}

		// Token: 0x06009DB9 RID: 40377 RVA: 0x003393E0 File Offset: 0x003375E0
		public void PutOrGet<T>(ref HashSet<T> data) where T : ISerializable
		{
			if (data == null)
			{
				this.PutUshort(0);
				return;
			}
			this.PutUshort((ushort)data.Count);
			foreach (T t in data)
			{
				this.PutMessage(t);
			}
		}

		// Token: 0x06009DBA RID: 40378 RVA: 0x00339450 File Offset: 0x00337650
		public void PutOrGetEnum<T>(ref HashSet<T> data) where T : Enum
		{
			this.PutUshort((ushort)data.Count);
			foreach (T t in data)
			{
				this.PutInt((int)Convert.ChangeType(t, typeof(int)));
			}
		}

		// Token: 0x06009DBB RID: 40379 RVA: 0x003394C8 File Offset: 0x003376C8
		public void PutOrGet(ref Dictionary<int, int> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<int, int> keyValuePair in data)
			{
				this.PutInt(keyValuePair.Key);
				this.PutInt(keyValuePair.Value);
			}
		}

		// Token: 0x06009DBC RID: 40380 RVA: 0x00339538 File Offset: 0x00337738
		public void PutOrGet(ref Dictionary<int, float> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<int, float> keyValuePair in data)
			{
				this.PutInt(keyValuePair.Key);
				this.PutFloat(keyValuePair.Value);
			}
		}

		// Token: 0x06009DBD RID: 40381 RVA: 0x003395A8 File Offset: 0x003377A8
		public void PutOrGet(ref Dictionary<long, int> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<long, int> keyValuePair in data)
			{
				this.PutLong(keyValuePair.Key);
				this.PutInt(keyValuePair.Value);
			}
		}

		// Token: 0x06009DBE RID: 40382 RVA: 0x00339618 File Offset: 0x00337818
		public void PutOrGet(ref Dictionary<int, long> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<int, long> keyValuePair in data)
			{
				this.PutInt(keyValuePair.Key);
				this.PutLong(keyValuePair.Value);
			}
		}

		// Token: 0x06009DBF RID: 40383 RVA: 0x00339688 File Offset: 0x00337888
		public void PutOrGet(ref Dictionary<byte, byte> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<byte, byte> keyValuePair in data)
			{
				this.PutByte(keyValuePair.Key);
				this.PutByte(keyValuePair.Value);
			}
		}

		// Token: 0x06009DC0 RID: 40384 RVA: 0x003396F8 File Offset: 0x003378F8
		public void PutOrGet(ref Dictionary<byte, long> data)
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<byte, long> keyValuePair in data)
			{
				this.PutByte(keyValuePair.Key);
				this.PutLong(keyValuePair.Value);
			}
		}

		// Token: 0x06009DC1 RID: 40385 RVA: 0x00339768 File Offset: 0x00337968
		public void PutOrGet<T>(ref Dictionary<byte, T> data) where T : ISerializable
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<byte, T> keyValuePair in data)
			{
				this.PutByte(keyValuePair.Key);
				this.PutMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009DC2 RID: 40386 RVA: 0x003397E0 File Offset: 0x003379E0
		public void PutOrGet<T>(ref Dictionary<short, T> data) where T : ISerializable
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<short, T> keyValuePair in data)
			{
				this.PutShort(keyValuePair.Key);
				this.PutMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009DC3 RID: 40387 RVA: 0x00339858 File Offset: 0x00337A58
		public void PutOrGet<T>(ref Dictionary<int, T> data) where T : ISerializable
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<int, T> keyValuePair in data)
			{
				this.PutInt(keyValuePair.Key);
				this.PutMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009DC4 RID: 40388 RVA: 0x003398D0 File Offset: 0x00337AD0
		public void PutOrGet<T>(ref Dictionary<long, T> data) where T : ISerializable
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<long, T> keyValuePair in data)
			{
				this.PutLong(keyValuePair.Key);
				this.PutMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009DC5 RID: 40389 RVA: 0x00339948 File Offset: 0x00337B48
		public void PutOrGet<T>(ref Dictionary<string, T> data) where T : ISerializable
		{
			this.PutUshort((ushort)data.Count);
			foreach (KeyValuePair<string, T> keyValuePair in data)
			{
				this.PutString(keyValuePair.Key);
				this.PutMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009DC6 RID: 40390 RVA: 0x003399C0 File Offset: 0x00337BC0
		public void PutOrGetEnum<T>(ref T data) where T : Enum
		{
			this.PutInt((int)Convert.ChangeType(data, typeof(int)));
		}

		// Token: 0x06009DC7 RID: 40391 RVA: 0x003399E8 File Offset: 0x00337BE8
		public void PutOrGetEnum<T>(ref List<T> data) where T : Enum
		{
			this.PutUshort((ushort)data.Count);
			for (int i = 0; i < data.Count; i++)
			{
				this.PutInt((int)Convert.ChangeType(data[i], typeof(int)));
			}
		}

		// Token: 0x06009DC8 RID: 40392 RVA: 0x00339A3C File Offset: 0x00337C3C
		public void PutOrGet<T>(ref T data) where T : ISerializable
		{
			this.PutMessage(data);
		}

		// Token: 0x06009DC9 RID: 40393 RVA: 0x00339A4F File Offset: 0x00337C4F
		public void PutBool(bool data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DCA RID: 40394 RVA: 0x00339A5D File Offset: 0x00337C5D
		public void PutSByte(sbyte data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DCB RID: 40395 RVA: 0x00339A6B File Offset: 0x00337C6B
		public void PutByte(byte data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DCC RID: 40396 RVA: 0x00339A79 File Offset: 0x00337C79
		public void PutShort(short data)
		{
			this.WriteRawVarint32(ZigZag.Encode32((int)data));
		}

		// Token: 0x06009DCD RID: 40397 RVA: 0x00339A87 File Offset: 0x00337C87
		public void PutUshort(ushort data)
		{
			this.WriteRawVarint32((uint)data);
		}

		// Token: 0x06009DCE RID: 40398 RVA: 0x00339A90 File Offset: 0x00337C90
		public void PutInt(int data)
		{
			this.WriteRawVarint32(ZigZag.Encode32(data));
		}

		// Token: 0x06009DCF RID: 40399 RVA: 0x00339A9E File Offset: 0x00337C9E
		public void PutUint(uint data)
		{
			this.WriteRawVarint32(data);
		}

		// Token: 0x06009DD0 RID: 40400 RVA: 0x00339AA7 File Offset: 0x00337CA7
		public void PutLong(long data)
		{
			this.WriteRawVarint64(ZigZag.Encode64(data));
		}

		// Token: 0x06009DD1 RID: 40401 RVA: 0x00339AB5 File Offset: 0x00337CB5
		public void PutUlong(ulong data)
		{
			this.WriteRawVarint64(data);
		}

		// Token: 0x06009DD2 RID: 40402 RVA: 0x00339ABE File Offset: 0x00337CBE
		public void PutFloat(float data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DD3 RID: 40403 RVA: 0x00339ACC File Offset: 0x00337CCC
		public void PutDouble(double data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DD4 RID: 40404 RVA: 0x00339ADA File Offset: 0x00337CDA
		public void PutRawInt(int data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DD5 RID: 40405 RVA: 0x00339AE8 File Offset: 0x00337CE8
		public void PutRawUint(uint data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DD6 RID: 40406 RVA: 0x00339AF6 File Offset: 0x00337CF6
		public void PutRawLong(long data)
		{
			this.writer.Write(data);
		}

		// Token: 0x06009DD7 RID: 40407 RVA: 0x00339B04 File Offset: 0x00337D04
		public void PutString(string data)
		{
			if (data == null)
			{
				this.PutShort(-1);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			this.PutShort((short)bytes.Length);
			this.writer.Write(bytes);
		}

		// Token: 0x06009DD8 RID: 40408 RVA: 0x00339B3E File Offset: 0x00337D3E
		public void PutWithoutNullBit(ISerializable message)
		{
			message.Serialize(this);
		}

		// Token: 0x06009DD9 RID: 40409 RVA: 0x00339B47 File Offset: 0x00337D47
		public void PutMessage(ISerializable message)
		{
			if (message == null)
			{
				this.PutBool(false);
				return;
			}
			this.PutBool(true);
			message.Serialize(this);
		}

		// Token: 0x06009DDA RID: 40410 RVA: 0x00339B62 File Offset: 0x00337D62
		private void WriteRawVarint32(uint value)
		{
			while (value > 127U)
			{
				this.writer.Write((byte)((value & 127U) | 128U));
				value >>= 7;
			}
			this.writer.Write((byte)value);
		}

		// Token: 0x06009DDB RID: 40411 RVA: 0x00339B93 File Offset: 0x00337D93
		private void WriteRawVarint64(ulong value)
		{
			while (value > 127UL)
			{
				this.writer.Write((byte)((value & 127UL) | 128UL));
				value >>= 7;
			}
			this.writer.Write((byte)value);
		}

		// Token: 0x0400908E RID: 37006
		private readonly BinaryWriter writer;
	}
}
