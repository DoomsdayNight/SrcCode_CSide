using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cs.Engine.Network.Buffer;
using Cs.Engine.Util;
using Cs.Protocol.Detail;

namespace Cs.Protocol
{
	// Token: 0x020010C4 RID: 4292
	public sealed class PacketSizeChecker : IPacketStream
	{
		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06009D55 RID: 40277 RVA: 0x00337F17 File Offset: 0x00336117
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06009D56 RID: 40278 RVA: 0x00337F1F File Offset: 0x0033611F
		public void PutOrGet(ref bool data)
		{
			this.size++;
		}

		// Token: 0x06009D57 RID: 40279 RVA: 0x00337F2F File Offset: 0x0033612F
		public void PutOrGet(ref sbyte data)
		{
			this.size++;
		}

		// Token: 0x06009D58 RID: 40280 RVA: 0x00337F3F File Offset: 0x0033613F
		public void PutOrGet(ref byte data)
		{
			this.size++;
		}

		// Token: 0x06009D59 RID: 40281 RVA: 0x00337F4F File Offset: 0x0033614F
		public void PutOrGet(ref short data)
		{
			this.CheckInt32((int)data);
		}

		// Token: 0x06009D5A RID: 40282 RVA: 0x00337F59 File Offset: 0x00336159
		public void PutOrGet(ref ushort data)
		{
			this.CheckUint32((uint)data);
		}

		// Token: 0x06009D5B RID: 40283 RVA: 0x00337F63 File Offset: 0x00336163
		public void PutOrGet(ref int data)
		{
			this.CheckInt32(data);
		}

		// Token: 0x06009D5C RID: 40284 RVA: 0x00337F6D File Offset: 0x0033616D
		public void PutOrGet(ref uint data)
		{
			this.CheckUint32(data);
		}

		// Token: 0x06009D5D RID: 40285 RVA: 0x00337F77 File Offset: 0x00336177
		public void PutOrGet(ref long data)
		{
			this.CheckInt64(data);
		}

		// Token: 0x06009D5E RID: 40286 RVA: 0x00337F81 File Offset: 0x00336181
		public void PutOrGet(ref ulong data)
		{
			this.CheckUint64(data);
		}

		// Token: 0x06009D5F RID: 40287 RVA: 0x00337F8B File Offset: 0x0033618B
		public void PutOrGet(ref float data)
		{
			this.size += 4;
		}

		// Token: 0x06009D60 RID: 40288 RVA: 0x00337F9B File Offset: 0x0033619B
		public void PutOrGet(ref double data)
		{
			this.size += 8;
		}

		// Token: 0x06009D61 RID: 40289 RVA: 0x00337FAB File Offset: 0x003361AB
		public void PutOrGet(ref string data)
		{
			this.CheckString(data);
		}

		// Token: 0x06009D62 RID: 40290 RVA: 0x00337FB5 File Offset: 0x003361B5
		public void CheckRawInt32(int data)
		{
			this.size += 4;
		}

		// Token: 0x06009D63 RID: 40291 RVA: 0x00337FC5 File Offset: 0x003361C5
		public void CheckRawUint32(uint data)
		{
			this.size += 4;
		}

		// Token: 0x06009D64 RID: 40292 RVA: 0x00337FD8 File Offset: 0x003361D8
		public void AsHalf(ref float data)
		{
			uint num = data.FloatToLow();
			this.PutOrGet(ref num);
		}

		// Token: 0x06009D65 RID: 40293 RVA: 0x00337FF5 File Offset: 0x003361F5
		public void PutOrGet(ref byte[] data)
		{
			this.CheckInt32(data.Length);
			this.size += data.Length;
		}

		// Token: 0x06009D66 RID: 40294 RVA: 0x00338014 File Offset: 0x00336214
		public void PutOrGet(ZeroCopyBuffer data)
		{
			int num = data.CalcTotalSize();
			this.CheckInt32(num);
			this.size += num;
		}

		// Token: 0x06009D67 RID: 40295 RVA: 0x00338040 File Offset: 0x00336240
		public void PutOrGet(ref BitArray data)
		{
			ushort num = (ushort)data.ToByteArray().Length;
			this.CheckUint32((uint)num);
			this.size += (int)num;
		}

		// Token: 0x06009D68 RID: 40296 RVA: 0x0033806D File Offset: 0x0033626D
		public void PutOrGet(ref DateTime data)
		{
			this.size += 8;
		}

		// Token: 0x06009D69 RID: 40297 RVA: 0x0033807D File Offset: 0x0033627D
		public void PutOrGet(ref TimeSpan data)
		{
			this.size += 8;
		}

		// Token: 0x06009D6A RID: 40298 RVA: 0x00338090 File Offset: 0x00336290
		public void PutOrGet(ref bool[] data)
		{
			ushort num = (ushort)data.Length;
			this.CheckUint32((uint)num);
			this.size += (int)num;
		}

		// Token: 0x06009D6B RID: 40299 RVA: 0x003380B8 File Offset: 0x003362B8
		public void PutOrGet(ref int[] data)
		{
			this.CheckUint32((uint)((ushort)data.Length));
			foreach (int data2 in data)
			{
				this.CheckInt32(data2);
			}
		}

		// Token: 0x06009D6C RID: 40300 RVA: 0x003380EC File Offset: 0x003362EC
		public void PutOrGet(ref long[] data)
		{
			this.CheckUint32((uint)((ushort)data.Length));
			foreach (long data2 in data)
			{
				this.CheckInt64(data2);
			}
		}

		// Token: 0x06009D6D RID: 40301 RVA: 0x00338120 File Offset: 0x00336320
		public void PutOrGet<T>(ref T[] data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Length));
			for (int i = 0; i < data.Length; i++)
			{
				this.CheckMessage(data[i]);
			}
		}

		// Token: 0x06009D6E RID: 40302 RVA: 0x0033815A File Offset: 0x0033635A
		public void PutOrGet(ref List<bool> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			this.size += data.Count;
		}

		// Token: 0x06009D6F RID: 40303 RVA: 0x0033817E File Offset: 0x0033637E
		public void PutOrGet(ref List<byte> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			this.size += data.Count;
		}

		// Token: 0x06009D70 RID: 40304 RVA: 0x003381A4 File Offset: 0x003363A4
		public void PutOrGet(ref List<short> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (short data2 in data)
			{
				this.CheckInt32((int)data2);
			}
		}

		// Token: 0x06009D71 RID: 40305 RVA: 0x00338204 File Offset: 0x00336404
		public void PutOrGet(ref List<int> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (int data2 in data)
			{
				this.CheckInt32(data2);
			}
		}

		// Token: 0x06009D72 RID: 40306 RVA: 0x00338264 File Offset: 0x00336464
		public void PutOrGet(ref List<float> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			this.size += 4 * data.Count;
		}

		// Token: 0x06009D73 RID: 40307 RVA: 0x0033828C File Offset: 0x0033648C
		public void PutOrGet(ref List<long> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (long data2 in data)
			{
				this.CheckInt64(data2);
			}
		}

		// Token: 0x06009D74 RID: 40308 RVA: 0x003382EC File Offset: 0x003364EC
		public void PutOrGet(ref List<string> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (string data2 in data)
			{
				this.CheckString(data2);
			}
		}

		// Token: 0x06009D75 RID: 40309 RVA: 0x0033834C File Offset: 0x0033654C
		public void PutOrGet<T>(ref List<T> data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (T t in data)
			{
				this.CheckMessage(t);
			}
		}

		// Token: 0x06009D76 RID: 40310 RVA: 0x003383B0 File Offset: 0x003365B0
		public void PutOrGet(ref HashSet<short> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (short data2 in data)
			{
				this.CheckInt32((int)data2);
			}
		}

		// Token: 0x06009D77 RID: 40311 RVA: 0x00338410 File Offset: 0x00336610
		public void PutOrGet(ref HashSet<int> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (int data2 in data)
			{
				this.CheckInt32(data2);
			}
		}

		// Token: 0x06009D78 RID: 40312 RVA: 0x00338470 File Offset: 0x00336670
		public void PutOrGet(ref HashSet<string> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (string data2 in data)
			{
				this.CheckString(data2);
			}
		}

		// Token: 0x06009D79 RID: 40313 RVA: 0x003384D0 File Offset: 0x003366D0
		public void PutOrGet(ref HashSet<long> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (long data2 in data)
			{
				this.CheckInt64(data2);
			}
		}

		// Token: 0x06009D7A RID: 40314 RVA: 0x00338530 File Offset: 0x00336730
		public void PutOrGet<T>(ref HashSet<T> data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (T t in data)
			{
				this.CheckMessage(t);
			}
		}

		// Token: 0x06009D7B RID: 40315 RVA: 0x00338594 File Offset: 0x00336794
		public void PutOrGetEnum<T>(ref HashSet<T> data) where T : Enum
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (T data2 in data)
			{
				this.CheckEnum<T>(data2);
			}
		}

		// Token: 0x06009D7C RID: 40316 RVA: 0x003385F4 File Offset: 0x003367F4
		public void PutOrGet(ref Dictionary<int, int> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<int, int> keyValuePair in data)
			{
				this.CheckInt32(keyValuePair.Key);
				this.CheckInt32(keyValuePair.Value);
			}
		}

		// Token: 0x06009D7D RID: 40317 RVA: 0x00338664 File Offset: 0x00336864
		public void PutOrGet(ref Dictionary<int, float> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<int, float> keyValuePair in data)
			{
				this.CheckInt32(keyValuePair.Key);
				this.CheckFloat(keyValuePair.Value);
			}
		}

		// Token: 0x06009D7E RID: 40318 RVA: 0x003386D4 File Offset: 0x003368D4
		public void PutOrGet(ref Dictionary<int, long> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<int, long> keyValuePair in data)
			{
				this.CheckInt32(keyValuePair.Key);
				this.CheckInt64(keyValuePair.Value);
			}
		}

		// Token: 0x06009D7F RID: 40319 RVA: 0x00338744 File Offset: 0x00336944
		public void PutOrGet(ref Dictionary<long, int> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<long, int> keyValuePair in data)
			{
				this.CheckInt64(keyValuePair.Key);
				this.CheckInt32(keyValuePair.Value);
			}
		}

		// Token: 0x06009D80 RID: 40320 RVA: 0x003387B4 File Offset: 0x003369B4
		public void PutOrGet(ref Dictionary<byte, byte> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			this.size += data.Count * 2;
		}

		// Token: 0x06009D81 RID: 40321 RVA: 0x003387DC File Offset: 0x003369DC
		public void PutOrGet(ref Dictionary<byte, long> data)
		{
			this.CheckUint32((uint)((ushort)data.Count));
			this.size += data.Count;
			foreach (KeyValuePair<byte, long> keyValuePair in data)
			{
				this.CheckInt64(keyValuePair.Value);
			}
		}

		// Token: 0x06009D82 RID: 40322 RVA: 0x00338854 File Offset: 0x00336A54
		public void PutOrGet<T>(ref Dictionary<byte, T> data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Count));
			this.size += data.Count;
			foreach (T t in data.Values)
			{
				this.CheckMessage(t);
			}
		}

		// Token: 0x06009D83 RID: 40323 RVA: 0x003388D0 File Offset: 0x00336AD0
		public void PutOrGet<T>(ref Dictionary<short, T> data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<short, T> keyValuePair in data)
			{
				this.CheckInt32((int)keyValuePair.Key);
				this.CheckMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009D84 RID: 40324 RVA: 0x00338948 File Offset: 0x00336B48
		public void PutOrGet<T>(ref Dictionary<int, T> data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<int, T> keyValuePair in data)
			{
				this.CheckInt32(keyValuePair.Key);
				this.CheckMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009D85 RID: 40325 RVA: 0x003389C0 File Offset: 0x00336BC0
		public void PutOrGet<T>(ref Dictionary<long, T> data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<long, T> keyValuePair in data)
			{
				this.CheckInt64(keyValuePair.Key);
				this.CheckMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009D86 RID: 40326 RVA: 0x00338A38 File Offset: 0x00336C38
		public void PutOrGet<T>(ref Dictionary<string, T> data) where T : ISerializable
		{
			this.CheckUint32((uint)((ushort)data.Count));
			foreach (KeyValuePair<string, T> keyValuePair in data)
			{
				this.CheckString(keyValuePair.Key);
				this.CheckMessage(keyValuePair.Value);
			}
		}

		// Token: 0x06009D87 RID: 40327 RVA: 0x00338AB0 File Offset: 0x00336CB0
		public void PutOrGetEnum<T>(ref T data) where T : Enum
		{
			this.CheckEnum<T>(data);
		}

		// Token: 0x06009D88 RID: 40328 RVA: 0x00338AC0 File Offset: 0x00336CC0
		public void PutOrGetEnum<T>(ref List<T> list) where T : Enum
		{
			this.CheckUint32((uint)((ushort)list.Count));
			foreach (T data in list)
			{
				this.CheckEnum<T>(data);
			}
		}

		// Token: 0x06009D89 RID: 40329 RVA: 0x00338B20 File Offset: 0x00336D20
		public void PutOrGet<T>(ref T data) where T : ISerializable
		{
			this.CheckMessage(data);
		}

		// Token: 0x06009D8A RID: 40330 RVA: 0x00338B33 File Offset: 0x00336D33
		public void CheckMessage(ISerializable message)
		{
			this.size++;
			if (message != null)
			{
				message.Serialize(this);
			}
		}

		// Token: 0x06009D8B RID: 40331 RVA: 0x00338B4D File Offset: 0x00336D4D
		private static int ComputeRawVarint32Size(uint value)
		{
			if ((value & 4294967168U) == 0U)
			{
				return 1;
			}
			if ((value & 4294950912U) == 0U)
			{
				return 2;
			}
			if ((value & 4292870144U) == 0U)
			{
				return 3;
			}
			if ((value & 4026531840U) == 0U)
			{
				return 4;
			}
			return 5;
		}

		// Token: 0x06009D8C RID: 40332 RVA: 0x00338B7C File Offset: 0x00336D7C
		private static int ComputeRawVarint64Size(ulong value)
		{
			if ((value & 18446744073709551488UL) == 0UL)
			{
				return 1;
			}
			if ((value & 18446744073709535232UL) == 0UL)
			{
				return 2;
			}
			if ((value & 18446744073707454464UL) == 0UL)
			{
				return 3;
			}
			if ((value & 18446744073441116160UL) == 0UL)
			{
				return 4;
			}
			if ((value & 18446744039349813248UL) == 0UL)
			{
				return 5;
			}
			if ((value & 18446739675663040512UL) == 0UL)
			{
				return 6;
			}
			if ((value & 18446181123756130304UL) == 0UL)
			{
				return 7;
			}
			if ((value & 18374686479671623680UL) == 0UL)
			{
				return 8;
			}
			if ((value & 9223372036854775808UL) == 0UL)
			{
				return 9;
			}
			return 10;
		}

		// Token: 0x06009D8D RID: 40333 RVA: 0x00338C04 File Offset: 0x00336E04
		private void CheckString(string data)
		{
			if (data == null)
			{
				this.CheckInt32(-1);
				return;
			}
			this.CheckInt32(data.Length);
			this.size += Encoding.UTF8.GetByteCount(data);
		}

		// Token: 0x06009D8E RID: 40334 RVA: 0x00338C35 File Offset: 0x00336E35
		private void CheckInt32(int data)
		{
			this.size += PacketSizeChecker.ComputeRawVarint32Size(ZigZag.Encode32(data));
		}

		// Token: 0x06009D8F RID: 40335 RVA: 0x00338C4F File Offset: 0x00336E4F
		private void CheckUint32(uint data)
		{
			this.size += PacketSizeChecker.ComputeRawVarint32Size(data);
		}

		// Token: 0x06009D90 RID: 40336 RVA: 0x00338C64 File Offset: 0x00336E64
		private void CheckInt64(long data)
		{
			this.size += PacketSizeChecker.ComputeRawVarint64Size(ZigZag.Encode64(data));
		}

		// Token: 0x06009D91 RID: 40337 RVA: 0x00338C7E File Offset: 0x00336E7E
		private void CheckUint64(ulong data)
		{
			this.size += PacketSizeChecker.ComputeRawVarint64Size(data);
		}

		// Token: 0x06009D92 RID: 40338 RVA: 0x00338C93 File Offset: 0x00336E93
		private void CheckFloat(float data)
		{
			this.size += 4;
		}

		// Token: 0x06009D93 RID: 40339 RVA: 0x00338CA4 File Offset: 0x00336EA4
		private void CheckEnum<T>(T data) where T : Enum
		{
			int data2 = (int)Convert.ChangeType(data, typeof(int));
			this.CheckInt32(data2);
		}

		// Token: 0x0400908D RID: 37005
		private int size;
	}
}
