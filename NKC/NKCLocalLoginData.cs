using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200071F RID: 1823
	public sealed class NKCLocalLoginData : ISerializable
	{
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x0015DC2D File Offset: 0x0015BE2D
		// (set) Token: 0x06004868 RID: 18536 RVA: 0x0015DC35 File Offset: 0x0015BE35
		public bool IsFirstLoginToday { get; private set; }

		// Token: 0x06004869 RID: 18537 RVA: 0x0015DC3E File Offset: 0x0015BE3E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.LastLoginTime);
			stream.PutOrGet(ref this.m_hsPlayedCutin);
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x0015DC58 File Offset: 0x0015BE58
		public static NKCLocalLoginData LoadLastLoginData()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			string @string = PlayerPrefs.GetString("PREF_LAST_LOGIN_DATA" + nkmuserData.m_UserUID.ToString(), null);
			NKCLocalLoginData nkclocalLoginData = new NKCLocalLoginData();
			if (string.IsNullOrEmpty(@string) || !nkclocalLoginData.FromBase64(@string))
			{
				nkclocalLoginData.LastLoginTime = DateTime.MinValue;
				nkclocalLoginData.m_hsPlayedCutin = new HashSet<int>();
			}
			nkclocalLoginData.IsFirstLoginToday = (nkclocalLoginData.LastLoginTime.Date < DateTime.UtcNow.Date);
			if (nkclocalLoginData.IsFirstLoginToday)
			{
				nkclocalLoginData.m_hsPlayedCutin.Clear();
			}
			Debug.Log(string.Format("Last Login : {0}, TodayFirstLogin {1}", nkclocalLoginData.LastLoginTime, nkclocalLoginData.IsFirstLoginToday));
			return nkclocalLoginData;
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x0015DD10 File Offset: 0x0015BF10
		public void SaveLastLoginData()
		{
			this.LastLoginTime = DateTime.UtcNow;
			string value = this.ToBase64<NKCLocalLoginData>();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			PlayerPrefs.SetString("PREF_LAST_LOGIN_DATA" + nkmuserData.m_UserUID.ToString(), value);
			PlayerPrefs.Save();
		}

		// Token: 0x04003839 RID: 14393
		public const string PREF_LAST_LOGIN_DATA = "PREF_LAST_LOGIN_DATA";

		// Token: 0x0400383B RID: 14395
		public DateTime LastLoginTime;

		// Token: 0x0400383C RID: 14396
		public HashSet<int> m_hsPlayedCutin = new HashSet<int>();
	}
}
