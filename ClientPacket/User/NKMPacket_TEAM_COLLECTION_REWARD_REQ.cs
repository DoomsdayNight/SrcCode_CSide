using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD3 RID: 3283
	[PacketId(ClientPacketId.kNKMPacket_TEAM_COLLECTION_REWARD_REQ)]
	public sealed class NKMPacket_TEAM_COLLECTION_REWARD_REQ : ISerializable
	{
		// Token: 0x060094A3 RID: 38051 RVA: 0x00329071 File Offset: 0x00327271
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.teamID);
		}

		// Token: 0x04008625 RID: 34341
		public int teamID;
	}
}
