using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E63 RID: 3683
	[PacketId(ClientPacketId.kNKMPacket_FAVORITES_STAGE_DELETE_REQ)]
	public sealed class NKMPacket_FAVORITES_STAGE_DELETE_REQ : ISerializable
	{
		// Token: 0x060097B6 RID: 38838 RVA: 0x0032D570 File Offset: 0x0032B770
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
		}

		// Token: 0x040089E3 RID: 35299
		public int stageId;
	}
}
