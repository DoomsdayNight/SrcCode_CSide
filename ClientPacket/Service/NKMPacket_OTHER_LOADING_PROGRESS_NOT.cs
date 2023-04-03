using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D46 RID: 3398
	[PacketId(ClientPacketId.kNKMPacket_OTHER_LOADING_PROGRESS_NOT)]
	public sealed class NKMPacket_OTHER_LOADING_PROGRESS_NOT : ISerializable
	{
		// Token: 0x06009589 RID: 38281 RVA: 0x0032A49A File Offset: 0x0032869A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.progress);
		}

		// Token: 0x0400873B RID: 34619
		public byte progress;
	}
}
