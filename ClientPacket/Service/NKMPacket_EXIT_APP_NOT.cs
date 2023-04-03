using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D51 RID: 3409
	[PacketId(ClientPacketId.kNKMPacket_EXIT_APP_NOT)]
	public sealed class NKMPacket_EXIT_APP_NOT : ISerializable
	{
		// Token: 0x0600959F RID: 38303 RVA: 0x0032A597 File Offset: 0x00328797
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008746 RID: 34630
		public NKM_ERROR_CODE errorCode;
	}
}
