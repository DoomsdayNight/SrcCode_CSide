using System;
using System.Collections.Generic;
using ClientPacket.Event;
using NKC.Publisher;
using NKM.Event;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BE7 RID: 3047
	public class NKCUIEventSubUIWechatFollow : NKCUIEventSubUIBase
	{
		// Token: 0x06008D54 RID: 36180 RVA: 0x00301194 File Offset: 0x002FF394
		public static void SetSendPacketAfterRefresh(bool bSet)
		{
			NKCUIEventSubUIWechatFollow.s_bSendPacketAfterRefresh = bSet;
		}

		// Token: 0x06008D55 RID: 36181 RVA: 0x0030119C File Offset: 0x002FF39C
		public static void DoAfterLogout()
		{
			NKCUIEventSubUIWechatFollow.s_WechatCouponData = null;
			NKCUIEventSubUIWechatFollow.s_fLastUpdateTime = float.MinValue;
			NKCUIEventSubUIWechatFollow.SetSendPacketAfterRefresh(true);
		}

		// Token: 0x06008D56 RID: 36182 RVA: 0x003011B4 File Offset: 0x002FF3B4
		public static void SetWechatCouponData(WechatCouponData cWechatCouponData)
		{
			NKCUIEventSubUIWechatFollow.s_WechatCouponData = cWechatCouponData;
		}

		// Token: 0x06008D57 RID: 36183 RVA: 0x003011BC File Offset: 0x002FF3BC
		public override void Init()
		{
			base.Init();
			if (this.m_csbtnCopy != null)
			{
				this.m_csbtnCopy.PointerClick.RemoveAllListeners();
				this.m_csbtnCopy.PointerClick.AddListener(new UnityAction(this.OnClickCopy));
			}
			if (this.m_csbtnGet != null)
			{
				this.m_csbtnGet.PointerClick.RemoveAllListeners();
				this.m_csbtnGet.PointerClick.AddListener(new UnityAction(this.OnClickGet));
			}
			if (this.m_lstRewardSlot != null)
			{
				for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
				{
					NKCUISlot nkcuislot = this.m_lstRewardSlot[i];
					if (!(nkcuislot == null))
					{
						nkcuislot.Init();
					}
				}
			}
		}

		// Token: 0x06008D58 RID: 36184 RVA: 0x0030127D File Offset: 0x002FF47D
		private void OnClickGet()
		{
			if (this.m_tabTemplet == null)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_WECHAT_COUPON_REWARD_REQ(this.m_tabTemplet.Key);
		}

		// Token: 0x06008D59 RID: 36185 RVA: 0x00301298 File Offset: 0x002FF498
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_tabTemplet = tabTemplet;
			this.UpdateUI();
			if (this.CheckComplete())
			{
				this.CheckCoupon(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			}
		}

		// Token: 0x06008D5A RID: 36186 RVA: 0x003012B7 File Offset: 0x002FF4B7
		private bool CheckComplete()
		{
			return NKCUIEventSubUIWechatFollow.s_WechatCouponData == null || NKCUIEventSubUIWechatFollow.s_WechatCouponData.state != WechatCouponState.Completed;
		}

		// Token: 0x06008D5B RID: 36187 RVA: 0x003012D0 File Offset: 0x002FF4D0
		private void CheckCoupon(NKC_OPEN_WAIT_BOX_TYPE eNKC_OPEN_WAIT_BOX_TYPE, bool bForce = false)
		{
			if (bForce || NKCUIEventSubUIWechatFollow.s_fLastUpdateTime + 600f < Time.time)
			{
				NKCUIEventSubUIWechatFollow.s_fLastUpdateTime = Time.time;
				NKCPacketSender.Send_NKMPacket_WECHAT_COUPON_CHECK_REQ(this.m_tabTemplet.Key, eNKC_OPEN_WAIT_BOX_TYPE);
			}
		}

		// Token: 0x06008D5C RID: 36188 RVA: 0x00301302 File Offset: 0x002FF502
		private void Update()
		{
			if (this.CheckComplete())
			{
				this.CheckCoupon(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, false);
			}
		}

		// Token: 0x06008D5D RID: 36189 RVA: 0x00301314 File Offset: 0x002FF514
		public override void Refresh()
		{
			this.UpdateUI();
			if (!NKCUIEventSubUIWechatFollow.s_bSendPacketAfterRefresh)
			{
				NKCUIEventSubUIWechatFollow.s_bSendPacketAfterRefresh = true;
				return;
			}
			if (this.CheckComplete())
			{
				this.CheckCoupon(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			}
		}

		// Token: 0x06008D5E RID: 36190 RVA: 0x0030133C File Offset: 0x002FF53C
		private void OnClickCopy()
		{
			if (this.m_lbCode == null)
			{
				return;
			}
			TextEditor textEditor = new TextEditor();
			textEditor.text = this.m_lbCode.text;
			textEditor.OnFocus();
			textEditor.Copy();
			NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_COPY_COMPLETE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
		}

		// Token: 0x06008D5F RID: 36191 RVA: 0x0030138C File Offset: 0x002FF58C
		private void UpdateUI()
		{
			if (this.m_tabTemplet == null)
			{
				return;
			}
			NKMEventWechatCouponTemplet nkmeventWechatCouponTemplet = NKMEventWechatCouponTemplet.Find(this.m_tabTemplet.m_EventID);
			if (nkmeventWechatCouponTemplet == null)
			{
				Debug.LogError("WechatCouponTemplet eventID not found, ID = " + this.m_tabTemplet.m_EventID.ToString());
				return;
			}
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstRewardSlot[i];
				if (!(nkcuislot == null))
				{
					if (i >= nkmeventWechatCouponTemplet.RewardList.Count)
					{
						nkcuislot.SetActive(false);
					}
					else
					{
						nkcuislot.SetActive(true);
						NKMRewardInfo nkmrewardInfo = nkmeventWechatCouponTemplet.RewardList[i];
						NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrewardInfo.rewardType, nkmrewardInfo.ID, nkmrewardInfo.Count, 0);
						nkcuislot.SetData(data, true, null);
						if (NKCUIEventSubUIWechatFollow.s_WechatCouponData != null && NKCUIEventSubUIWechatFollow.s_WechatCouponData.state == WechatCouponState.Completed)
						{
							nkcuislot.SetCompleteMark(true);
						}
						else
						{
							nkcuislot.SetCompleteMark(false);
						}
					}
				}
			}
			NKCUtil.SetLabelText(this.m_lbCode, NKCPublisherModule.Marketing.MakeWechatFollowCode(nkmeventWechatCouponTemplet.ZlongActivityInstanceId));
			if (NKCUIEventSubUIWechatFollow.s_WechatCouponData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnGet.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_objComplete, false);
				return;
			}
			if (NKCUIEventSubUIWechatFollow.s_WechatCouponData.state == WechatCouponState.Completed)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnGet.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_objComplete, true);
				return;
			}
			if (NKCUIEventSubUIWechatFollow.s_WechatCouponData.state == WechatCouponState.Registered)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnGet.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_objComplete, false);
				this.m_csbtnGet.SetLock(false, false);
				return;
			}
			if (NKCUIEventSubUIWechatFollow.s_WechatCouponData.state == WechatCouponState.Initialized)
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnGet.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_objComplete, false);
				this.m_csbtnGet.SetLock(true, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnGet.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_objComplete, false);
		}

		// Token: 0x04007A29 RID: 31273
		public List<NKCUISlot> m_lstRewardSlot;

		// Token: 0x04007A2A RID: 31274
		public Text m_lbCode;

		// Token: 0x04007A2B RID: 31275
		public NKCUIComStateButton m_csbtnCopy;

		// Token: 0x04007A2C RID: 31276
		public NKCUIComStateButton m_csbtnGet;

		// Token: 0x04007A2D RID: 31277
		public GameObject m_objComplete;

		// Token: 0x04007A2E RID: 31278
		private static float s_fLastUpdateTime = float.MinValue;

		// Token: 0x04007A2F RID: 31279
		private static WechatCouponData s_WechatCouponData = new WechatCouponData();

		// Token: 0x04007A30 RID: 31280
		private static bool s_bSendPacketAfterRefresh = true;
	}
}
