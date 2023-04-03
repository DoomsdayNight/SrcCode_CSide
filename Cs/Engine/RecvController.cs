using System;
using System.Collections.Concurrent;
using Cs.Engine.Network;
using Cs.Protocol;

namespace Cs.Engine
{
	// Token: 0x020010A4 RID: 4260
	internal sealed class RecvController
	{
		// Token: 0x06009C08 RID: 39944 RVA: 0x00334331 File Offset: 0x00332531
		public void Enqueue(ISerializable packet, ushort packetId)
		{
			this.queue_.Enqueue(new RecvController.Node(packet, packetId));
		}

		// Token: 0x06009C09 RID: 39945 RVA: 0x00334345 File Offset: 0x00332545
		public void RegisterHandler(Type containerType)
		{
			this.handlerMap_.RegisterHandler(containerType);
		}

		// Token: 0x06009C0A RID: 39946 RVA: 0x00334354 File Offset: 0x00332554
		public void ProcessResponses(Connection connection)
		{
			RecvController.Node node;
			while (this.queue_.TryDequeue(out node))
			{
				this.handlerMap_.Process(node.Message, node.PacketId, connection);
			}
		}

		// Token: 0x0400903B RID: 36923
		private readonly ConcurrentQueue<RecvController.Node> queue_ = new ConcurrentQueue<RecvController.Node>();

		// Token: 0x0400903C RID: 36924
		private HandlerMap handlerMap_ = new HandlerMap();

		// Token: 0x02001A2F RID: 6703
		private readonly struct Node
		{
			// Token: 0x0600BB47 RID: 47943 RVA: 0x0036EA32 File Offset: 0x0036CC32
			public Node(ISerializable message, ushort packetId)
			{
				this.Message = message;
				this.PacketId = packetId;
			}

			// Token: 0x170019F8 RID: 6648
			// (get) Token: 0x0600BB48 RID: 47944 RVA: 0x0036EA42 File Offset: 0x0036CC42
			public ISerializable Message { get; }

			// Token: 0x170019F9 RID: 6649
			// (get) Token: 0x0600BB49 RID: 47945 RVA: 0x0036EA4A File Offset: 0x0036CC4A
			public ushort PacketId { get; }
		}
	}
}
