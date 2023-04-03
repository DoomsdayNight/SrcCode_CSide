using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E61 RID: 3681
	[PacketId(ClientPacketId.kNKMPacket_FAVORITES_STAGE_ADD_REQ)]
	public sealed class NKMPacket_FAVORITES_STAGE_ADD_REQ : ISerializable
	{
		// Token: 0x060097B2 RID: 38834 RVA: 0x0032D52D File Offset: 0x0032B72D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
		}

		// Token: 0x040089E0 RID: 35296
		public int stageId;
	}
}
