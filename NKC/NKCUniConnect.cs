using System;

namespace NKC
{
	// Token: 0x020006E1 RID: 1761
	public class NKCUniConnect
	{
		// Token: 0x06003D79 RID: 15737 RVA: 0x0013C7EE File Offset: 0x0013A9EE
		public static void DisconnectTest()
		{
			NKCScenManager.GetScenManager().GetConnectLogin().SimulateDisconnect();
			NKCScenManager.GetScenManager().GetConnectGame().SimulateDisconnect();
		}
	}
}
