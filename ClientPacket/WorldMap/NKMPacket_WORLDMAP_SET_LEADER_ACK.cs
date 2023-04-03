using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C7A RID: 3194
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_SET_LEADER_ACK)]
	public sealed class NKMPacket_WORLDMAP_SET_LEADER_ACK : ISerializable
	{
		// Token: 0x060093F3 RID: 37875 RVA: 0x00327D27 File Offset: 0x00325F27
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.leaderUID);
		}

		// Token: 0x040084FD RID: 34045
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040084FE RID: 34046
		public int cityID;

		// Token: 0x040084FF RID: 34047
		public long leaderUID;
	}
}
