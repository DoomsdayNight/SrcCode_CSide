using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using Cs.Engine.Network.Buffer;
using Cs.Logging;
using Cs.Protocol;

namespace Cs.Engine.Network
{
	// Token: 0x020010AD RID: 4269
	internal sealed class SendController : IDisposable
	{
		// Token: 0x06009C42 RID: 40002 RVA: 0x003351F0 File Offset: 0x003333F0
		public SendController(Socket socket)
		{
			this.socket_ = socket;
			this.eventArgs_.SetBuffer(this.sendingBuffer_.Data, 0, this.sendingBuffer_.Data.Length);
			this.eventArgs_.Completed += this.OnSendCompleted;
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06009C43 RID: 40003 RVA: 0x00335266 File Offset: 0x00333466
		public int MessageCount
		{
			get
			{
				return this.messageCount_;
			}
		}

		// Token: 0x06009C44 RID: 40004 RVA: 0x0033526E File Offset: 0x0033346E
		public void Dispose()
		{
			this.eventArgs_.Dispose();
		}

		// Token: 0x06009C45 RID: 40005 RVA: 0x0033527C File Offset: 0x0033347C
		public void Push(Packet data)
		{
			this.sendQueue_.Enqueue(data);
			if (Interlocked.Increment(ref this.messageCount_) != 1)
			{
				return;
			}
			if (!this.socket_.Connected)
			{
				Log.Warn("[SendController] socket is not connected.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/SendController.cs", 46);
				return;
			}
			this.TryFillBuffer();
			if (!this.RequestSendAsync())
			{
				Log.Error("send data failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/SendController.cs", 53);
			}
		}

		// Token: 0x06009C46 RID: 40006 RVA: 0x003352E2 File Offset: 0x003334E2
		public void TryConsumeQueue()
		{
			if (this.sendQueue_.Count == 0)
			{
				return;
			}
			this.TryFillBuffer();
			if (!this.RequestSendAsync())
			{
				Log.Error("send data failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/SendController.cs", 67);
			}
		}

		// Token: 0x06009C47 RID: 40007 RVA: 0x00335314 File Offset: 0x00333514
		private void TryFillBuffer()
		{
			Packet packet;
			while (this.sendQueue_.TryDequeue(out packet))
			{
				this.sendingMessageCount_++;
				packet.WriteTo(this.sendingBuffer_);
			}
		}

		// Token: 0x06009C48 RID: 40008 RVA: 0x0033534D File Offset: 0x0033354D
		private bool RequestSendAsync()
		{
			this.eventArgs_.SetBuffer(0, this.sendingBuffer_.HeadOffset);
			return this.socket_.SendAsync(this.eventArgs_);
		}

		// Token: 0x06009C49 RID: 40009 RVA: 0x00335378 File Offset: 0x00333578
		private void OnSendCompleted(object sender, SocketAsyncEventArgs arg)
		{
			this.sendingBuffer_.Consume(arg.BytesTransferred);
			if (!this.sendingBuffer_.HasData)
			{
				int num = this.sendingMessageCount_;
				this.sendingMessageCount_ = 0;
				if (Interlocked.Add(ref this.messageCount_, -num) == 0)
				{
					return;
				}
			}
			this.TryFillBuffer();
			if (!this.RequestSendAsync())
			{
				Log.Error("send data failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/SendController.cs", 106);
			}
		}

		// Token: 0x04009057 RID: 36951
		private readonly Socket socket_;

		// Token: 0x04009058 RID: 36952
		private SocketAsyncEventArgs eventArgs_ = new SocketAsyncEventArgs();

		// Token: 0x04009059 RID: 36953
		private ConcurrentQueue<Packet> sendQueue_ = new ConcurrentQueue<Packet>();

		// Token: 0x0400905A RID: 36954
		private SendBuffer sendingBuffer_ = new SendBuffer();

		// Token: 0x0400905B RID: 36955
		private int messageCount_;

		// Token: 0x0400905C RID: 36956
		private int sendingMessageCount_;
	}
}
