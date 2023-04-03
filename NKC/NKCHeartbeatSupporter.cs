using System;
using System.Threading;
using ClientPacket.Service;
using Cs.Logging;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200068D RID: 1677
	public sealed class NKCHeartbeatSupporter
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x0011A26F File Offset: 0x0011846F
		public static NKCHeartbeatSupporter Instance { get; } = new NKCHeartbeatSupporter();

		// Token: 0x060036C5 RID: 14021 RVA: 0x0011A278 File Offset: 0x00118478
		public void Update()
		{
			if (!NKCHeartbeatSupporter.NeedToSupport())
			{
				return;
			}
			if (this.HeartBitThread == null)
			{
				Log.Info("[HeartBit] create thread", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCHeartbeatSupporter.cs", 29);
				this.HeartBitThread = new Thread(new ThreadStart(NKCHeartbeatSupporter.ThreadEntry));
				this.HeartBitThread.Start();
			}
			else if (!this.HeartBitThread.IsAlive)
			{
				Log.Warn("[HeartBit] thread is dead. create new thread.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCHeartbeatSupporter.cs", 35);
				this.HeartBitThread = new Thread(new ThreadStart(NKCHeartbeatSupporter.ThreadEntry));
				this.HeartBitThread.Start();
			}
			if (this.isActivated)
			{
				this.activationDeltaTime += Time.deltaTime;
				if (this.activationDeltaTime > 3f)
				{
					this.isActivated = false;
				}
			}
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x0011A338 File Offset: 0x00118538
		public void StartSupport()
		{
			this.isActivated = true;
			this.activationDeltaTime = 0f;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x0011A34C File Offset: 0x0011854C
		public void EndSupport()
		{
			this.isActivated = false;
			this.activationDeltaTime = 0f;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x0011A360 File Offset: 0x00118560
		private static bool NeedToSupport()
		{
			return NKCDefineManager.DEFINE_ANDROID() && (NKMContentsVersionManager.HasCountryTag(CountryTagType.CHN) || NKMContentsVersionManager.HasCountryTag(CountryTagType.SEA) || NKMContentsVersionManager.HasCountryTag(CountryTagType.TWN));
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x0011A384 File Offset: 0x00118584
		private static void ThreadEntry()
		{
			try
			{
				NKMPacket_HEART_BIT_REQ nkmpacket_HEART_BIT_REQ = new NKMPacket_HEART_BIT_REQ();
				long num = 0L;
				for (;;)
				{
					Thread.Sleep(TimeSpan.FromSeconds(20.0));
					if (NKCHeartbeatSupporter.Instance.isActivated)
					{
						NKCConnectGame connectGame = NKCScenManager.GetScenManager().GetConnectGame();
						if (connectGame != null)
						{
							long sendSequence = connectGame.SendSequence;
							if (num == sendSequence)
							{
								nkmpacket_HEART_BIT_REQ.time = DateTime.Now.Ticks;
								connectGame.RawSend(nkmpacket_HEART_BIT_REQ);
							}
							else
							{
								num = sendSequence;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCHeartbeatSupporter.cs", 112);
			}
		}

		// Token: 0x040033F7 RID: 13303
		private const float DeactivateThresholdSec = 3f;

		// Token: 0x040033F8 RID: 13304
		private Thread HeartBitThread;

		// Token: 0x040033F9 RID: 13305
		private bool isActivated;

		// Token: 0x040033FA RID: 13306
		private float activationDeltaTime;
	}
}
