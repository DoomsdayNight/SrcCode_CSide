using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D65 RID: 3429
	[PacketId(ClientPacketId.kNKMPacket_RAID_RESULT_ACCEPT_REQ)]
	public sealed class NKMPacket_RAID_RESULT_ACCEPT_REQ : ISerializable
	{
		// Token: 0x060095C5 RID: 38341 RVA: 0x0032AAED File Offset: 0x00328CED
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidUID);
		}

		// Token: 0x040087A0 RID: 34720
		public long raidUID;
	}
}
