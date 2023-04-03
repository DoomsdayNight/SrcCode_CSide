using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cs.Protocol.Detail;
using NKC;

namespace Cs.Protocol
{
	// Token: 0x020010C3 RID: 4291
	public sealed class PacketReader : IDisposable, IPacketStream
	{
		// Token: 0x06009D0B RID: 40203 RVA: 0x003372E1 File Offset: 0x003354E1
		public PacketReader(BinaryReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06009D0C RID: 40204 RVA: 0x003372F0 File Offset: 0x003354F0
		public PacketReader(byte[] buffer)
		{
			this.reader = new BinaryReader(new MemoryStream(buffer));
		}

		// Token: 0x06009D0D RID: 40205 RVA: 0x00337309 File Offset: 0x00335509
		public void Dispose()
		{
			this.reader.Close();
		}

		// Token: 0x06009D0E RID: 40206 RVA: 0x00337316 File Offset: 0x00335516
		public void PutOrGet(ref bool data)
		{
			data = this.reader.ReadBoolean();
		}

		// Token: 0x06009D0F RID: 40207 RVA: 0x00337325 File Offset: 0x00335525
		public void PutOrGet(ref sbyte data)
		{
			data = this.reader.ReadSByte();
		}

		// Token: 0x06009D10 RID: 40208 RVA: 0x00337334 File Offset: 0x00335534
		public void PutOrGet(ref byte data)
		{
			data = this.reader.ReadByte();
		}

		// Token: 0x06009D11 RID: 40209 RVA: 0x00337343 File Offset: 0x00335543
		public void PutOrGet(ref short data)
		{
			data = (short)ZigZag.Decode32(this.ReadRawVarInt32());
		}

		// Token: 0x06009D12 RID: 40210 RVA: 0x00337353 File Offset: 0x00335553
		public void PutOrGet(ref ushort data)
		{
			data = (ushort)this.ReadRawVarInt32();
		}

		// Token: 0x06009D13 RID: 40211 RVA: 0x0033735E File Offset: 0x0033555E
		public void PutOrGet(ref int data)
		{
			data = ZigZag.Decode32(this.ReadRawVarInt32());
		}

		// Token: 0x06009D14 RID: 40212 RVA: 0x0033736D File Offset: 0x0033556D
		public void PutOrGet(ref uint data)
		{
			data = this.ReadRawVarInt32();
		}

		// Token: 0x06009D15 RID: 40213 RVA: 0x00337377 File Offset: 0x00335577
		public void PutOrGet(ref long data)
		{
			data = ZigZag.Decode64(this.ReadRawVarInt64());
		}

		// Token: 0x06009D16 RID: 40214 RVA: 0x00337386 File Offset: 0x00335586
		public void PutOrGet(ref ulong data)
		{
			data = this.ReadRawVarInt64();
		}

		// Token: 0x06009D17 RID: 40215 RVA: 0x00337390 File Offset: 0x00335590
		public void PutOrGet(ref float data)
		{
			data = this.reader.ReadSingle();
		}

		// Token: 0x06009D18 RID: 40216 RVA: 0x0033739F File Offset: 0x0033559F
		public void PutOrGet(ref double data)
		{
			data = this.reader.ReadDouble();
		}

		// Token: 0x06009D19 RID: 40217 RVA: 0x003373AE File Offset: 0x003355AE
		public void PutOrGet(ref string data)
		{
			data = this.GetString();
		}

		// Token: 0x06009D1A RID: 40218 RVA: 0x003373B8 File Offset: 0x003355B8
		public void AsHalf(ref float data)
		{
			uint @uint = this.GetUint();
			data = @uint.LowToFloat();
		}

		// Token: 0x06009D1B RID: 40219 RVA: 0x003373D4 File Offset: 0x003355D4
		public void PutOrGet(ref byte[] data)
		{
			int @int = this.GetInt();
			data = this.reader.ReadBytes(@int);
		}

		// Token: 0x06009D1C RID: 40220 RVA: 0x003373F8 File Offset: 0x003355F8
		public void PutOrGet(ref BitArray data)
		{
			ushort @ushort = this.GetUshort();
			data = new BitArray(this.reader.ReadBytes((int)@ushort));
		}

		// Token: 0x06009D1D RID: 40221 RVA: 0x0033741F File Offset: 0x0033561F
		public void PutOrGet(ref DateTime data)
		{
			data = this.GetDateTime();
		}

		// Token: 0x06009D1E RID: 40222 RVA: 0x0033742D File Offset: 0x0033562D
		public void PutOrGet(ref TimeSpan data)
		{
			data = this.GetTimeSpan();
		}

		// Token: 0x06009D1F RID: 40223 RVA: 0x0033743C File Offset: 0x0033563C
		public void PutOrGet(ref bool[] data)
		{
			ushort @ushort = this.GetUshort();
			data = new bool[(int)@ushort];
			for (int i = 0; i < (int)@ushort; i++)
			{
				data[i] = this.GetBool();
			}
		}

		// Token: 0x06009D20 RID: 40224 RVA: 0x00337470 File Offset: 0x00335670
		public void PutOrGet(ref int[] data)
		{
			ushort @ushort = this.GetUshort();
			data = new int[(int)@ushort];
			for (int i = 0; i < (int)@ushort; i++)
			{
				data[i] = this.GetInt();
			}
		}

		// Token: 0x06009D21 RID: 40225 RVA: 0x003374A4 File Offset: 0x003356A4
		public void PutOrGet(ref long[] data)
		{
			ushort @ushort = this.GetUshort();
			data = new long[(int)@ushort];
			for (int i = 0; i < (int)@ushort; i++)
			{
				data[i] = this.GetLong();
			}
		}

		// Token: 0x06009D22 RID: 40226 RVA: 0x003374D8 File Offset: 0x003356D8
		public void PutOrGet<T>(ref T[] data) where T : ISerializable
		{
			ushort @ushort = this.GetUshort();
			data = new T[(int)@ushort];
			for (int i = 0; i < (int)@ushort; i++)
			{
				this.GetMessage<T>(out data[i]);
			}
		}

		// Token: 0x06009D23 RID: 40227 RVA: 0x00337510 File Offset: 0x00335710
		public void PutOrGet(ref List<bool> data)
		{
			ushort @ushort = this.GetUshort();
			data = new List<bool>((int)@ushort);
			for (int i = 0; i < (int)@ushort; i++)
			{
				data.Add(this.GetBool());
			}
		}

		// Token: 0x06009D24 RID: 40228 RVA: 0x00337548 File Offset: 0x00335748
		public void PutOrGet(ref List<byte> data)
		{
			ushort @ushort = this.GetUshort();
			data = new List<byte>((int)@ushort);
			for (int i = 0; i < (int)@ushort; i++)
			{
				data.Add(this.GetByte());
			}
		}

		// Token: 0x06009D25 RID: 40229 RVA: 0x00337580 File Offset: 0x00335780
		public void PutOrGet(ref List<short> data)
		{
			ushort @ushort = this.GetUshort();
			data = new List<short>((int)@ushort);
			for (int i = 0; i < (int)@ushort; i++)
			{
				data.Add(this.GetShort());
			}
		}

		// Token: 0x06009D26 RID: 40230 RVA: 0x003375B8 File Offset: 0x003357B8
		public void PutOrGet(ref List<int> data)
		{
			ushort @ushort = this.GetUshort();
			data = new List<int>((int)@ushort);
			for (int i = 0; i < (int)@ushort; i++)
			{
				data.Add(this.GetInt());
			}
		}

		// Token: 0x06009D27 RID: 40231 RVA: 0x003375F0 File Offset: 0x003357F0
		public void PutOrGet(ref List<float> data)
		{
			ushort @ushort = this.GetUshort();
			data = new List<float>((int)@ushort);
			for (int i = 0; i < (int)@ushort; i++)
			{
				data.Add(this.GetFloat());
			}
		}

		// Token: 0x06009D28 RID: 40232 RVA: 0x00337628 File Offset: 0x00335828
		public void PutOrGet(ref List<long> data)
		{
			ushort @ushort = this.GetUshort();
			data = new List<long>((int)@ushort);
			for (int i = 0; i < (int)@ushort; i++)
			{
				data.Add(this.GetLong());
			}
		}

		// Token: 0x06009D29 RID: 40233 RVA: 0x00337660 File Offset: 0x00335860
		public void PutOrGet(ref List<string> data)
		{
			ushort @ushort = this.GetUshort();
			data = new List<string>((int)@ushort);
			for (int i = 0; i < (int)@ushort; i++)
			{
				data.Add(this.GetString());
			}
		}

		// Token: 0x06009D2A RID: 40234 RVA: 0x00337698 File Offset: 0x00335898
		public void PutOrGet<T>(ref List<T> data) where T : ISerializable
		{
			ushort @ushort = this.GetUshort();
			if (data != null)
			{
				data.Clear();
			}
			else if (NKCPacketObjectPool.IsManagedType(typeof(List<T>)))
			{
				data = (List<T>)NKCPacketObjectPool.OpenObject(typeof(List<T>));
			}
			else
			{
				data = new List<T>((int)@ushort);
			}
			ICollection<T> collection = data;
			this.GetCollection<T>(collection, (int)@ushort);
		}

		// Token: 0x06009D2B RID: 40235 RVA: 0x003376F8 File Offset: 0x003358F8
		public void PutOrGet(ref HashSet<short> data)
		{
			ushort @ushort = this.GetUshort();
			data = new HashSet<short>();
			for (int i = 0; i < (int)@ushort; i++)
			{
				short @short = this.GetShort();
				data.Add(@short);
			}
		}

		// Token: 0x06009D2C RID: 40236 RVA: 0x00337730 File Offset: 0x00335930
		public void PutOrGet(ref HashSet<int> data)
		{
			ushort @ushort = this.GetUshort();
			data = new HashSet<int>();
			for (int i = 0; i < (int)@ushort; i++)
			{
				int @int = this.GetInt();
				data.Add(@int);
			}
		}

		// Token: 0x06009D2D RID: 40237 RVA: 0x00337768 File Offset: 0x00335968
		public void PutOrGet(ref HashSet<string> data)
		{
			ushort @ushort = this.GetUshort();
			data = new HashSet<string>();
			for (int i = 0; i < (int)@ushort; i++)
			{
				string @string = this.GetString();
				data.Add(@string);
			}
		}

		// Token: 0x06009D2E RID: 40238 RVA: 0x003377A0 File Offset: 0x003359A0
		public void PutOrGet(ref HashSet<long> data)
		{
			ushort @ushort = this.GetUshort();
			data = new HashSet<long>();
			for (int i = 0; i < (int)@ushort; i++)
			{
				long @long = this.GetLong();
				data.Add(@long);
			}
		}

		// Token: 0x06009D2F RID: 40239 RVA: 0x003377D8 File Offset: 0x003359D8
		public void PutOrGet<T>(ref HashSet<T> data) where T : ISerializable
		{
			ushort @ushort = this.GetUshort();
			if (data != null)
			{
				data.Clear();
			}
			else if (NKCPacketObjectPool.IsManagedType(typeof(HashSet<T>)))
			{
				data = (HashSet<T>)NKCPacketObjectPool.OpenObject(typeof(HashSet<T>));
			}
			else
			{
				data = new HashSet<T>();
			}
			ICollection<T> collection = data;
			this.GetCollection<T>(collection, (int)@ushort);
		}

		// Token: 0x06009D30 RID: 40240 RVA: 0x00337838 File Offset: 0x00335A38
		public void PutOrGetEnum<T>(ref HashSet<T> data) where T : Enum
		{
			data = new HashSet<T>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				int @int = this.GetInt();
				T item = (T)((object)Enum.ToObject(typeof(T), @int));
				data.Add(item);
			}
		}

		// Token: 0x06009D31 RID: 40241 RVA: 0x00337888 File Offset: 0x00335A88
		public void PutOrGet(ref Dictionary<int, int> data)
		{
			data = new Dictionary<int, int>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				int @int = this.GetInt();
				int int2 = this.GetInt();
				data.Add(@int, int2);
			}
		}

		// Token: 0x06009D32 RID: 40242 RVA: 0x003378C8 File Offset: 0x00335AC8
		public void PutOrGet(ref Dictionary<int, float> data)
		{
			data = new Dictionary<int, float>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				int @int = this.GetInt();
				float @float = this.GetFloat();
				data.Add(@int, @float);
			}
		}

		// Token: 0x06009D33 RID: 40243 RVA: 0x00337908 File Offset: 0x00335B08
		public void PutOrGet(ref Dictionary<long, int> data)
		{
			data = new Dictionary<long, int>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				long @long = this.GetLong();
				int @int = this.GetInt();
				data.Add(@long, @int);
			}
		}

		// Token: 0x06009D34 RID: 40244 RVA: 0x00337948 File Offset: 0x00335B48
		public void PutOrGet(ref Dictionary<int, long> data)
		{
			data = new Dictionary<int, long>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				int @int = this.GetInt();
				long @long = this.GetLong();
				data.Add(@int, @long);
			}
		}

		// Token: 0x06009D35 RID: 40245 RVA: 0x00337988 File Offset: 0x00335B88
		public void PutOrGet(ref Dictionary<byte, byte> data)
		{
			data = new Dictionary<byte, byte>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				byte @byte = this.GetByte();
				byte byte2 = this.GetByte();
				data.Add(@byte, byte2);
			}
		}

		// Token: 0x06009D36 RID: 40246 RVA: 0x003379C8 File Offset: 0x00335BC8
		public void PutOrGet(ref Dictionary<byte, long> data)
		{
			data = new Dictionary<byte, long>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				byte @byte = this.GetByte();
				long @long = this.GetLong();
				data.Add(@byte, @long);
			}
		}

		// Token: 0x06009D37 RID: 40247 RVA: 0x00337A08 File Offset: 0x00335C08
		public void PutOrGet<T>(ref Dictionary<byte, T> data) where T : ISerializable
		{
			if (data != null)
			{
				data.Clear();
			}
			else if (NKCPacketObjectPool.IsManagedType(typeof(Dictionary<byte, T>)))
			{
				data = (Dictionary<byte, T>)NKCPacketObjectPool.OpenObject(typeof(Dictionary<byte, T>));
			}
			else
			{
				data = new Dictionary<byte, T>();
			}
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				byte @byte = this.GetByte();
				T value;
				this.GetMessage<T>(out value);
				data.Add(@byte, value);
			}
		}

		// Token: 0x06009D38 RID: 40248 RVA: 0x00337A80 File Offset: 0x00335C80
		public void PutOrGet<T>(ref Dictionary<short, T> data) where T : ISerializable
		{
			if (data != null)
			{
				data.Clear();
			}
			else if (NKCPacketObjectPool.IsManagedType(typeof(Dictionary<short, T>)))
			{
				data = (Dictionary<short, T>)NKCPacketObjectPool.OpenObject(typeof(Dictionary<short, T>));
			}
			else
			{
				data = new Dictionary<short, T>();
			}
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				short @short = this.GetShort();
				T value;
				this.GetMessage<T>(out value);
				data.Add(@short, value);
			}
		}

		// Token: 0x06009D39 RID: 40249 RVA: 0x00337AF8 File Offset: 0x00335CF8
		public void PutOrGet<T>(ref Dictionary<int, T> data) where T : ISerializable
		{
			if (data != null)
			{
				data.Clear();
			}
			else if (NKCPacketObjectPool.IsManagedType(typeof(Dictionary<int, T>)))
			{
				data = (Dictionary<int, T>)NKCPacketObjectPool.OpenObject(typeof(Dictionary<int, T>));
			}
			else
			{
				data = new Dictionary<int, T>();
			}
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				int @int = this.GetInt();
				T value;
				this.GetMessage<T>(out value);
				data.Add(@int, value);
			}
		}

		// Token: 0x06009D3A RID: 40250 RVA: 0x00337B70 File Offset: 0x00335D70
		public void PutOrGet<T>(ref Dictionary<long, T> data) where T : ISerializable
		{
			if (data != null)
			{
				data.Clear();
			}
			else if (NKCPacketObjectPool.IsManagedType(typeof(Dictionary<long, T>)))
			{
				data = (Dictionary<long, T>)NKCPacketObjectPool.OpenObject(typeof(Dictionary<long, T>));
			}
			else
			{
				data = new Dictionary<long, T>();
			}
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				long @long = this.GetLong();
				T value;
				this.GetMessage<T>(out value);
				data.Add(@long, value);
			}
		}

		// Token: 0x06009D3B RID: 40251 RVA: 0x00337BE8 File Offset: 0x00335DE8
		public void PutOrGet<T>(ref Dictionary<string, T> data) where T : ISerializable
		{
			if (data != null)
			{
				data.Clear();
			}
			else if (NKCPacketObjectPool.IsManagedType(typeof(Dictionary<string, T>)))
			{
				data = (Dictionary<string, T>)NKCPacketObjectPool.OpenObject(typeof(Dictionary<string, T>));
			}
			else
			{
				data = new Dictionary<string, T>();
			}
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				string @string = this.GetString();
				T value;
				this.GetMessage<T>(out value);
				data.Add(@string, value);
			}
		}

		// Token: 0x06009D3C RID: 40252 RVA: 0x00337C60 File Offset: 0x00335E60
		public void PutOrGetEnum<T>(ref T data) where T : Enum
		{
			int @int = this.GetInt();
			data = (T)((object)Enum.ToObject(typeof(T), @int));
		}

		// Token: 0x06009D3D RID: 40253 RVA: 0x00337C90 File Offset: 0x00335E90
		public void PutOrGetEnum<T>(ref List<T> data) where T : Enum
		{
			data = new List<T>();
			ushort @ushort = this.GetUshort();
			for (int i = 0; i < (int)@ushort; i++)
			{
				int @int = this.GetInt();
				T item = (T)((object)Enum.ToObject(typeof(T), @int));
				data.Add(item);
			}
		}

		// Token: 0x06009D3E RID: 40254 RVA: 0x00337CDC File Offset: 0x00335EDC
		public void PutOrGet<T>(ref T data) where T : ISerializable
		{
			this.GetMessage<T>(out data);
		}

		// Token: 0x06009D3F RID: 40255 RVA: 0x00337CE5 File Offset: 0x00335EE5
		public bool GetBool()
		{
			return this.reader.ReadBoolean();
		}

		// Token: 0x06009D40 RID: 40256 RVA: 0x00337CF2 File Offset: 0x00335EF2
		public sbyte GetSByte()
		{
			return this.reader.ReadSByte();
		}

		// Token: 0x06009D41 RID: 40257 RVA: 0x00337CFF File Offset: 0x00335EFF
		public byte GetByte()
		{
			return this.reader.ReadByte();
		}

		// Token: 0x06009D42 RID: 40258 RVA: 0x00337D0C File Offset: 0x00335F0C
		public short GetShort()
		{
			return (short)ZigZag.Decode32(this.ReadRawVarInt32());
		}

		// Token: 0x06009D43 RID: 40259 RVA: 0x00337D1A File Offset: 0x00335F1A
		public ushort GetUshort()
		{
			return (ushort)this.ReadRawVarInt32();
		}

		// Token: 0x06009D44 RID: 40260 RVA: 0x00337D23 File Offset: 0x00335F23
		public int GetInt()
		{
			return ZigZag.Decode32(this.ReadRawVarInt32());
		}

		// Token: 0x06009D45 RID: 40261 RVA: 0x00337D30 File Offset: 0x00335F30
		public uint GetUint()
		{
			return this.ReadRawVarInt32();
		}

		// Token: 0x06009D46 RID: 40262 RVA: 0x00337D38 File Offset: 0x00335F38
		public long GetLong()
		{
			return ZigZag.Decode64(this.ReadRawVarInt64());
		}

		// Token: 0x06009D47 RID: 40263 RVA: 0x00337D45 File Offset: 0x00335F45
		public ulong GetUlong()
		{
			return this.ReadRawVarInt64();
		}

		// Token: 0x06009D48 RID: 40264 RVA: 0x00337D4D File Offset: 0x00335F4D
		public float GetFloat()
		{
			return this.reader.ReadSingle();
		}

		// Token: 0x06009D49 RID: 40265 RVA: 0x00337D5A File Offset: 0x00335F5A
		public double GetDouble()
		{
			return this.reader.ReadDouble();
		}

		// Token: 0x06009D4A RID: 40266 RVA: 0x00337D67 File Offset: 0x00335F67
		public int GetRawInt()
		{
			return this.reader.ReadInt32();
		}

		// Token: 0x06009D4B RID: 40267 RVA: 0x00337D74 File Offset: 0x00335F74
		public uint GetRawUint()
		{
			return this.reader.ReadUInt32();
		}

		// Token: 0x06009D4C RID: 40268 RVA: 0x00337D81 File Offset: 0x00335F81
		public long GetRawLong()
		{
			return this.reader.ReadInt64();
		}

		// Token: 0x06009D4D RID: 40269 RVA: 0x00337D90 File Offset: 0x00335F90
		public string GetString()
		{
			short @short = this.GetShort();
			if (@short == -1)
			{
				return null;
			}
			byte[] bytes = this.reader.ReadBytes((int)@short);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06009D4E RID: 40270 RVA: 0x00337DC2 File Offset: 0x00335FC2
		public DateTime GetDateTime()
		{
			return DateTime.FromBinary(this.GetRawLong());
		}

		// Token: 0x06009D4F RID: 40271 RVA: 0x00337DCF File Offset: 0x00335FCF
		public TimeSpan GetTimeSpan()
		{
			return new TimeSpan(this.GetRawLong());
		}

		// Token: 0x06009D50 RID: 40272 RVA: 0x00337DDC File Offset: 0x00335FDC
		public void GetWithoutNullBit(ISerializable message)
		{
			message.Serialize(this);
		}

		// Token: 0x06009D51 RID: 40273 RVA: 0x00337DE8 File Offset: 0x00335FE8
		private void GetCollection<T>(in ICollection<T> collection, int count) where T : ISerializable
		{
			for (int i = 0; i < count; i++)
			{
				if (!this.GetBool())
				{
					collection.Add(default(T));
				}
				else
				{
					T item = (T)((object)NKCPacketObjectPool.OpenObject(typeof(T)));
					item.Serialize(this);
					collection.Add(item);
				}
			}
		}

		// Token: 0x06009D52 RID: 40274 RVA: 0x00337E46 File Offset: 0x00336046
		private void GetMessage<T>(out T message) where T : ISerializable
		{
			if (!this.GetBool())
			{
				message = default(T);
				return;
			}
			message = (T)((object)NKCPacketObjectPool.OpenObject(typeof(T)));
			message.Serialize(this);
		}

		// Token: 0x06009D53 RID: 40275 RVA: 0x00337E80 File Offset: 0x00336080
		private uint ReadRawVarInt32()
		{
			int i = 0;
			uint num = 0U;
			while (i < 32)
			{
				byte b = this.reader.ReadByte();
				num |= (uint)((uint)(b & 127) << i);
				if ((b & 128) == 0)
				{
					return num;
				}
				i += 7;
			}
			throw new Exception("[PacketReader] Malformed Varint32");
		}

		// Token: 0x06009D54 RID: 40276 RVA: 0x00337ECC File Offset: 0x003360CC
		private ulong ReadRawVarInt64()
		{
			int i = 0;
			ulong num = 0UL;
			while (i < 64)
			{
				byte b = this.reader.ReadByte();
				num |= (ulong)((ulong)((long)(b & 127)) << i);
				if ((b & 128) == 0)
				{
					return num;
				}
				i += 7;
			}
			throw new Exception("[PacketReader] Malformed Varint64");
		}

		// Token: 0x0400908C RID: 37004
		private readonly BinaryReader reader;
	}
}
