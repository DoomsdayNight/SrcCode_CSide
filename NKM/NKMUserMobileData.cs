using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000390 RID: 912
	public sealed class NKMUserMobileData : ISerializable
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x0005EA9C File Offset: 0x0005CC9C
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_MarketId);
			stream.PutOrGet(ref this.m_Country);
			stream.PutOrGet(ref this.m_Language);
			stream.PutOrGet(ref this.m_AuthPlatform);
			stream.PutOrGet(ref this.m_Platform);
			stream.PutOrGet(ref this.m_OsVersion);
			stream.PutOrGet(ref this.m_AdId);
			stream.PutOrGet(ref this.m_ClientVersion);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0005EB0C File Offset: 0x0005CD0C
		public short GetMarketID()
		{
			string marketId = this.m_MarketId;
			if (marketId != null)
			{
				if (marketId == "Google Play Store")
				{
					return 1;
				}
				if (marketId == "Apple App Store")
				{
					return 2;
				}
				if (marketId == "One Store")
				{
					return 3;
				}
				if (marketId == "WINDOWS")
				{
					return 200;
				}
			}
			return 0;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0005EB66 File Offset: 0x0005CD66
		public static string GetMarketStr(short index)
		{
			switch (index)
			{
			case 1:
				return "Google Play Store";
			case 2:
				return "Apple App Store";
			case 3:
				return "One Store";
			default:
				if (index != 200)
				{
					return "Unknown";
				}
				return "WINDOWS";
			}
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0005EBA4 File Offset: 0x0005CDA4
		public ClientOsType GetOsType()
		{
			string marketId = this.m_MarketId;
			if (marketId != null)
			{
				if (marketId == "Google Play Store")
				{
					return ClientOsType.Android;
				}
				if (marketId == "Apple App Store")
				{
					return ClientOsType.iOS;
				}
				if (marketId == "One Store")
				{
					return ClientOsType.Android;
				}
				if (marketId == "Steam" || marketId == "WINDOWS")
				{
					return ClientOsType.Windows;
				}
			}
			return ClientOsType.Unknown;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0005EC08 File Offset: 0x0005CE08
		public ClientOsNxLogType GetNxLogOsType()
		{
			string marketId = this.m_MarketId;
			if (marketId != null)
			{
				if (marketId == "Google Play Store")
				{
					return ClientOsNxLogType.Android;
				}
				if (marketId == "Apple App Store")
				{
					return ClientOsNxLogType.iOS;
				}
				if (marketId == "One Store")
				{
					return ClientOsNxLogType.Android;
				}
				if (marketId == "WINDOWS")
				{
					return ClientOsNxLogType.Windows;
				}
			}
			return ClientOsNxLogType.Unknown;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0005EC60 File Offset: 0x0005CE60
		public string GetMarketStrID()
		{
			string marketId = this.m_MarketId;
			if (marketId != null)
			{
				if (marketId == "Google Play Store")
				{
					return "GPS";
				}
				if (marketId == "Apple App Store")
				{
					return "AAS";
				}
				if (marketId == "One Store")
				{
					return "ONE";
				}
			}
			return string.Empty;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0005ECB8 File Offset: 0x0005CEB8
		public byte GetAuthPlatformID()
		{
			byte result = 0;
			string authPlatform = this.m_AuthPlatform;
			if (authPlatform != null)
			{
				if (!(authPlatform == "NexonPlay"))
				{
					if (!(authPlatform == "KaKao"))
					{
						if (!(authPlatform == "NPA"))
						{
							if (!(authPlatform == "Nexon.com"))
							{
								if (authPlatform == "inner")
								{
									result = 5;
								}
							}
							else
							{
								result = 4;
							}
						}
						else
						{
							result = 3;
						}
					}
					else
					{
						result = 2;
					}
				}
				else
				{
					result = 1;
				}
			}
			return result;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0005ED28 File Offset: 0x0005CF28
		public string GetPlatformStrID()
		{
			string platform = this.m_Platform;
			if (platform != null)
			{
				if (platform == "Android")
				{
					return "A";
				}
				if (platform == "IPhonePlayer")
				{
					return "I";
				}
			}
			return string.Empty;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0005ED6C File Offset: 0x0005CF6C
		public string GetPlatformStrIDForNGSM()
		{
			string platform = this.m_Platform;
			if (platform != null)
			{
				if (platform == "Android")
				{
					return "AOS";
				}
				if (platform == "IPhonePlayer")
				{
					return "IOS";
				}
			}
			return string.Empty;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0005EDB0 File Offset: 0x0005CFB0
		public string GetCountryCode()
		{
			string country = this.m_Country;
			if (country != null && (country == "KO_KR" || country == "Korea"))
			{
				return "KR";
			}
			return this.m_Country;
		}

		// Token: 0x04000FD8 RID: 4056
		public const string NxPcMarketId = "WINDOWS";

		// Token: 0x04000FD9 RID: 4057
		public static readonly NKMUserMobileData DevDefault = new NKMUserMobileData
		{
			m_MarketId = "DevDefault",
			m_Country = "DevDefault",
			m_Language = "DevDefault",
			m_AuthPlatform = "DevDefault",
			m_Platform = "DevDefault",
			m_OsVersion = "DevDefault",
			m_AdId = "DevDefault",
			m_ClientVersion = "DevDefault"
		};

		// Token: 0x04000FDA RID: 4058
		public string m_MarketId;

		// Token: 0x04000FDB RID: 4059
		public string m_Country;

		// Token: 0x04000FDC RID: 4060
		public string m_Language;

		// Token: 0x04000FDD RID: 4061
		public string m_AuthPlatform;

		// Token: 0x04000FDE RID: 4062
		public string m_Platform;

		// Token: 0x04000FDF RID: 4063
		public string m_OsVersion;

		// Token: 0x04000FE0 RID: 4064
		public string m_AdId;

		// Token: 0x04000FE1 RID: 4065
		public string m_ClientVersion;
	}
}
