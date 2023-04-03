using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000EA6 RID: 3750
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_PROFILE_ACK)]
	public sealed class NKMPacket_EQUIP_PROFILE_ACK : ISerializable
	{
		// Token: 0x06009838 RID: 38968 RVA: 0x0032E22A File Offset: 0x0032C42A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<EquipProfileInfo>(ref this.equipProfileInfos);
		}

		// Token: 0x04008A99 RID: 35481
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A9A RID: 35482
		public List<EquipProfileInfo> equipProfileInfos = new List<EquipProfileInfo>();
	}
}
