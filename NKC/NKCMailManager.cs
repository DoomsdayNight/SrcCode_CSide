using System;
using System.Collections.Generic;
using ClientPacket.User;
using NKC.UI;
using NKM;

namespace NKC
{
	// Token: 0x02000698 RID: 1688
	internal static class NKCMailManager
	{
		// Token: 0x06003784 RID: 14212 RVA: 0x0011DF23 File Offset: 0x0011C123
		public static int GetReceivedMailCount()
		{
			if (NKCMailManager.s_slstPostData == null)
			{
				return 0;
			}
			return NKCMailManager.s_slstPostData.Count;
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x0011DF38 File Offset: 0x0011C138
		public static int GetTotalMailCount()
		{
			return NKCMailManager.s_MailTotalCount;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x0011DF3F File Offset: 0x0011C13F
		public static NKMPostData GetMailByIndex(int index)
		{
			if (index < NKCMailManager.s_slstPostData.Count)
			{
				return NKCMailManager.s_slstPostData.Values[index];
			}
			return null;
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x0011DF60 File Offset: 0x0011C160
		public static NKMPostData GetMailByPostID(long postID)
		{
			NKMPostData result;
			if (NKCMailManager.s_slstPostData.TryGetValue(postID, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x0011DF7F File Offset: 0x0011C17F
		private static void NotifyToObservers()
		{
			if (NKCMailManager.dOnMailFlagChange != null)
			{
				NKCMailManager.dOnMailFlagChange(NKCMailManager.HasNewMail());
			}
			if (NKCMailManager.dOnMailCountChange != null)
			{
				NKCMailManager.dOnMailCountChange(NKCMailManager.s_MailTotalCount);
			}
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x0011DFAD File Offset: 0x0011C1AD
		public static bool HasNewMail()
		{
			return NKCMailManager.s_bServerHaveNewMail || NKCMailManager.GetTotalMailCount() > 0;
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x0011DFC0 File Offset: 0x0011C1C0
		private static void SetRefreshOnNextInterval()
		{
			NKCMailManager.s_bRefreshOnNextInterval = true;
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x0011DFC8 File Offset: 0x0011C1C8
		public static bool CanRefreshMail()
		{
			return NKCScenManager.CurrentUserData() != null && NKCScenManager.CurrentUserData().m_UserNickName != null && NKCMailManager.s_fRefreshMailTimer >= 5f;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x0011DFF0 File Offset: 0x0011C1F0
		public static bool CanGetNextMail()
		{
			return NKCMailManager.s_fNextMailTimer >= 1f && NKCMailManager.GetTotalMailCount() > NKCMailManager.GetReceivedMailCount();
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x0011E00C File Offset: 0x0011C20C
		public static void Update(float deltaTime)
		{
			if (NKCMailManager.s_fNextMailTimer <= 1f)
			{
				NKCMailManager.s_fNextMailTimer += deltaTime;
			}
			if (NKCMailManager.s_fRefreshMailTimer <= 5f)
			{
				NKCMailManager.s_fRefreshMailTimer += deltaTime;
				return;
			}
			if (NKCMailManager.s_bRefreshOnNextInterval)
			{
				NKCMailManager.RefreshMailList();
			}
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x0011E04B File Offset: 0x0011C24B
		public static void Cleanup()
		{
			NKCMailManager.s_fRefreshMailTimer = 5f;
			NKCMailManager.s_fNextMailTimer = 1f;
			NKCMailManager.s_slstPostData.Clear();
			NKCMailManager.s_MailTotalCount = 0;
			NKCMailManager.s_bServerHaveNewMail = false;
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x0011E078 File Offset: 0x0011C278
		public static void AddMail(List<NKMPostData> lstPostData, int newTotalCount)
		{
			if (newTotalCount >= 0)
			{
				NKCMailManager.s_MailTotalCount = newTotalCount;
			}
			foreach (NKMPostData nkmpostData in lstPostData)
			{
				NKCMailManager.s_slstPostData[nkmpostData.postIndex] = nkmpostData;
			}
			NKCMailManager.NotifyToObservers();
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x0011E0E0 File Offset: 0x0011C2E0
		private static long GetLastMailIndex()
		{
			if (NKCMailManager.s_slstPostData.Count != 0)
			{
				return NKCMailManager.s_slstPostData.Keys[NKCMailManager.s_slstPostData.Count - 1];
			}
			return 0L;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x0011E10C File Offset: 0x0011C30C
		public static IEnumerable<NKMPostData> GetPostList()
		{
			return NKCMailManager.s_slstPostData.Values;
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x0011E118 File Offset: 0x0011C318
		public static void OnPostReceive(NKMPacket_POST_RECEIVE_ACK sPacket)
		{
			if (sPacket.postCount >= 0)
			{
				NKCMailManager.s_MailTotalCount = sPacket.postCount;
			}
			if (sPacket.postIndex == 0L)
			{
				NKCMailManager.s_slstPostData.Clear();
				NKCMailManager.s_bServerHaveNewMail = false;
				if (sPacket.postCount != 0)
				{
					NKCMailManager.SetRefreshOnNextInterval();
				}
			}
			else
			{
				NKCMailManager.RemoveMail(sPacket.postIndex);
			}
			NKCMailManager.NotifyToObservers();
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0011E170 File Offset: 0x0011C370
		private static void RemoveMail(long index)
		{
			if (NKCMailManager.s_slstPostData.ContainsKey(index))
			{
				NKCMailManager.s_slstPostData.Remove(index);
			}
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0011E18C File Offset: 0x0011C38C
		public static void CheckAndRemoveExpiredMail()
		{
			List<long> list = new List<long>();
			foreach (KeyValuePair<long, NKMPostData> keyValuePair in NKCMailManager.s_slstPostData)
			{
				NKMPostData value = keyValuePair.Value;
				if (value.expirationDate < NKMConst.Post.UnlimitedExpirationUtcDate && NKCSynchronizedTime.IsFinished(value.expirationDate))
				{
					list.Add(value.postIndex);
				}
			}
			foreach (long index in list)
			{
				NKCMailManager.RemoveMail(index);
			}
			if (list.Count > 0)
			{
				NKCMailManager.NotifyToObservers();
			}
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0011E254 File Offset: 0x0011C454
		public static void RefreshMailList()
		{
			if (!NKCMailManager.CanRefreshMail())
			{
				return;
			}
			NKCMailManager.s_bRefreshOnNextInterval = false;
			NKCMailManager.s_fRefreshMailTimer = 0f;
			NKCMailManager.s_slstPostData.Clear();
			NKCMailManager.s_bServerHaveNewMail = false;
			NKCPacketSender.Send_NKMPacket_POST_LIST_REQ(0L);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x0011E285 File Offset: 0x0011C485
		public static void GetNextMail()
		{
			if (!NKCMailManager.CanGetNextMail())
			{
				return;
			}
			NKCMailManager.s_fNextMailTimer = 0f;
			NKCMailManager.s_bServerHaveNewMail = false;
			NKCPacketSender.Send_NKMPacket_POST_LIST_REQ(NKCMailManager.GetLastMailIndex());
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x0011E2A9 File Offset: 0x0011C4A9
		public static void OnNewMailNotify(int mailcount = 1)
		{
			NKCMailManager.s_bServerHaveNewMail = true;
			NKCMailManager.s_MailTotalCount += mailcount;
			if (NKCUIMail.IsInstanceOpen)
			{
				NKCMailManager.SetRefreshOnNextInterval();
			}
			NKCMailManager.NotifyToObservers();
		}

		// Token: 0x04003436 RID: 13366
		private static SortedList<long, NKMPostData> s_slstPostData = new SortedList<long, NKMPostData>(new NKCMailManager.LongComparer());

		// Token: 0x04003437 RID: 13367
		private static int s_MailTotalCount;

		// Token: 0x04003438 RID: 13368
		public static NKCMailManager.OnMailFlagChange dOnMailFlagChange;

		// Token: 0x04003439 RID: 13369
		public static NKCMailManager.OnMailCountChange dOnMailCountChange;

		// Token: 0x0400343A RID: 13370
		private static bool s_bServerHaveNewMail = false;

		// Token: 0x0400343B RID: 13371
		private const float MAIL_REFRESH_INTERVAL = 5f;

		// Token: 0x0400343C RID: 13372
		private static float s_fRefreshMailTimer = 5f;

		// Token: 0x0400343D RID: 13373
		private const float MAIL_NEXT_INTERVAL = 1f;

		// Token: 0x0400343E RID: 13374
		private static float s_fNextMailTimer = 1f;

		// Token: 0x0400343F RID: 13375
		private static bool s_bRefreshOnNextInterval = false;

		// Token: 0x02001360 RID: 4960
		private class LongComparer : IComparer<long>
		{
			// Token: 0x0600A5B8 RID: 42424 RVA: 0x00345E18 File Offset: 0x00344018
			public int Compare(long a, long b)
			{
				return b.CompareTo(a);
			}
		}

		// Token: 0x02001361 RID: 4961
		// (Invoke) Token: 0x0600A5BB RID: 42427
		public delegate void OnMailFlagChange(bool bHaveMail);

		// Token: 0x02001362 RID: 4962
		// (Invoke) Token: 0x0600A5BF RID: 42431
		public delegate void OnMailCountChange(int TotalCount);
	}
}
