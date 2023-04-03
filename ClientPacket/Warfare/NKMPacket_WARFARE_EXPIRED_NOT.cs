using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000CA5 RID: 3237
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_EXPIRED_NOT)]
	public sealed class NKMPacket_WARFARE_EXPIRED_NOT : ISerializable
	{
		// Token: 0x06009447 RID: 37959 RVA: 0x00328739 File Offset: 0x00326939
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
		}

		// Token: 0x0400859A RID: 34202
		public int stageId;
	}
}
