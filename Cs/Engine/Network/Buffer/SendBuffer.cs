using System;
using System.Collections.Generic;
using System.IO;
using Cs.Engine.Network.Buffer.Detail;

namespace Cs.Engine.Network.Buffer
{
	// Token: 0x020010AF RID: 4271
	public sealed class SendBuffer
	{
		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06009C50 RID: 40016 RVA: 0x003354E2 File Offset: 0x003336E2
		public byte[] Data
		{
			get
			{
				return this.headBuffer;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06009C51 RID: 40017 RVA: 0x003354EA File Offset: 0x003336EA
		public int HeadOffset
		{
			get
			{
				return this.headOffset;
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06009C52 RID: 40018 RVA: 0x003354F2 File Offset: 0x003336F2
		public bool HasData
		{
			get
			{
				return this.headOffset > 0 || this.tailBuffers.Count > 0;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06009C53 RID: 40019 RVA: 0x0033550D File Offset: 0x0033370D
		internal int TailCount
		{
			get
			{
				return this.tailBuffers.Count;
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06009C54 RID: 40020 RVA: 0x0033551A File Offset: 0x0033371A
		private int HeadReservedSize
		{
			get
			{
				return this.headBuffer.Length - this.headOffset;
			}
		}

		// Token: 0x06009C55 RID: 40021 RVA: 0x0033552C File Offset: 0x0033372C
		public int CalcTotalSize()
		{
			int num = this.headOffset;
			foreach (TailBuffer tailBuffer in this.tailBuffers)
			{
				num += tailBuffer.Offset;
			}
			return num;
		}

		// Token: 0x06009C56 RID: 40022 RVA: 0x0033558C File Offset: 0x0033378C
		public BinaryWriter GetWriter()
		{
			return new BinaryWriter(new SendStream(this));
		}

		// Token: 0x06009C57 RID: 40023 RVA: 0x0033559C File Offset: 0x0033379C
		public void Consume(int size)
		{
			if (this.headOffset < size)
			{
				throw new ArgumentException(string.Format("this.offset:{0} size:{1}", this.headOffset, size));
			}
			this.headOffset -= size;
			if (this.headOffset > 0)
			{
				Buffer.BlockCopy(this.headBuffer, size, this.headBuffer, 0, this.headOffset);
			}
			this.TryConsumeTailBuffers();
		}

		// Token: 0x06009C58 RID: 40024 RVA: 0x0033560C File Offset: 0x0033380C
		public byte[] Flush()
		{
			byte[] array = new byte[this.CalcTotalSize()];
			int num = 0;
			Buffer.BlockCopy(this.headBuffer, 0, array, num, this.HeadOffset);
			num += this.HeadOffset;
			this.headOffset = 0;
			foreach (TailBuffer tailBuffer in this.tailBuffers)
			{
				Buffer.BlockCopy(tailBuffer.Data, 0, array, num, tailBuffer.Offset);
				num += tailBuffer.Offset;
				tailBuffer.ToRecycleBin();
			}
			this.tailBuffers.Clear();
			return array;
		}

		// Token: 0x06009C59 RID: 40025 RVA: 0x003356BC File Offset: 0x003338BC
		public void Absorb(ZeroCopyBuffer zeroCopyBuffer)
		{
			foreach (TailBuffer value in zeroCopyBuffer.Move())
			{
				this.tailBuffers.AddLast(value);
			}
			this.TryConsumeTailBuffers();
		}

		// Token: 0x06009C5A RID: 40026 RVA: 0x003356F8 File Offset: 0x003338F8
		internal void Write(byte[] buffer, int offset, int count)
		{
			if (this.tailBuffers.Count == 0 && this.HeadReservedSize > 0)
			{
				int num = Math.Min(this.HeadReservedSize, count);
				Buffer.BlockCopy(buffer, offset, this.headBuffer, this.headOffset, num);
				this.headOffset += num;
				offset += num;
				count -= num;
			}
			if (count == 0)
			{
				return;
			}
			TailBuffer tailBuffer = null;
			if (this.tailBuffers.Count > 0)
			{
				tailBuffer = this.tailBuffers.Last.Value;
				if (!tailBuffer.IsFull)
				{
					int num2 = tailBuffer.AddData(buffer, offset, count);
					offset += num2;
					count -= num2;
				}
			}
			while (count > 0)
			{
				if (tailBuffer != null && !tailBuffer.IsFull)
				{
					throw new Exception(string.Format("memory offset error. lastBuffer.Offset:{0}", tailBuffer.Offset));
				}
				TailBuffer tailBuffer2 = TailBuffer.Create(buffer, offset, count);
				this.tailBuffers.AddLast(tailBuffer2);
				offset += tailBuffer2.Offset;
				count -= tailBuffer2.Offset;
			}
		}

		// Token: 0x06009C5B RID: 40027 RVA: 0x003357EC File Offset: 0x003339EC
		internal void Clear()
		{
			this.headOffset = 0;
			foreach (TailBuffer tailBuffer in this.tailBuffers)
			{
				tailBuffer.ToRecycleBin();
			}
			this.tailBuffers.Clear();
		}

		// Token: 0x06009C5C RID: 40028 RVA: 0x00335850 File Offset: 0x00333A50
		private void TryConsumeTailBuffers()
		{
			while (this.tailBuffers.Count > 0)
			{
				TailBuffer value = this.tailBuffers.First.Value;
				if (this.HeadReservedSize < value.Offset)
				{
					break;
				}
				Buffer.BlockCopy(value.Data, 0, this.headBuffer, this.headOffset, value.Offset);
				this.headOffset += value.Offset;
				value.ToRecycleBin();
				this.tailBuffers.RemoveFirst();
			}
		}

		// Token: 0x04009060 RID: 36960
		private readonly byte[] headBuffer = new byte[4096];

		// Token: 0x04009061 RID: 36961
		private readonly LinkedList<TailBuffer> tailBuffers = new LinkedList<TailBuffer>();

		// Token: 0x04009062 RID: 36962
		private int headOffset;
	}
}
