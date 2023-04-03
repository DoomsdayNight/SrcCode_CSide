using System;
using Cs.Engine.Core;

namespace Cs.Engine.Network.Buffer.Detail
{
	// Token: 0x020010B3 RID: 4275
	internal sealed class TailBuffer
	{
		// Token: 0x06009C80 RID: 40064 RVA: 0x00335E7B File Offset: 0x0033407B
		private TailBuffer()
		{
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06009C81 RID: 40065 RVA: 0x00335E93 File Offset: 0x00334093
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06009C82 RID: 40066 RVA: 0x00335E9B File Offset: 0x0033409B
		public int Offset
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06009C83 RID: 40067 RVA: 0x00335EA3 File Offset: 0x003340A3
		public bool IsFull
		{
			get
			{
				return this.data.Length == this.size;
			}
		}

		// Token: 0x06009C84 RID: 40068 RVA: 0x00335EB5 File Offset: 0x003340B5
		public static TailBuffer Create()
		{
			return TailBuffer.pool.CreateInstance();
		}

		// Token: 0x06009C85 RID: 40069 RVA: 0x00335EC1 File Offset: 0x003340C1
		public static TailBuffer Create(byte[] data, int offset, int size)
		{
			TailBuffer tailBuffer = TailBuffer.pool.CreateInstance();
			tailBuffer.FillData(data, offset, size);
			return tailBuffer;
		}

		// Token: 0x06009C86 RID: 40070 RVA: 0x00335ED8 File Offset: 0x003340D8
		public int AddData(byte[] data, int offset, int size)
		{
			int num = Math.Min(this.data.Length - this.size, size);
			Buffer.BlockCopy(data, offset, this.data, this.size, num);
			this.size += num;
			return num;
		}

		// Token: 0x06009C87 RID: 40071 RVA: 0x00335F1E File Offset: 0x0033411E
		public void ToRecycleBin()
		{
			this.size = 0;
			TailBuffer.pool.ToRecycleBin(this);
		}

		// Token: 0x06009C88 RID: 40072 RVA: 0x00335F32 File Offset: 0x00334132
		private void FillData(byte[] data, int offset, int size)
		{
			this.size = Math.Min(this.data.Length, size);
			Buffer.BlockCopy(data, offset, this.data, 0, this.size);
		}

		// Token: 0x04009067 RID: 36967
		private const int BufferSize = 4096;

		// Token: 0x04009068 RID: 36968
		private static SimpleObjectPool<TailBuffer> pool = new SimpleObjectPool<TailBuffer>(() => new TailBuffer());

		// Token: 0x04009069 RID: 36969
		private readonly byte[] data = new byte[4096];

		// Token: 0x0400906A RID: 36970
		private int size;
	}
}
