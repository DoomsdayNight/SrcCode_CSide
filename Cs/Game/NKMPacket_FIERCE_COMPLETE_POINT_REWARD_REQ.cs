using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F5F RID: 3935
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_COMPLETE_POINT_REWARD_REQ)]
	public sealed class NKMPacket_FIERCE_COMPLETE_POINT_REWARD_REQ : ISerializable
	{
		// Token: 0x0600999E RID: 39326 RVA: 0x0033040C File Offset: 0x0032E60C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.fiercePointRewardId);
		}

		// Token: 0x04008CA5 RID: 36005
		public int fiercePointRewardId;
	}
}
