using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E1C RID: 3612
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_PRESET_APPLY_REQ)]
	public sealed class NKMPacket_OFFICE_PRESET_APPLY_REQ : ISerializable
	{
		// Token: 0x0600972C RID: 38700 RVA: 0x0032C9E2 File Offset: 0x0032ABE2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.roomId);
			stream.PutOrGet(ref this.presetId);
		}

		// Token: 0x04008943 RID: 35139
		public int roomId;

		// Token: 0x04008944 RID: 35140
		public int presetId;
	}
}
