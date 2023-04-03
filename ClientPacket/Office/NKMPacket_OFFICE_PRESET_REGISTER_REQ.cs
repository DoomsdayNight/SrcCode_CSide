using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E1A RID: 3610
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_REGISTER_REQ)]
	public sealed class NKMPacket_OFFICE_PRESET_REGISTER_REQ : ISerializable
	{
		// Token: 0x06009728 RID: 38696 RVA: 0x0032C993 File Offset: 0x0032AB93
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.presetId);
		}

		// Token: 0x0400893F RID: 35135
		public int roomId;

		// Token: 0x04008940 RID: 35136
		public int presetId;
	}
}
