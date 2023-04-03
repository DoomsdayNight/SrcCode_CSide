using System;

namespace Cs.Protocol
{
	// Token: 0x020010BF RID: 4287
	public interface ISerializable
	{
		// Token: 0x06009CF5 RID: 40181
		void Serialize(IPacketStream stream);
	}
}
