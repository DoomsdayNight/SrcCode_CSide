using System;
using System.IO;
using System.Reflection;
using Cs.Engine;
using Cs.Engine.Network.Buffer;
using Cs.Engine.Network.Buffer.Detail;
using Cs.Engine.Util;
using Cs.Logging;

namespace Cs.Protocol
{
	// Token: 0x020010C0 RID: 4288
	public struct Packet
	{
		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06009CF6 RID: 40182 RVA: 0x00336B96 File Offset: 0x00334D96
		public ushort PacketId
		{
			get
			{
				return this.packetId;
			}
		}

		// Token: 0x06009CF7 RID: 40183 RVA: 0x00336BA0 File Offset: 0x00334DA0
		public static Packet? Pack(ISerializable data, long sequence)
		{
			Packet packet = new Packet
			{
				sequence = sequence,
				packetId = PacketController.Instance.GetId(data)
			};
			if (packet.packetId == 65535)
			{
				Log.Error(MethodBase.GetCurrentMethod().Name + " invalid data", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/Packet.cs", 36);
				return null;
			}
			packet.buffer = PacketWriter.ToBufferWithoutNullBit(data);
			if (packet.buffer.CalcTotalSize() > 1024)
			{
				packet.buffer.Lz4Compress();
				packet.compressed = true;
			}
			else
			{
				packet.buffer.Encrypt();
			}
			packet.totalLength = packet.CalcTotalLength();
			return new Packet?(packet);
		}

		// Token: 0x06009CF8 RID: 40184 RVA: 0x00336C5C File Offset: 0x00334E5C
		internal static bool ProcessRecv(MemoryPipe pipe, RecvController recvController)
		{
			int num = 0;
			Stream readStream = pipe.GetReadStream();
			using (BinaryReader binaryReader = new BinaryReader(readStream))
			{
				using (PacketReader packetReader = new PacketReader(binaryReader))
				{
					while (pipe.Length >= (long)(num + 12))
					{
						readStream.Seek((long)num, SeekOrigin.Begin);
						if (binaryReader.ReadUInt32() != 2864434397U)
						{
							Log.Error("invalid head fence.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/Packet.cs", 73);
							return false;
						}
						int num2 = binaryReader.ReadInt32();
						if (num2 <= 12)
						{
							Log.Error(string.Format("invalid packet length:{0} headerSize:{1}", num2, 12), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/Packet.cs", 80);
							return false;
						}
						if (pipe.Length < (long)(num + num2))
						{
							break;
						}
						packetReader.GetLong();
						ushort @ushort = packetReader.GetUshort();
						bool @bool = packetReader.GetBool();
						int num3 = (int)readStream.Position - num;
						int num4 = num2 - num3;
						readStream.Seek((long)(num + num2 - 4), SeekOrigin.Begin);
						if (binaryReader.ReadUInt32() != 287454020U)
						{
							Log.Error("invalid tail fence. packetId:" + PacketController.Instance.GetIdStr(@ushort), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/Packet.cs", 100);
							return false;
						}
						num += num3;
						readStream.Seek((long)num, SeekOrigin.Begin);
						ISerializable serializable = Packet.Extract(packetReader, @ushort, @bool);
						if (serializable == null)
						{
							return false;
						}
						recvController.Enqueue(serializable, @ushort);
						num += num4;
					}
				}
			}
			pipe.Adavnce((long)num);
			return true;
		}

		// Token: 0x06009CF9 RID: 40185 RVA: 0x00336E08 File Offset: 0x00335008
		public static ISerializable Extract(PacketReader reader, ushort packetId, bool compressed)
		{
			ISerializable serializable = PacketController.Instance.Create(packetId);
			if (serializable == null)
			{
				Log.Error("deserializing failed. id:" + PacketController.Instance.GetIdStr(packetId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/Packet.cs", 127);
				return null;
			}
			ISerializable result;
			try
			{
				byte[] array = null;
				reader.PutOrGet(ref array);
				if (compressed)
				{
					ZeroCopyBuffer zeroCopyBuffer = Lz4Util.Decompress(array);
					using (zeroCopyBuffer.Hold())
					{
						using (PacketReader packetReader = new PacketReader(zeroCopyBuffer.GetReader()))
						{
							packetReader.GetWithoutNullBit(serializable);
						}
						goto IL_A4;
					}
				}
				Crypto.Encrypt(array, array.Length);
				using (PacketReader packetReader2 = new PacketReader(array))
				{
					packetReader2.GetWithoutNullBit(serializable);
				}
				IL_A4:
				result = serializable;
			}
			catch (Exception ex)
			{
				Log.Error("exception:" + ex.Message + " id:" + PacketController.Instance.GetIdStr(packetId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/COMMON/Cs.Protocol/Packet.cs", 158);
				result = null;
			}
			return result;
		}

		// Token: 0x06009CFA RID: 40186 RVA: 0x00336F2C File Offset: 0x0033512C
		public void WriteTo(SendBuffer sendBuffer)
		{
			using (BinaryWriter writer = sendBuffer.GetWriter())
			{
				using (PacketWriter packetWriter = new PacketWriter(writer))
				{
					packetWriter.PutRawUint(2864434397U);
					packetWriter.PutRawInt(this.totalLength);
					packetWriter.PutOrGet(ref this.sequence);
					packetWriter.PutOrGet(ref this.packetId);
					packetWriter.PutOrGet(ref this.compressed);
					packetWriter.PutInt(this.buffer.CalcTotalSize());
					sendBuffer.Absorb(this.buffer);
					packetWriter.PutRawUint(287454020U);
				}
			}
		}

		// Token: 0x06009CFB RID: 40187 RVA: 0x00336FDC File Offset: 0x003351DC
		private int CalcTotalLength()
		{
			PacketSizeChecker packetSizeChecker = new PacketSizeChecker();
			packetSizeChecker.CheckRawUint32(2864434397U);
			packetSizeChecker.CheckRawInt32(this.totalLength);
			packetSizeChecker.PutOrGet(ref this.sequence);
			packetSizeChecker.PutOrGet(ref this.packetId);
			packetSizeChecker.PutOrGet(ref this.compressed);
			packetSizeChecker.PutOrGet(this.buffer);
			packetSizeChecker.CheckRawUint32(287454020U);
			return packetSizeChecker.Size;
		}

		// Token: 0x0400907C RID: 36988
		private const uint HeadFence = 2864434397U;

		// Token: 0x0400907D RID: 36989
		private const uint TailFence = 287454020U;

		// Token: 0x0400907E RID: 36990
		private const int CompressThreshold = 1024;

		// Token: 0x0400907F RID: 36991
		private const ushort MinHeaderSize = 12;

		// Token: 0x04009080 RID: 36992
		private long sequence;

		// Token: 0x04009081 RID: 36993
		private ushort packetId;

		// Token: 0x04009082 RID: 36994
		private bool compressed;

		// Token: 0x04009083 RID: 36995
		private ZeroCopyBuffer buffer;

		// Token: 0x04009084 RID: 36996
		private int totalLength;
	}
}
