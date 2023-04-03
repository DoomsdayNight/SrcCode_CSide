using System;
using System.Collections;
using System.Collections.Generic;
using NKC.Templet;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A96 RID: 2710
	public class NKCUIPopupVoice : NKCUIBase
	{
		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06007807 RID: 30727 RVA: 0x0027DD73 File Offset: 0x0027BF73
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06007808 RID: 30728 RVA: 0x0027DD78 File Offset: 0x0027BF78
		public static NKCUIPopupVoice Instance
		{
			get
			{
				if (NKCUIPopupVoice.m_Instance == null)
				{
					NKCUIPopupVoice.m_Instance = NKCUIManager.OpenNewInstance<NKCUIPopupVoice>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_VOICE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIPopupVoice.CleanupInstance)).GetInstance<NKCUIPopupVoice>();
					NKCUIPopupVoice.m_Instance.InitUI();
				}
				return NKCUIPopupVoice.m_Instance;
			}
		}

		// Token: 0x06007809 RID: 30729 RVA: 0x0027DDC7 File Offset: 0x0027BFC7
		private static void CleanupInstance()
		{
			NKCUIPopupVoice.m_Instance = null;
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x0600780A RID: 30730 RVA: 0x0027DDCF File Offset: 0x0027BFCF
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIPopupVoice.m_Instance != null && NKCUIPopupVoice.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600780B RID: 30731 RVA: 0x0027DDEA File Offset: 0x0027BFEA
		public static void CheckInstanceAndClose()
		{
			if (NKCUIPopupVoice.m_Instance != null && NKCUIPopupVoice.m_Instance.IsOpen)
			{
				NKCUIPopupVoice.m_Instance.Close();
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x0600780C RID: 30732 RVA: 0x0027DE0F File Offset: 0x0027C00F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x0600780D RID: 30733 RVA: 0x0027DE12 File Offset: 0x0027C012
		public override string MenuName
		{
			get
			{
				return "VOICE";
			}
		}

		// Token: 0x0600780E RID: 30734 RVA: 0x0027DE1C File Offset: 0x0027C01C
		private void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnBG.PointerClick.RemoveAllListeners();
			this.m_btnBG.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_loopScrollRect.dOnGetObject += this.OnGetObject;
			this.m_loopScrollRect.dOnProvideData += this.OnProvideData;
			this.m_loopScrollRect.dOnReturnObject += this.OnReturnObject;
			this.m_loopScrollRect.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopScrollRect, null);
		}

		// Token: 0x0600780F RID: 30735 RVA: 0x0027DEE0 File Offset: 0x0027C0E0
		private RectTransform OnGetObject(int index)
		{
			if (this.m_slotPool.Count > 0)
			{
				RectTransform rectTransform = this.m_slotPool.Pop();
				NKCUtil.SetGameobjectActive(rectTransform, true);
				return rectTransform;
			}
			NKCUIPopupVoiceSlot nkcuipopupVoiceSlot = NKCUIPopupVoiceSlot.newInstance(this.m_contents);
			if (nkcuipopupVoiceSlot == null)
			{
				return null;
			}
			this.m_slotList.Add(nkcuipopupVoiceSlot);
			return nkcuipopupVoiceSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007810 RID: 30736 RVA: 0x0027DF38 File Offset: 0x0027C138
		private void OnProvideData(Transform tr, int index)
		{
			NKCUIPopupVoiceSlot component = tr.GetComponent<NKCUIPopupVoiceSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_listData.Count <= index)
			{
				Debug.LogError(string.Format("popupvoice - index {0}, data Count {1}", index, this.m_listData.Count));
				return;
			}
			NKCUIPopupVoice.VoiceData voiceData = this.m_listData[index];
			if (voiceData == null)
			{
				return;
			}
			NKCCollectionVoiceTemplet templet = NKMTempletContainer<NKCCollectionVoiceTemplet>.Find(voiceData.idx);
			component.SetUI(index, templet, voiceData.voiceBundle == VOICE_BUNDLE.SKIN, new NKCUIPopupVoiceSlot.OnChangeToggle(this.OnTouhchSlot));
			component.SetToggle(this.m_currentIndex);
		}

		// Token: 0x06007811 RID: 30737 RVA: 0x0027DFD0 File Offset: 0x0027C1D0
		private void OnReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
			this.m_slotPool.Push(tr.GetComponent<RectTransform>());
		}

		// Token: 0x06007812 RID: 30738 RVA: 0x0027DFF6 File Offset: 0x0027C1F6
		public void Open(NKMUnitData unit)
		{
			if (unit == null)
			{
				return;
			}
			this.Open(unit.m_UnitID, unit.m_SkinID, unit.IsPermanentContract);
		}

		// Token: 0x06007813 RID: 30739 RVA: 0x0027E014 File Offset: 0x0027C214
		public void Open(NKMUnitTempletBase unitTempletBase, bool bLifetime = false)
		{
			if (unitTempletBase == null)
			{
				return;
			}
			this.Open(unitTempletBase.m_UnitID, 0, bLifetime);
		}

		// Token: 0x06007814 RID: 30740 RVA: 0x0027E028 File Offset: 0x0027C228
		public void Open(NKMSkinTemplet skinTemplet, bool bLifetime = false)
		{
			if (skinTemplet == null)
			{
				return;
			}
			this.Open(skinTemplet.m_SkinEquipUnitID, skinTemplet.m_SkinID, bLifetime);
		}

		// Token: 0x06007815 RID: 30741 RVA: 0x0027E044 File Offset: 0x0027C244
		public void Open(int unitID, int skinID, bool bLifetime)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			this.m_unitStrID = unitTempletBase.m_UnitStrID;
			this.m_skinID = skinID;
			this.m_bLifetime = bLifetime;
			this.SetUI();
			base.UIOpened(true);
		}

		// Token: 0x06007816 RID: 30742 RVA: 0x0027E07F File Offset: 0x0027C27F
		public override void CloseInternal()
		{
			this.EndCoroutine();
			NKCUIVoiceManager.StopVoice();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007817 RID: 30743 RVA: 0x0027E098 File Offset: 0x0027C298
		private void OnDestroy()
		{
			this.Clear();
			NKCUIPopupVoice.m_Instance = null;
		}

		// Token: 0x06007818 RID: 30744 RVA: 0x0027E0A8 File Offset: 0x0027C2A8
		private void Clear()
		{
			for (int i = 0; i < this.m_slotList.Count; i++)
			{
				this.m_slotList[i].Clear();
			}
			this.m_slotList.Clear();
			this.m_slotPool.Clear();
			this.m_listData.Clear();
		}

		// Token: 0x06007819 RID: 30745 RVA: 0x0027E100 File Offset: 0x0027C300
		private void SetUI()
		{
			this.m_currentIndex = -1;
			this.RefreshSlotToggle(this.m_currentIndex);
			this.m_listData.Clear();
			this.m_listData = this.GetListVoice(this.m_unitStrID, this.m_skinID, this.m_bLifetime);
			this.m_listData.Sort(new Comparison<NKCUIPopupVoice.VoiceData>(this.CompareVoiceData));
			this.m_loopScrollRect.TotalCount = this.m_listData.Count;
			this.m_loopScrollRect.SetIndexPosition(0);
			this.m_loopScrollRect.RefreshCells(true);
			NKCUtil.SetLabelText(this.m_lbVoiceActorName, NKCVoiceActorNameTemplet.FindActorName(this.m_unitStrID, this.m_skinID));
		}

		// Token: 0x0600781A RID: 30746 RVA: 0x0027E1AC File Offset: 0x0027C3AC
		private void OnTouhchSlot(bool bPlay, int index)
		{
			if (index < 0 || this.m_listData.Count <= index)
			{
				return;
			}
			this.EndCoroutine();
			this.m_currentIndex = index;
			this.PlayVoice(bPlay, index);
			if (bPlay)
			{
				this.RefreshSlotToggle(index);
				this.m_toggleCoroutine = base.StartCoroutine(this.UpdateSlotToggle());
			}
		}

		// Token: 0x0600781B RID: 30747 RVA: 0x0027E200 File Offset: 0x0027C400
		private void PlayVoice(bool bPlay, int index)
		{
			if (bPlay)
			{
				if (index < 0 || this.m_listData.Count <= index)
				{
					return;
				}
				NKCUIPopupVoice.VoiceData voiceData = this.m_listData[index];
				if (voiceData == null)
				{
					return;
				}
				NKCCollectionVoiceTemplet nkccollectionVoiceTemplet = NKMTempletContainer<NKCCollectionVoiceTemplet>.Find(voiceData.idx);
				if (nkccollectionVoiceTemplet == null)
				{
					return;
				}
				VOICE_BUNDLE voiceBundle = voiceData.voiceBundle;
				NKMAssetName nkmassetName = NKCUIVoiceManager.PlayOnUI(this.m_unitStrID, this.m_skinID, nkccollectionVoiceTemplet.m_VoicePostID, 100f, voiceBundle, false);
				if (nkmassetName != null && nkccollectionVoiceTemplet.m_VoiceCategory != NKC_VOICE_TYPE.ETC)
				{
					NKCUIManager.NKCUIOverlayCaption.OpenCaption(NKCUtilString.GetVoiceCaption(nkmassetName), NKCUIVoiceManager.GetCurrentSoundUID(), 0f);
					return;
				}
			}
			else
			{
				NKCUIVoiceManager.StopVoice();
			}
		}

		// Token: 0x0600781C RID: 30748 RVA: 0x0027E29C File Offset: 0x0027C49C
		private void RefreshSlotToggle(int seletedIndex)
		{
			for (int i = 0; i < this.m_slotList.Count; i++)
			{
				this.m_slotList[i].SetToggle(seletedIndex);
			}
		}

		// Token: 0x0600781D RID: 30749 RVA: 0x0027E2D1 File Offset: 0x0027C4D1
		private IEnumerator UpdateSlotToggle()
		{
			while (NKCSoundManager.IsPlayingVoice(-1))
			{
				yield return null;
			}
			this.m_toggleCoroutine = null;
			this.m_currentIndex = -1;
			this.RefreshSlotToggle(this.m_currentIndex);
			yield break;
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x0027E2E0 File Offset: 0x0027C4E0
		private void EndCoroutine()
		{
			if (this.m_toggleCoroutine != null)
			{
				base.StopCoroutine(this.m_toggleCoroutine);
				this.m_toggleCoroutine = null;
			}
		}

		// Token: 0x0600781F RID: 30751 RVA: 0x0027E2FD File Offset: 0x0027C4FD
		private int CompareVoiceData(NKCUIPopupVoice.VoiceData a, NKCUIPopupVoice.VoiceData b)
		{
			if (a.idx == b.idx)
			{
				return a.voiceBundle.CompareTo(b.voiceBundle);
			}
			return a.idx.CompareTo(b.idx);
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x0027E33C File Offset: 0x0027C53C
		private List<NKCUIPopupVoice.VoiceData> GetListVoice(string unitStrID, int skinID, bool bLifetime)
		{
			List<NKCUIPopupVoice.VoiceData> list = new List<NKCUIPopupVoice.VoiceData>();
			using (IEnumerator<NKCCollectionVoiceTemplet> enumerator = NKMTempletContainer<NKCCollectionVoiceTemplet>.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NKCCollectionVoiceTemplet voiceTemplet = enumerator.Current;
					if (!voiceTemplet.m_bVoiceCondLifetime || bLifetime)
					{
						if (this.CheckVoice(unitStrID, skinID, voiceTemplet, VOICE_BUNDLE.SKIN))
						{
							if (voiceTemplet.m_VoiceType != VOICE_TYPE.VT_NONE)
							{
								list.RemoveAll((NKCUIPopupVoice.VoiceData v) => v.voiceType == voiceTemplet.m_VoiceType && v.voiceBundle != VOICE_BUNDLE.SKIN);
							}
							list.Add(new NKCUIPopupVoice.VoiceData
							{
								idx = voiceTemplet.IDX,
								voiceBundle = VOICE_BUNDLE.SKIN,
								voiceType = voiceTemplet.m_VoiceType
							});
						}
						else if (voiceTemplet.m_VoiceType == VOICE_TYPE.VT_NONE || !list.Exists((NKCUIPopupVoice.VoiceData v) => v.voiceType == voiceTemplet.m_VoiceType && v.voiceBundle == VOICE_BUNDLE.SKIN))
						{
							if (this.CheckVoice(unitStrID, skinID, voiceTemplet, VOICE_BUNDLE.UNIT))
							{
								list.Add(new NKCUIPopupVoice.VoiceData
								{
									idx = voiceTemplet.IDX,
									voiceBundle = VOICE_BUNDLE.UNIT,
									voiceType = voiceTemplet.m_VoiceType
								});
							}
							if (this.CheckVoice(unitStrID, skinID, voiceTemplet, VOICE_BUNDLE.COMMON))
							{
								list.Add(new NKCUIPopupVoice.VoiceData
								{
									idx = voiceTemplet.IDX,
									voiceBundle = VOICE_BUNDLE.COMMON,
									voiceType = voiceTemplet.m_VoiceType
								});
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06007821 RID: 30753 RVA: 0x0027E4E0 File Offset: 0x0027C6E0
		private bool CheckVoice(string unitStrID, int skinID, NKCCollectionVoiceTemplet voiceTemplet, VOICE_BUNDLE bundleType)
		{
			return NKCUIVoiceManager.CheckAsset(unitStrID, skinID, voiceTemplet.m_VoicePostID, bundleType);
		}

		// Token: 0x04006498 RID: 25752
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04006499 RID: 25753
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_VOICE";

		// Token: 0x0400649A RID: 25754
		private static NKCUIPopupVoice m_Instance;

		// Token: 0x0400649B RID: 25755
		public LoopVerticalScrollRect m_loopScrollRect;

		// Token: 0x0400649C RID: 25756
		public Transform m_contents;

		// Token: 0x0400649D RID: 25757
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400649E RID: 25758
		public NKCUIComStateButton m_btnBG;

		// Token: 0x0400649F RID: 25759
		public Text m_lbVoiceActorName;

		// Token: 0x040064A0 RID: 25760
		private List<NKCUIPopupVoiceSlot> m_slotList = new List<NKCUIPopupVoiceSlot>();

		// Token: 0x040064A1 RID: 25761
		private Stack<RectTransform> m_slotPool = new Stack<RectTransform>();

		// Token: 0x040064A2 RID: 25762
		private List<NKCUIPopupVoice.VoiceData> m_listData = new List<NKCUIPopupVoice.VoiceData>();

		// Token: 0x040064A3 RID: 25763
		private string m_unitStrID;

		// Token: 0x040064A4 RID: 25764
		private int m_skinID;

		// Token: 0x040064A5 RID: 25765
		private bool m_bLifetime;

		// Token: 0x040064A6 RID: 25766
		private int m_currentIndex;

		// Token: 0x040064A7 RID: 25767
		private Coroutine m_toggleCoroutine;

		// Token: 0x020017F2 RID: 6130
		public class VoiceData
		{
			// Token: 0x0400A7BB RID: 42939
			public int idx;

			// Token: 0x0400A7BC RID: 42940
			public VOICE_TYPE voiceType;

			// Token: 0x0400A7BD RID: 42941
			public VOICE_BUNDLE voiceBundle;
		}
	}
}
