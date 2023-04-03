using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E4B RID: 3659
	[PacketId(ClientPacketId.kNKMPacket_STAGE_UNLOCK_REQ)]
	public sealed class NKMPacket_STAGE_UNLOCK_REQ : ISerializable
	{
		// Token: 0x06009786 RID: 38790 RVA: 0x0032D126 File Offset: 0x0032B326
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.stageId);
		}

		// Token: 0x040089AC RID: 35244
		public int stageId;
	}
}
