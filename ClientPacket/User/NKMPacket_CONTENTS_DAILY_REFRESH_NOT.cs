using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC7 RID: 3271
	[PacketId(ClientPacketId.kNKMPacket_CONTENTS_DAILY_REFRESH_NOT)]
	public sealed class NKMPacket_CONTENTS_DAILY_REFRESH_NOT : ISerializable
	{
		// Token: 0x0600948B RID: 38027 RVA: 0x00328E04 File Offset: 0x00327004
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMItemMiscData>(ref this.refreshItemDataList);
		}

		// Token: 0x04008600 RID: 34304
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008601 RID: 34305
		public List<NKMItemMiscData> refreshItemDataList = new List<NKMItemMiscData>();
	}
}
