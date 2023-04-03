using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C7E RID: 3198
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_CITY_MISSION_CANCEL_ACK)]
	public sealed class NKMPacket_WORLDMAP_CITY_MISSION_CANCEL_ACK : ISerializable
	{
		// Token: 0x060093FB RID: 37883 RVA: 0x00327DD3 File Offset: 0x00325FD3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.cityID);
		}

		// Token: 0x04008508 RID: 34056
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008509 RID: 34057
		public int cityID;
	}
}
