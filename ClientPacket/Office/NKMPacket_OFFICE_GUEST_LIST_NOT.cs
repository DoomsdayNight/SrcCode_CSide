using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E11 RID: 3601
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_GUEST_LIST_NOT)]
	public sealed class NKMPacket_OFFICE_GUEST_LIST_NOT : ISerializable
	{
		// Token: 0x06009716 RID: 38678 RVA: 0x0032C7F0 File Offset: 0x0032A9F0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUserProfileData>(ref this.guestList);
		}

		// Token: 0x0400892A RID: 35114
		public List<NKMUserProfileData> guestList = new List<NKMUserProfileData>();
	}
}
