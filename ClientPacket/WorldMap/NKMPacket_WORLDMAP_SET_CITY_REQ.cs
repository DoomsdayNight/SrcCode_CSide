using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.WorldMap
{
	// Token: 0x02000C77 RID: 3191
	[PacketId(ClientPacketId.kNKMPacket_WORLDMAP_SET_CITY_REQ)]
	public sealed class NKMPacket_WORLDMAP_SET_CITY_REQ : ISerializable
	{
		// Token: 0x060093ED RID: 37869 RVA: 0x00327CAA File Offset: 0x00325EAA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.cityID);
			stream.PutOrGet(ref this.isCash);
		}

		// Token: 0x040084F6 RID: 34038
		public int cityID;

		// Token: 0x040084F7 RID: 34039
		public bool isCash;
	}
}
