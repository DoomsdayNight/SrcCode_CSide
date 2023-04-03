using System;
using Cs.Protocol;

namespace ClientPacket.Account
{
	// Token: 0x02001068 RID: 4200
	public sealed class ZlongUserData : ISerializable
	{
		// Token: 0x06009B8D RID: 39821 RVA: 0x003333A2 File Offset: 0x003315A2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userId);
		}

		// Token: 0x04008F7F RID: 36735
		public string userId;
	}
}
