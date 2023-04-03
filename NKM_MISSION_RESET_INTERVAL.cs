using System;
using Cs.GameLog.CountryDescription;

namespace NKM
{
	// Token: 0x02000435 RID: 1077
	public enum NKM_MISSION_RESET_INTERVAL
	{
		// Token: 0x04001CC7 RID: 7367
		[CountryDescription("�Ϸ���� ����", CountryCode.KOR)]
		ALWAYS,
		// Token: 0x04001CC8 RID: 7368
		[CountryDescription("���� �ݺ��̼�", CountryCode.KOR)]
		DAILY,
		// Token: 0x04001CC9 RID: 7369
		[CountryDescription("�ְ� �ݺ��̼�", CountryCode.KOR)]
		WEEKLY,
		// Token: 0x04001CCA RID: 7370
		[CountryDescription("���� �ݺ��̼�", CountryCode.KOR)]
		MONTHLY,
		// Token: 0x04001CCB RID: 7371
		[CountryDescription("�ݺ��ֱ� ����", CountryCode.KOR)]
		NONE
	}
}
