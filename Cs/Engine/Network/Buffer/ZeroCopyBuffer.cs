using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cs.Engine.Network.Buffer.Detail;
using Cs.Logging;
using LZ4;

namespace Cs.Engine.Network.Buffer
{
	// Token: 0x020010B0 RID: 4272
	public sealed class ZeroCopyBuffer
	{
		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06009C5E RID: 40030 RVA: 0x003358F1 File Offset: 0x00333AF1
		public int SegmentCount
		{
			get
			{
				return this.tailBuffers.Count;
			}
		}

		// Token: 0x06009C5F RID: 40031 RVA: 0x00335900 File Offset: 0x00333B00
		public int CalcTotalSize()
		{
			int num = 0;
			foreach (TailBuffer tailBuffer in this.tailBuffers)
			{
				num += tailBuffer.Offset;
			}
			return num;
		}

		// Token: 0x06009C60 RID: 40032 RVA: 0x00335958 File Offset: 0x00333B58
		public BinaryWriter GetWriter()
		{
			return new BinaryWriter(new ZeroCopyOutputStream(this));
		}

		// Token: 0x06009C61 RID: 40033 RVA: 0x00335965 File Offset: 0x00333B65
		public BinaryReader GetReader()
		{
			return new BinaryReader(new ZeroCopyInputStream(this));
		}

		// Token: 0x06009C62 RID: 40034 RVA: 0x00335972 File Offset: 0x00333B72
		public Stream GetOutputStream()
		{
			return new ZeroCopyOutputStream(this);
		}

		// Token: 0x06009C63 RID: 40035 RVA: 0x0033597A File Offset: 0x00333B7A
		public IDisposable Hold()
		{
			return new ZeroCopyBuffer.Cleaner(this);
		}

		// Token: 0x06009C64 RID: 40036 RVA: 0x00335984 File Offset: 0x00333B84
		public void Lz4Compress()
		{
			TailBuffer[] array = this.Move();
			using (ZeroCopyOutputStream zeroCopyOutputStream = new ZeroCopyOutputStream(this))
			{
				using (LZ4Stream lz4Stream = new LZ4Stream(zeroCopyOutputStream, LZ4StreamMode.Compress, LZ4StreamFlags.None, 1048576))
				{
					foreach (TailBuffer tailBuffer in array)
					{
						lz4Stream.Write(tailBuffer.Data, 0, tailBuffer.Offset);
						tailBuffer.ToRecycleBin();
					}
				}
			}
		}

		// Token: 0x06009C65 RID: 40037 RVA: 0x00335A18 File Offset: 0x00333C18
		public void Encrypt()
		{
			if (this.tailBuffers.Count > 1)
			{
				throw new Exception(string.Format("[ZeroCopyBuffer] encryption only support single tail. #tail:{0}", this.tailBuffers.Count));
			}
			if (this.tailBuffers.Count == 0)
			{
				return;
			}
			int num = 0;
			Crypto.Encrypt(this.last.Data, this.last.Offset, ref num);
		}

		// Token: 0x06009C66 RID: 40038 RVA: 0x00335A80 File Offset: 0x00333C80
		internal TailBuffer Peek()
		{
			return this.tailBuffers.Peek();
		}

		// Token: 0x06009C67 RID: 40039 RVA: 0x00335A8D File Offset: 0x00333C8D
		internal void PopHeadSegment()
		{
			this.tailBuffers.Dequeue().ToRecycleBin();
			if (this.tailBuffers.Count == 0)
			{
				this.last = null;
			}
		}

		// Token: 0x06009C68 RID: 40040 RVA: 0x00335AB4 File Offset: 0x00333CB4
		public bool WriteToFile(string filePath, string fileName)
		{
			Log.Info("[WriteToFile] WriteToFile - " + filePath + " : " + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Buffer/ZeroCopyBuffer.cs", 83);
			if (!Directory.Exists(filePath))
			{
				Log.Info("[WriteToFile] CreateDirectory - " + filePath, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Buffer/ZeroCopyBuffer.cs", 86);
				Directory.CreateDirectory(filePath);
			}
			string text = Path.Combine(filePath, fileName);
			try
			{
				using (FileStream fileStream = File.OpenWrite(text))
				{
					Log.Info("[WriteToFile] OpenWrite - " + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Buffer/ZeroCopyBuffer.cs", 96);
					foreach (TailBuffer tailBuffer in this.tailBuffers)
					{
						fileStream.Write(tailBuffer.Data, 0, tailBuffer.Offset);
					}
					fileStream.Flush();
					Log.Info("[WriteToFile] Flush", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Buffer/ZeroCopyBuffer.cs", 104);
				}
				return true;
			}
			catch (Exception ex)
			{
				Log.Error("[WriteToFile] exception:" + ex.Message + " filePath:" + text, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Buffer/ZeroCopyBuffer.cs", 111);
			}
			return false;
		}

		// Token: 0x06009C69 RID: 40041 RVA: 0x00335BE8 File Offset: 0x00333DE8
		public Task<bool> WriteToFileAsync(string filePath, string fileName)
		{
			ZeroCopyBuffer.<WriteToFileAsync>d__14 <WriteToFileAsync>d__;
			<WriteToFileAsync>d__.<>4__this = this;
			<WriteToFileAsync>d__.filePath = filePath;
			<WriteToFileAsync>d__.fileName = fileName;
			<WriteToFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<WriteToFileAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <WriteToFileAsync>d__.<>t__builder;
			<>t__builder.Start<ZeroCopyBuffer.<WriteToFileAsync>d__14>(ref <WriteToFileAsync>d__);
			return <WriteToFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06009C6A RID: 40042 RVA: 0x00335C40 File Offset: 0x00333E40
		internal void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				if (this.last == null || this.last.IsFull)
				{
					this.last = TailBuffer.Create();
					this.tailBuffers.Enqueue(this.last);
				}
				int num = this.last.AddData(buffer, offset, count);
				offset += num;
				count -= num;
			}
		}

		// Token: 0x06009C6B RID: 40043 RVA: 0x00335C9D File Offset: 0x00333E9D
		internal TailBuffer[] Move()
		{
			TailBuffer[] result = this.tailBuffers.ToArray();
			this.tailBuffers.Clear();
			this.last = null;
			return result;
		}

		// Token: 0x06009C6C RID: 40044 RVA: 0x00335CBC File Offset: 0x00333EBC
		internal TailBuffer[] GetView()
		{
			return this.tailBuffers.ToArray();
		}

		// Token: 0x06009C6D RID: 40045 RVA: 0x00335CCC File Offset: 0x00333ECC
		public string ToBase64()
		{
			if (this.tailBuffers.Count > 1)
			{
				throw new Exception(string.Format("[ZeroCopyBuffer] ToBase64 only support single tail. #tail:{0}", this.tailBuffers.Count));
			}
			return Convert.ToBase64String(this.last.Data, 0, this.last.Offset);
		}

		// Token: 0x04009063 RID: 36963
		private readonly Queue<TailBuffer> tailBuffers = new Queue<TailBuffer>();

		// Token: 0x04009064 RID: 36964
		private TailBuffer last;

		// Token: 0x02001A35 RID: 6709
		private sealed class Cleaner : IDisposable
		{
			// Token: 0x0600BB64 RID: 47972 RVA: 0x0036F0BF File Offset: 0x0036D2BF
			public Cleaner(ZeroCopyBuffer buffer)
			{
				this.buffer = buffer;
			}

			// Token: 0x0600BB65 RID: 47973 RVA: 0x0036F0D0 File Offset: 0x0036D2D0
			public void Dispose()
			{
				foreach (TailBuffer tailBuffer in this.buffer.tailBuffers)
				{
					tailBuffer.ToRecycleBin();
				}
				this.buffer.tailBuffers.Clear();
				this.buffer.last = null;
			}

			// Token: 0x0400AE03 RID: 44547
			private readonly ZeroCopyBuffer buffer;
		}
	}
}
