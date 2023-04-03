using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Raid
{
	// Token: 0x02000D6A RID: 3434
	[PacketId(ClientPacketId.kNKMPacket_RAID_POINT_REWARD_REQ)]
	public sealed class NKMPacket_RAID_POINT_REWARD_REQ : ISerializable
	{
		// Token: 0x060095CF RID: 38351 RVA: 0x0032ABAD File Offset: 0x00328DAD
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.raidPointReward);
		}

		// Token: 0x040087AA RID: 34730
		public int raidPointReward;
	}
}
