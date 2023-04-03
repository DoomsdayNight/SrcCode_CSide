using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D45 RID: 3397
	[PacketId(ClientPacketId.kNKMPacket_INFORM_MY_LOADING_PROGRESS_REQ)]
	public sealed class NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ : ISerializable
	{
		// Token: 0x06009587 RID: 38279 RVA: 0x0032A484 File Offset: 0x00328684
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.progress);
		}

		// Token: 0x0400873A RID: 34618
		public byte progress;
	}
}
