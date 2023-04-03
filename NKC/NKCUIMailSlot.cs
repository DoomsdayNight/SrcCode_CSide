using System;
using System.Collections.Generic;
using NKC.Publisher;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B7 RID: 2487
	public class NKCUIMailSlot : MonoBehaviour
	{
		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x0600691A RID: 26906 RVA: 0x0021FA46 File Offset: 0x0021DC46
		// (set) Token: 0x0600691B RID: 26907 RVA: 0x0021FA4E File Offset: 0x0021DC4E
		public long Index { get; private set; }

		// Token: 0x0600691C RID: 26908 RVA: 0x0021FA58 File Offset: 0x0021DC58
		public void Init()
		{
			foreach (NKCUISlot nkcuislot in this.m_lstSlot)
			{
				nkcuislot.Init();
			}
			this.m_cbtnOpen.PointerClick.AddListener(new UnityAction(this.OnBtnOpen));
		}

		// Token: 0x0600691D RID: 26909 RVA: 0x0021FAC4 File Offset: 0x0021DCC4
		public void SetData(NKMPostData postData, NKCUIMailSlot.OnReceive onReceive, NKCUIMailSlot.OnOpen onOpen)
		{
			this.m_PostData = postData;
			this.Index = postData.postIndex;
			this.dOnReceive = onReceive;
			this.dOnOpen = onOpen;
			this.SetIcon(postData.postType);
			string @string = NKCStringTable.GetString(postData.title, true);
			this.m_lbTitle.text = NKCPublisherModule.Localization.GetTranslationIfJson(@string);
			this.m_lbContent.text = NKCUtilString.GetFinalMailContents(postData.contents);
			this.m_lbContent.text = NKCUtil.LabelLongTextCut(this.m_lbContent);
			this.SetSlot(postData.items);
			this.m_lbDate.text = NKMTime.UTCtoLocal(postData.sendDate, 0).ToString("yyyy-MM-dd");
			this.SetTimeLeft(postData);
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x0021FB84 File Offset: 0x0021DD84
		private void SetIcon(NKM_POST_TYPE postType)
		{
			if (postType == NKM_POST_TYPE.NORMAL)
			{
				this.m_imgMailType.sprite = this.m_spMailTypeNormal;
				return;
			}
			if (postType != NKM_POST_TYPE.ANNOUNCEMENT)
			{
				Debug.Log("Undefined mail type");
				return;
			}
			this.m_imgMailType.sprite = this.m_spMailTypeAnnouncement;
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x0021FBC0 File Offset: 0x0021DDC0
		private void SetSlot(List<NKMRewardInfo> lstPostItem)
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlot[i];
				if (i < lstPostItem.Count)
				{
					NKMRewardInfo nkmrewardInfo = lstPostItem[i];
					bool flag = NKCUIMailSlot.IsSlotVisible(nkmrewardInfo.rewardType, nkmrewardInfo.ID, nkmrewardInfo.Count);
					NKCUtil.SetGameobjectActive(nkcuislot, flag);
					if (flag)
					{
						nkcuislot.SetData(NKCUISlot.SlotData.MakePostItemData(nkmrewardInfo), false, null);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x0021FC3C File Offset: 0x0021DE3C
		private void SetTimeLeft(NKMPostData postData)
		{
			if (postData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objTimeLeft, false);
				NKCUtil.SetGameobjectActive(this.m_objTimeLeftShort, false);
				return;
			}
			if (postData.expirationDate >= NKMConst.Post.UnlimitedExpirationUtcDate)
			{
				NKCUtil.SetGameobjectActive(this.m_objTimeLeft, true);
				NKCUtil.SetGameobjectActive(this.m_objTimeLeftShort, false);
				this.m_lbTimeLeft.text = NKCUtilString.GET_STRING_TIME_NO_LIMIT;
				return;
			}
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(postData.expirationDate);
			if (timeLeft.TotalDays >= 1.0)
			{
				NKCUtil.SetGameobjectActive(this.m_objTimeLeft, true);
				NKCUtil.SetGameobjectActive(this.m_objTimeLeftShort, false);
				this.m_lbTimeLeft.text = string.Format(NKCUtilString.GET_STRING_TIME_DAY_HOUR_TWO_PARAM, timeLeft.Days, timeLeft.Hours);
				return;
			}
			if (timeLeft.TotalHours >= 1.0)
			{
				NKCUtil.SetGameobjectActive(this.m_objTimeLeft, true);
				NKCUtil.SetGameobjectActive(this.m_objTimeLeftShort, false);
				this.m_lbTimeLeft.text = string.Format(NKCUtilString.GET_STRING_TIME_HOUR_MINUTE_TWO_PARAM, timeLeft.Hours, timeLeft.Minutes);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objTimeLeft, false);
			NKCUtil.SetGameobjectActive(this.m_objTimeLeftShort, true);
			this.m_lbTimeLeftShort.text = string.Format(NKCUtilString.GET_STRING_TIME_MINUTE_ONE_PARAM, timeLeft.Minutes);
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x0021FD95 File Offset: 0x0021DF95
		private void OnBtnOpen()
		{
			if (this.dOnOpen != null)
			{
				this.dOnOpen((this.m_PostData != null) ? this.m_PostData : null);
			}
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x0021FDBB File Offset: 0x0021DFBB
		public void UpdateTime()
		{
			if (this.m_PostData.expirationDate < NKMConst.Post.UnlimitedExpirationUtcDate)
			{
				this.SetTimeLeft(this.m_PostData);
			}
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x0021FDE0 File Offset: 0x0021DFE0
		public void OnBtnReceive()
		{
			if (this.dOnReceive != null)
			{
				this.dOnReceive((this.m_PostData != null) ? this.m_PostData.postIndex : -1L);
			}
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x0021FE0C File Offset: 0x0021E00C
		public static bool IsSlotVisible(NKM_REWARD_TYPE rewardType, int rewardID, int count)
		{
			switch (rewardType)
			{
			case NKM_REWARD_TYPE.RT_NONE:
				return false;
			case NKM_REWARD_TYPE.RT_UNIT:
			case NKM_REWARD_TYPE.RT_SHIP:
				break;
			case NKM_REWARD_TYPE.RT_MISC:
				if (NKMItemManager.GetItemMiscTempletByID(rewardID) == null)
				{
					return false;
				}
				if (rewardID == 0 || count == 0)
				{
					return false;
				}
				return true;
			case NKM_REWARD_TYPE.RT_USER_EXP:
				goto IL_57;
			case NKM_REWARD_TYPE.RT_EQUIP:
				if (rewardID == 0)
				{
					return false;
				}
				if (NKMItemManager.GetEquipTemplet(rewardID) == null)
				{
					return false;
				}
				return true;
			default:
				if (rewardType != NKM_REWARD_TYPE.RT_OPERATOR)
				{
					goto IL_57;
				}
				break;
			}
			if (rewardID == 0)
			{
				return false;
			}
			if (NKMUnitManager.GetUnitTempletBase(rewardID) == null)
			{
				return false;
			}
			return true;
			IL_57:
			if (count == 0)
			{
				return false;
			}
			return true;
		}

		// Token: 0x040054E9 RID: 21737
		public Image m_imgMailType;

		// Token: 0x040054EA RID: 21738
		public Text m_lbTitle;

		// Token: 0x040054EB RID: 21739
		public Text m_lbContent;

		// Token: 0x040054EC RID: 21740
		public Text m_lbDate;

		// Token: 0x040054ED RID: 21741
		public List<NKCUISlot> m_lstSlot;

		// Token: 0x040054EE RID: 21742
		public GameObject m_objTimeLeft;

		// Token: 0x040054EF RID: 21743
		public Text m_lbTimeLeft;

		// Token: 0x040054F0 RID: 21744
		public GameObject m_objTimeLeftShort;

		// Token: 0x040054F1 RID: 21745
		public Text m_lbTimeLeftShort;

		// Token: 0x040054F2 RID: 21746
		public NKCUIComButton m_cbtnReceive;

		// Token: 0x040054F3 RID: 21747
		public NKCUIComStateButton m_cbtnOpen;

		// Token: 0x040054F4 RID: 21748
		public Sprite m_spMailTypeNormal;

		// Token: 0x040054F5 RID: 21749
		public Sprite m_spMailTypeAnnouncement;

		// Token: 0x040054F6 RID: 21750
		private NKMPostData m_PostData;

		// Token: 0x040054F7 RID: 21751
		private NKCUIMailSlot.OnReceive dOnReceive;

		// Token: 0x040054F8 RID: 21752
		private NKCUIMailSlot.OnOpen dOnOpen;

		// Token: 0x020016B0 RID: 5808
		// (Invoke) Token: 0x0600B100 RID: 45312
		public delegate void OnReceive(long index);

		// Token: 0x020016B1 RID: 5809
		// (Invoke) Token: 0x0600B104 RID: 45316
		public delegate void OnOpen(NKMPostData postData);
	}
}
