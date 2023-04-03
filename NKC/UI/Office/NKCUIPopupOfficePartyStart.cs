using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.Templet;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Office
{
	// Token: 0x02000AFC RID: 2812
	public class NKCUIPopupOfficePartyStart : NKCUIBase
	{
		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06007F25 RID: 32549 RVA: 0x002AA7E4 File Offset: 0x002A89E4
		public static NKCUIPopupOfficePartyStart Instance
		{
			get
			{
				if (NKCUIPopupOfficePartyStart.m_Instance == null)
				{
					NKCUIPopupOfficePartyStart.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupOfficePartyStart>("ab_ui_office", "AB_UI_POPUP_OFFICE_PARTY_START", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupOfficePartyStart.CleanupInstance)).GetInstance<NKCUIPopupOfficePartyStart>();
					NKCUIPopupOfficePartyStart.m_Instance.InitUI();
				}
				return NKCUIPopupOfficePartyStart.m_Instance;
			}
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x002AA833 File Offset: 0x002A8A33
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupOfficePartyStart.m_Instance != null && NKCUIPopupOfficePartyStart.m_Instance.IsOpen)
			{
				NKCUIPopupOfficePartyStart.m_Instance.Close();
			}
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x002AA858 File Offset: 0x002A8A58
		private static void CleanupInstance()
		{
			NKCUIPopupOfficePartyStart.m_Instance = null;
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06007F28 RID: 32552 RVA: 0x002AA860 File Offset: 0x002A8A60
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupOfficePartyStart.m_Instance != null && NKCUIPopupOfficePartyStart.m_Instance.IsOpen;
			}
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06007F29 RID: 32553 RVA: 0x002AA87B File Offset: 0x002A8A7B
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06007F2A RID: 32554 RVA: 0x002AA87E File Offset: 0x002A8A7E
		public override string MenuName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x002AA885 File Offset: 0x002A8A85
		public override void OnBackButton()
		{
			if (this.m_bProgress)
			{
				this.Finish();
				return;
			}
			base.Close();
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x002AA89C File Offset: 0x002A8A9C
		public override void CloseInternal()
		{
			this.m_Animator.ResetTrigger("PARTY_OVER");
			base.gameObject.SetActive(false);
			NKCUIPopupOfficePartyStart.OnClose onClose = this.dOnClose;
			if (onClose == null)
			{
				return;
			}
			onClose(this.m_partyTemplet);
		}

		// Token: 0x06007F2D RID: 32557 RVA: 0x002AA8D0 File Offset: 0x002A8AD0
		private void InitUI()
		{
			foreach (NKCUISlot nkcuislot in this.m_lstSlotReward)
			{
				nkcuislot.Init();
				NKCUtil.SetGameobjectActive(nkcuislot, false);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.OnBackButton));
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x002AA940 File Offset: 0x002A8B40
		public void Open(NKMRewardData rewardData, NKCUIPopupOfficePartyStart.OnClose onClose)
		{
			this.m_partyTemplet = NKCOfficePartyTemplet.GetRandomTemplet();
			if (this.m_Animator != null)
			{
				this.m_Animator.ResetTrigger("PARTY_OVER");
			}
			if (this.m_partyTemplet == null)
			{
				Debug.LogError("party templet not loaded!");
				base.gameObject.SetActive(false);
				if (onClose != null)
				{
					onClose(null);
				}
				return;
			}
			this.dOnClose = onClose;
			this.SetIllust(this.m_partyTemplet);
			this.SetReward(rewardData);
			base.UIOpened(true);
			base.StartCoroutine(this.Process());
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x002AA9CD File Offset: 0x002A8BCD
		private IEnumerator Process()
		{
			this.m_fTotalTime = this.m_fTimeBeforeFirstReward + this.m_fTimePerReward * (float)this.m_activeRewardCount + this.m_fTimeAfterLastReward + this.m_fTimeAfterFinishAnimation;
			this.m_fCurrentTime = 0f;
			this.m_bProgress = true;
			foreach (NKCUISlot targetMono in this.m_lstSlotReward)
			{
				NKCUtil.SetGameobjectActive(targetMono, false);
			}
			yield return this.WaitTime(this.m_fTimeBeforeFirstReward);
			int num;
			for (int i = 0; i < this.m_activeRewardCount; i = num + 1)
			{
				NKCUISlot nkcuislot = this.m_lstSlotReward[i];
				NKCUtil.SetGameobjectActive(nkcuislot, true);
				if (nkcuislot != null)
				{
					nkcuislot.transform.localScale = Vector3.zero;
					nkcuislot.transform.DOScale(this.m_fSlotScale, this.m_fSlotAnimTime).SetEase(this.m_fSlotAnimEase);
				}
				yield return this.WaitTime(this.m_fTimePerReward);
				num = i;
			}
			yield return this.WaitTime(this.m_fTimeAfterLastReward);
			this.m_Animator.SetTrigger("PARTY_OVER");
			yield return this.WaitTime(this.m_fTimeAfterFinishAnimation);
			this.Finish();
			yield break;
		}

		// Token: 0x06007F30 RID: 32560 RVA: 0x002AA9DC File Offset: 0x002A8BDC
		private void SetProgress(float progress)
		{
			NKCUtil.SetImageFillAmount(this.m_imgProgress, progress);
			NKCUtil.SetLabelText(this.m_lbProgress, string.Format("{0:0.00%}", progress));
		}

		// Token: 0x06007F31 RID: 32561 RVA: 0x002AAA05 File Offset: 0x002A8C05
		private IEnumerator WaitTime(float waitTime)
		{
			float currentTime = 0f;
			if (waitTime <= 0f)
			{
				yield break;
			}
			while (currentTime < waitTime)
			{
				if (Input.anyKey)
				{
					currentTime += Time.deltaTime * 2f;
					this.m_fCurrentTime += Time.deltaTime * 2f;
				}
				else
				{
					currentTime += Time.deltaTime;
					this.m_fCurrentTime += Time.deltaTime;
				}
				this.SetProgress(this.m_fCurrentTime / this.m_fTotalTime);
				yield return null;
			}
			yield break;
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x002AAA1C File Offset: 0x002A8C1C
		private void Finish()
		{
			base.StopAllCoroutines();
			this.SetProgress(1f);
			for (int i = 0; i < this.m_lstSlotReward.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlotReward[i];
				if (!(nkcuislot == null))
				{
					NKCUtil.SetGameobjectActive(nkcuislot, i < this.m_activeRewardCount);
					nkcuislot.transform.DOKill(false);
					nkcuislot.transform.localScale = Vector3.one * this.m_fSlotScale;
				}
			}
			this.m_Animator.SetTrigger("PARTY_OVER");
			this.m_bProgress = false;
		}

		// Token: 0x06007F33 RID: 32563 RVA: 0x002AAAB4 File Offset: 0x002A8CB4
		private void SetReward(NKMRewardData rewardData)
		{
			List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(rewardData, false, false);
			NKCUISlot.SetSlotListData(this.m_lstSlotReward, list, false, true, true, null, new NKCUISlot.SlotClickType[1]);
			this.m_activeRewardCount = Mathf.Min(list.Count, this.m_lstSlotReward.Count);
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x002AAAFC File Offset: 0x002A8CFC
		private void SetIllust(NKCOfficePartyTemplet partyTemplet)
		{
			UnityEngine.Object.Destroy(this.m_objIllust);
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<GameObject>(partyTemplet.IllustName, false);
			if (nkcassetResourceData != null && nkcassetResourceData.GetAsset<GameObject>() != null)
			{
				this.m_objIllust = UnityEngine.Object.Instantiate<GameObject>(nkcassetResourceData.GetAsset<GameObject>(), this.m_trIllustRoot);
			}
		}

		// Token: 0x04006BAE RID: 27566
		private const string ASSET_BUNDLE_NAME = "ab_ui_office";

		// Token: 0x04006BAF RID: 27567
		private const string UI_ASSET_NAME = "AB_UI_POPUP_OFFICE_PARTY_START";

		// Token: 0x04006BB0 RID: 27568
		private static NKCUIPopupOfficePartyStart m_Instance;

		// Token: 0x04006BB1 RID: 27569
		public Transform m_trIllustRoot;

		// Token: 0x04006BB2 RID: 27570
		public Text m_lbProgress;

		// Token: 0x04006BB3 RID: 27571
		public Image m_imgProgress;

		// Token: 0x04006BB4 RID: 27572
		public List<NKCUISlot> m_lstSlotReward;

		// Token: 0x04006BB5 RID: 27573
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006BB6 RID: 27574
		public Animator m_Animator;

		// Token: 0x04006BB7 RID: 27575
		public float m_fTimeBeforeFirstReward = 1f;

		// Token: 0x04006BB8 RID: 27576
		public float m_fTimePerReward = 0.25f;

		// Token: 0x04006BB9 RID: 27577
		public float m_fTimeAfterLastReward = 1f;

		// Token: 0x04006BBA RID: 27578
		public float m_fTimeAfterFinishAnimation = 1f;

		// Token: 0x04006BBB RID: 27579
		public float m_fSlotAnimTime = 0.5f;

		// Token: 0x04006BBC RID: 27580
		public Ease m_fSlotAnimEase = Ease.OutBack;

		// Token: 0x04006BBD RID: 27581
		private GameObject m_objIllust;

		// Token: 0x04006BBE RID: 27582
		private NKCUIPopupOfficePartyStart.OnClose dOnClose;

		// Token: 0x04006BBF RID: 27583
		private NKCOfficePartyTemplet m_partyTemplet;

		// Token: 0x04006BC0 RID: 27584
		private int m_activeRewardCount;

		// Token: 0x04006BC1 RID: 27585
		private bool m_bProgress;

		// Token: 0x04006BC2 RID: 27586
		private float m_fTotalTime;

		// Token: 0x04006BC3 RID: 27587
		private float m_fCurrentTime;

		// Token: 0x04006BC4 RID: 27588
		public float m_fSlotScale = 0.75f;

		// Token: 0x04006BC5 RID: 27589
		private const string ANIMATOR_TRIGGER = "PARTY_OVER";

		// Token: 0x02001886 RID: 6278
		// (Invoke) Token: 0x0600B61E RID: 46622
		public delegate void OnClose(NKCOfficePartyTemplet partyTemplet);
	}
}
