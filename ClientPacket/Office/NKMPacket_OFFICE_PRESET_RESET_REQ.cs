using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E22 RID: 3618
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_RESET_REQ)]
	public sealed class NKMPacket_OFFICE_PRESET_RESET_REQ : ISerializable
	{
		// Token: 0x06009738 RID: 38712 RVA: 0x0032CB0A File Offset: 0x0032AD0A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.presetId);
		}

		// Token: 0x04008953 RID: 35155
		public int presetId;
	}
}
