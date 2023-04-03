using System;
using System.IO;
using Cs.Engine.Network.Buffer;
using Cs.Engine.Network.Buffer.Detail;
using LZ4;

namespace Cs.Engine.Util
{
	// Token: 0x020010A7 RID: 4263
	public static class Lz4Util
	{
		// Token: 0x06009C17 RID: 39959 RVA: 0x00334554 File Offset: 0x00332754
		public static byte[] Compress(byte[] source)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (LZ4Stream lz4Stream = new LZ4Stream(memoryStream, LZ4StreamMode.Compress, LZ4StreamFlags.None, 1048576))
				{
					lz4Stream.Write(source, 0, source.Length);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06009C18 RID: 39960 RVA: 0x003345BC File Offset: 0x003327BC
		public static ZeroCopyBuffer Decompress(byte[] source)
		{
			ZeroCopyBuffer zeroCopyBuffer = new ZeroCopyBuffer();
			using (ZeroCopyOutputStream zeroCopyOutputStream = new ZeroCopyOutputStream(zeroCopyBuffer))
			{
				using (LZ4Stream lz4Stream = new LZ4Stream(new MemoryStream(source), LZ4StreamMode.Decompress, LZ4StreamFlags.None, 1048576))
				{
					byte[] array = new byte[4096];
					int count;
					while ((count = lz4Stream.Read(array, 0, array.Length)) != 0)
					{
						zeroCopyOutputStream.Write(array, 0, count);
					}
				}
			}
			return zeroCopyBuffer;
		}

		// Token: 0x06009C19 RID: 39961 RVA: 0x00334648 File Offset: 0x00332848
		public static ZeroCopyBuffer Decompress(Stream source)
		{
			ZeroCopyBuffer zeroCopyBuffer = new ZeroCopyBuffer();
			ZeroCopyBuffer result;
			using (Stream outputStream = zeroCopyBuffer.GetOutputStream())
			{
				using (LZ4Stream lz4Stream = new LZ4Stream(source, LZ4StreamMode.Decompress, LZ4StreamFlags.None, 1048576))
				{
					byte[] array = new byte[4096];
					int count;
					while ((count = lz4Stream.Read(array, 0, array.Length)) != 0)
					{
						outputStream.Write(array, 0, count);
					}
					result = zeroCopyBuffer;
				}
			}
			return result;
		}
	}
}
