using System;
using System.Collections.Generic;
using ClientPacket.Community;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A41 RID: 2625
	public class NKCPopupEmoticonSetting : NKCUIBase
	{
		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06007313 RID: 29459 RVA: 0x00263FDC File Offset: 0x002621DC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06007314 RID: 29460 RVA: 0x00263FDF File Offset: 0x002621DF
		public override string MenuName
		{
			get
			{
				return "PopupEmoticonSetting";
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06007315 RID: 29461 RVA: 0x00263FE8 File Offset: 0x002621E8
		public static NKCPopupEmoticonSetting Instance
		{
			get
			{
				if (NKCPopupEmoticonSetting.m_Instance == null)
				{
					NKCPopupEmoticonSetting.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEmoticonSetting>("AB_UI_NKM_UI_EMOTICON", "NKM_UI_EMOTICON_DECK_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEmoticonSetting.CleanupInstance)).GetInstance<NKCPopupEmoticonSetting>();
					NKCPopupEmoticonSetting.m_Instance.InitUI();
				}
				return NKCPopupEmoticonSetting.m_Instance;
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06007316 RID: 29462 RVA: 0x00264037 File Offset: 0x00262237
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEmoticonSetting.m_Instance != null && NKCPopupEmoticonSetting.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007317 RID: 29463 RVA: 0x00264052 File Offset: 0x00262252
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEmoticonSetting.m_Instance != null && NKCPopupEmoticonSetting.m_Instance.IsOpen)
			{
				NKCPopupEmoticonSetting.m_Instance.Close();
			}
		}

		// Token: 0x06007318 RID: 29464 RVA: 0x00264077 File Offset: 0x00262277
		private static void CleanupInstance()
		{
			NKCPopupEmoticonSetting.m_Instance = null;
		}

		// Token: 0x06007319 RID: 29465 RVA: 0x00264080 File Offset: 0x00262280
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_etBG.triggers.Clear();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(base.Close));
			for (int i = 0; i < this.m_lstNKCPopupEmoticonSlotSDLeft.Count; i++)
			{
				this.m_lstNKCPopupEmoticonSlotSDLeft[i].SetClickEvent(new NKCUISlot.OnClick(this.OnClickLeftEmoticon));
				this.m_lstNKCPopupEmoticonSlotSDLeft[i].Reset_SD_Scale(0.8f);
			}
			for (int j = 0; j < this.m_lstNKCPopupEmoticonSlotCommentLeft.Count; j++)
			{
				this.m_lstNKCPopupEmoticonSlotCommentLeft[j].SetClickEvent(new NKCPopupEmoticonSlotComment.dOnClick(this.OnClickLeftEmoticon));
			}
			this.m_lvsrSD.dOnGetObject += this.GetSDSlot;
			this.m_lvsrSD.dOnReturnObject += this.ReturnSDSlot;
			this.m_lvsrSD.dOnProvideData += this.ProvideSDSlot;
			NKCUtil.SetScrollHotKey(this.m_lvsrSD, null);
			this.m_lvsrComment.dOnGetObject += this.GetCommentSlot;
			this.m_lvsrComment.dOnReturnObject += this.ReturnCommentSlot;
			this.m_lvsrComment.dOnProvideData += this.ProvideCommentSlot;
			NKCUtil.SetScrollHotKey(this.m_lvsrComment, null);
		}

		// Token: 0x0600731A RID: 29466 RVA: 0x00264214 File Offset: 0x00262414
		private void UpdateSDCollectionExceptPreset()
		{
			this.m_lstSDCollectionExceptPreset.Clear();
			using (HashSet<int>.Enumerator enumerator = NKCEmoticonManager.m_hsEmoticonCollection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int id = enumerator.Current;
					NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(id);
					if (nkmemoticonTemplet != null && nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_ANI && NKCEmoticonManager.m_lstAniPreset.FindIndex((int key) => key == id) == -1)
					{
						this.m_lstSDCollectionExceptPreset.Add(id);
					}
				}
			}
			this.m_lstSDCollectionExceptPreset.Sort();
		}

		// Token: 0x0600731B RID: 29467 RVA: 0x002642C4 File Offset: 0x002624C4
		private void UpdateCommentCollectionExceptPreset()
		{
			this.m_lstCommentCollectionExceptPreset.Clear();
			using (HashSet<int>.Enumerator enumerator = NKCEmoticonManager.m_hsEmoticonCollection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int id = enumerator.Current;
					NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(id);
					if (nkmemoticonTemplet != null && nkmemoticonTemplet.m_EmoticonType == NKM_EMOTICON_TYPE.NET_TEXT && NKCEmoticonManager.m_lstTextPreset.FindIndex((int key) => key == id) == -1)
					{
						this.m_lstCommentCollectionExceptPreset.Add(id);
					}
				}
			}
			this.m_lstCommentCollectionExceptPreset.Sort();
		}

		// Token: 0x0600731C RID: 29468 RVA: 0x00264374 File Offset: 0x00262574
		public RectTransform GetSDSlot(int index)
		{
			NKCPopupEmoticonSlotSD newInstance = NKCPopupEmoticonSlotSD.GetNewInstance(null);
			newInstance.Reset_SD_Scale(0.62f);
			this.m_lstSlotSD.Add(newInstance);
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x0600731D RID: 29469 RVA: 0x002643A8 File Offset: 0x002625A8
		public void ReturnSDSlot(Transform tr)
		{
			NKCPopupEmoticonSlotSD component = tr.GetComponent<NKCPopupEmoticonSlotSD>();
			this.m_lstSlotSD.Remove(component);
			tr.SetParent(base.transform);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x0600731E RID: 29470 RVA: 0x002643E0 File Offset: 0x002625E0
		public void ProvideSDSlot(Transform tr, int index)
		{
			NKCPopupEmoticonSlotSD component = tr.GetComponent<NKCPopupEmoticonSlotSD>();
			if (component != null)
			{
				if (this.m_lstSDCollectionExceptPreset.Count > index && index >= 0)
				{
					NKCUtil.SetGameobjectActive(component, true);
					component.StopSDAni();
					component.SetClickEvent(new NKCUISlot.OnClick(this.OnClickRightSideSD));
					component.SetClickEventForChange(new NKCPopupEmoticonSlotSD.dOnClickChange(this.OnClickRightSideSDForChange));
					component.SetUI(this.m_lstSDCollectionExceptPreset[index]);
					if (component.GetEmoticonID() == this.m_RightSideSelectedEmoticonIDSD)
					{
						component.SetSelectedWithChangeButton(true);
						component.MakeCanvas();
						component.ResetCanvasLayer(101);
						return;
					}
					component.SetSelectedWithChangeButton(false);
					component.RemoveCanvas();
					return;
				}
				else
				{
					NKCUtil.SetGameobjectActive(component, false);
				}
			}
		}

		// Token: 0x0600731F RID: 29471 RVA: 0x00264490 File Offset: 0x00262690
		public RectTransform GetCommentSlot(int index)
		{
			NKCPopupEmoticonSlotComment newInstanceLarge = NKCPopupEmoticonSlotComment.GetNewInstanceLarge(null);
			this.m_lstSlotComment.Add(newInstanceLarge);
			return newInstanceLarge.GetComponent<RectTransform>();
		}

		// Token: 0x06007320 RID: 29472 RVA: 0x002644B8 File Offset: 0x002626B8
		public void ReturnCommentSlot(Transform tr)
		{
			NKCPopupEmoticonSlotComment component = tr.GetComponent<NKCPopupEmoticonSlotComment>();
			this.m_lstSlotComment.Remove(component);
			tr.SetParent(base.transform);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007321 RID: 29473 RVA: 0x002644F0 File Offset: 0x002626F0
		public void ProvideCommentSlot(Transform tr, int index)
		{
			NKCPopupEmoticonSlotComment component = tr.GetComponent<NKCPopupEmoticonSlotComment>();
			if (component != null)
			{
				if (this.m_lstCommentCollectionExceptPreset.Count > index && index >= 0)
				{
					NKCUtil.SetGameobjectActive(component, true);
					component.SetClickEvent(new NKCPopupEmoticonSlotComment.dOnClick(this.OnClickRightSideComment));
					component.SetClickEventForChange(new NKCPopupEmoticonSlotComment.dOnClick(this.OnClickRightSideCommentForChange));
					component.SetUI(this.m_lstCommentCollectionExceptPreset[index]);
					if (component.GetEmoticonID() == this.m_RightSideSelectedEmoticonIDComment)
					{
						component.SetSelected(true);
						return;
					}
					component.SetSelected(false);
					return;
				}
				else
				{
					NKCUtil.SetGameobjectActive(component, false);
				}
			}
		}

		// Token: 0x06007322 RID: 29474 RVA: 0x00264584 File Offset: 0x00262784
		private void OnClickRightSideSD(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData == null)
			{
				return;
			}
			this.m_RightSideSelectedEmoticonIDSD = slotData.ID;
			for (int i = 0; i < this.m_lstSlotSD.Count; i++)
			{
				NKCPopupEmoticonSlotSD nkcpopupEmoticonSlotSD = this.m_lstSlotSD[i];
				if (nkcpopupEmoticonSlotSD != null)
				{
					bool flag = nkcpopupEmoticonSlotSD.GetEmoticonID() == this.m_RightSideSelectedEmoticonIDSD;
					nkcpopupEmoticonSlotSD.SetSelectedWithChangeButton(flag);
					if (flag)
					{
						nkcpopupEmoticonSlotSD.PlaySDAni();
						nkcpopupEmoticonSlotSD.MakeCanvas();
						nkcpopupEmoticonSlotSD.ResetCanvasLayer(101);
					}
					else
					{
						nkcpopupEmoticonSlotSD.StopSDAni();
						nkcpopupEmoticonSlotSD.RemoveCanvas();
					}
				}
			}
		}

		// Token: 0x06007323 RID: 29475 RVA: 0x00264608 File Offset: 0x00262808
		private void OnClickRightSideSDForChange(int emoticonID)
		{
			int num = -1;
			for (int i = 0; i < this.m_lstNKCPopupEmoticonSlotSDLeft.Count; i++)
			{
				NKCPopupEmoticonSlotSD nkcpopupEmoticonSlotSD = this.m_lstNKCPopupEmoticonSlotSDLeft[i];
				if (!(nkcpopupEmoticonSlotSD == null) && nkcpopupEmoticonSlotSD.GetSelected())
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			NKCPacketSender.Send_NKMPacket_EMOTICON_ANI_CHANGE_REQ(num, emoticonID);
		}

		// Token: 0x06007324 RID: 29476 RVA: 0x0026465C File Offset: 0x0026285C
		public void OnRecv(NKMPacket_EMOTICON_ANI_CHANGE_ACK cNKMPacket_EMOTICON_ANI_CHANGE_ACK)
		{
			int presetIndex = cNKMPacket_EMOTICON_ANI_CHANGE_ACK.presetIndex;
			int emoticonId = cNKMPacket_EMOTICON_ANI_CHANGE_ACK.emoticonId;
			if (presetIndex < 0 || presetIndex >= NKCEmoticonManager.m_lstAniPreset.Count)
			{
				Debug.LogError("EMOTICON_ANI_CHANGE_ACK preset index invalid, index : " + presetIndex.ToString());
				return;
			}
			if (NKMEmoticonTemplet.Find(emoticonId) == null)
			{
				Debug.LogError("EMOTICON_ANI_CHANGE_ACK emoticon ID is invalid, ID : " + emoticonId.ToString());
				return;
			}
			this.m_RightSideSelectedEmoticonIDSD = -1;
			NKCPopupEmoticonSlotSD nkcpopupEmoticonSlotSD = this.m_lstNKCPopupEmoticonSlotSDLeft[presetIndex];
			nkcpopupEmoticonSlotSD.StopSDAni();
			nkcpopupEmoticonSlotSD.SetUI(emoticonId);
			nkcpopupEmoticonSlotSD.PlayChangeEffect();
			NKCEmoticonManager.m_lstAniPreset[presetIndex] = emoticonId;
			this.UpdateSDCollectionExceptPreset();
			int index = (presetIndex + 1) % NKCEmoticonManager.m_lstAniPreset.Count;
			nkcpopupEmoticonSlotSD.SetSelected(false);
			this.m_lstNKCPopupEmoticonSlotSDLeft[index].SetSelected(true);
			this.UpdateSDCollectionUI(true);
		}

		// Token: 0x06007325 RID: 29477 RVA: 0x00264724 File Offset: 0x00262924
		private void OnClickRightSideComment(int emoticonID)
		{
			this.m_RightSideSelectedEmoticonIDComment = emoticonID;
			this.m_RightSidePreviewEmoticonIDComment = emoticonID;
			for (int i = 0; i < this.m_lstSlotComment.Count; i++)
			{
				NKCPopupEmoticonSlotComment nkcpopupEmoticonSlotComment = this.m_lstSlotComment[i];
				if (nkcpopupEmoticonSlotComment != null)
				{
					if (nkcpopupEmoticonSlotComment.GetEmoticonID() == this.m_RightSideSelectedEmoticonIDComment)
					{
						if (!nkcpopupEmoticonSlotComment.GetSelected())
						{
							nkcpopupEmoticonSlotComment.SetSelected(true);
						}
					}
					else
					{
						nkcpopupEmoticonSlotComment.SetSelected(false);
					}
				}
			}
			this.UpdateCommentCollectionUI(false);
		}

		// Token: 0x06007326 RID: 29478 RVA: 0x00264798 File Offset: 0x00262998
		private void OnClickRightSideCommentForChange(int emoticonID)
		{
			for (int i = 0; i < this.m_lstSlotComment.Count; i++)
			{
				NKCPopupEmoticonSlotComment nkcpopupEmoticonSlotComment = this.m_lstSlotComment[i];
				if (nkcpopupEmoticonSlotComment != null && nkcpopupEmoticonSlotComment.GetEmoticonID() == this.m_RightSideSelectedEmoticonIDComment && nkcpopupEmoticonSlotComment.GetSelected())
				{
					int num = -1;
					for (int j = 0; j < this.m_lstNKCPopupEmoticonSlotCommentLeft.Count; j++)
					{
						NKCPopupEmoticonSlotComment nkcpopupEmoticonSlotComment2 = this.m_lstNKCPopupEmoticonSlotCommentLeft[j];
						if (!(nkcpopupEmoticonSlotComment2 == null) && nkcpopupEmoticonSlotComment2.GetSelected())
						{
							num = j;
							break;
						}
					}
					if (num >= 0)
					{
						NKCPacketSender.Send_NKMPacket_EMOTICON_TEXT_CHANGE_REQ(num, emoticonID);
					}
				}
			}
		}

		// Token: 0x06007327 RID: 29479 RVA: 0x00264834 File Offset: 0x00262A34
		public void OnRecv(NKMPacket_EMOTICON_TEXT_CHANGE_ACK cNKMPacket_EMOTICON_TEXT_CHANGE_ACK)
		{
			int presetIndex = cNKMPacket_EMOTICON_TEXT_CHANGE_ACK.presetIndex;
			int emoticonId = cNKMPacket_EMOTICON_TEXT_CHANGE_ACK.emoticonId;
			if (presetIndex < 0 || presetIndex >= NKCEmoticonManager.m_lstTextPreset.Count)
			{
				Debug.LogError("EMOTICON_TEXT_CHANGE_ACK preset index invalid, index : " + presetIndex.ToString());
				return;
			}
			if (NKMEmoticonTemplet.Find(emoticonId) == null)
			{
				Debug.LogError("EMOTICON_TEXT_CHANGE_ACK emoticon ID is invalid, ID : " + emoticonId.ToString());
				return;
			}
			this.m_RightSideSelectedEmoticonIDComment = -1;
			NKCPopupEmoticonSlotComment nkcpopupEmoticonSlotComment = this.m_lstNKCPopupEmoticonSlotCommentLeft[presetIndex];
			nkcpopupEmoticonSlotComment.SetUI(emoticonId);
			nkcpopupEmoticonSlotComment.PlayChangeEffect();
			NKCEmoticonManager.m_lstTextPreset[presetIndex] = emoticonId;
			this.UpdateCommentCollectionExceptPreset();
			int index = (presetIndex + 1) % NKCEmoticonManager.m_lstTextPreset.Count;
			nkcpopupEmoticonSlotComment.SetSelected(false);
			NKCPopupEmoticonSlotComment nkcpopupEmoticonSlotComment2 = this.m_lstNKCPopupEmoticonSlotCommentLeft[index];
			this.m_RightSidePreviewEmoticonIDComment = nkcpopupEmoticonSlotComment2.GetEmoticonID();
			nkcpopupEmoticonSlotComment2.SetSelected(true);
			this.UpdateCommentCollectionUI(true);
		}

		// Token: 0x06007328 RID: 29480 RVA: 0x00264904 File Offset: 0x00262B04
		private void SetRightSidePage(NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE ePage, string emptyNoticeString = "")
		{
			if (!string.IsNullOrWhiteSpace(emptyNoticeString))
			{
				this.m_EmptyNoticeString = NKCStringTable.GetString(emptyNoticeString, false);
			}
			else
			{
				this.m_EmptyNoticeString = "";
			}
			this.m_eRightSidePage = ePage;
			this.SetRightSidePageUI();
		}

		// Token: 0x06007329 RID: 29481 RVA: 0x00264938 File Offset: 0x00262B38
		private void SetRightSidePageUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objEmptyNotice, this.m_eRightSidePage == NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_NONE || this.m_eRightSidePage == NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_TEXT);
			NKCUtil.SetGameobjectActive(this.m_objSDCollection, this.m_eRightSidePage == NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_SD);
			NKCUtil.SetGameobjectActive(this.m_objTextCollection, this.m_eRightSidePage == NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_TEXT);
			if (this.m_eRightSidePage == NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_SD)
			{
				this.UpdateSDCollectionUI(false);
				return;
			}
			if (this.m_eRightSidePage == NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_TEXT)
			{
				this.UpdateCommentCollectionUI(false);
				NKCUtil.SetLabelText(this.m_lbEmptyNotice, this.m_EmptyNoticeString);
				return;
			}
			NKCUtil.SetLabelText(this.m_lbEmptyNotice, this.m_EmptyNoticeString);
		}

		// Token: 0x0600732A RID: 29482 RVA: 0x002649D0 File Offset: 0x00262BD0
		private void UpdateSDCollectionUI(bool bResetPos = false)
		{
			this.m_lvsrSD.TotalCount = this.m_lstSDCollectionExceptPreset.Count;
			if (this.m_bFirstOpenPageSD)
			{
				this.m_bFirstOpenPageSD = false;
				this.m_lvsrSD.PrepareCells(0);
				this.m_lvsrSD.SetIndexPosition(0);
				return;
			}
			if (bResetPos)
			{
				this.m_lvsrSD.SetIndexPosition(0);
				return;
			}
			this.m_lvsrSD.RefreshCells(false);
		}

		// Token: 0x0600732B RID: 29483 RVA: 0x00264A38 File Offset: 0x00262C38
		private void UpdateCommentCollectionUI(bool bResetPos = false)
		{
			this.m_lvsrComment.TotalCount = this.m_lstCommentCollectionExceptPreset.Count;
			if (this.m_bFirstOpenPageComment)
			{
				this.m_bFirstOpenPageComment = false;
				this.m_lvsrComment.PrepareCells(0);
				this.m_lvsrComment.SetIndexPosition(0);
			}
			else if (bResetPos)
			{
				this.m_lvsrComment.SetIndexPosition(0);
			}
			else
			{
				this.m_lvsrComment.RefreshCells(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objCommentPreview, this.m_RightSidePreviewEmoticonIDComment > 0);
			NKCUtil.SetGameobjectActive(this.m_objCommentPreviewNone, this.m_RightSidePreviewEmoticonIDComment <= 0);
			if (this.m_RightSidePreviewEmoticonIDComment > 0)
			{
				this.m_NKCGameHudEmoticonCommentPreview.PlayPreview(this.m_RightSidePreviewEmoticonIDComment);
			}
		}

		// Token: 0x0600732C RID: 29484 RVA: 0x00264AE6 File Offset: 0x00262CE6
		private void OnClickLeftEmoticon(NKCUISlot.SlotData slotData, bool bLocked)
		{
			if (slotData != null)
			{
				this.OnClickLeftEmoticon(slotData.ID);
			}
		}

		// Token: 0x0600732D RID: 29485 RVA: 0x00264AF8 File Offset: 0x00262CF8
		private void OnClickLeftEmoticon(int emoticonID)
		{
			for (int i = 0; i < this.m_lstNKCPopupEmoticonSlotSDLeft.Count; i++)
			{
				NKCPopupEmoticonSlotSD nkcpopupEmoticonSlotSD = this.m_lstNKCPopupEmoticonSlotSDLeft[i];
				if (!(nkcpopupEmoticonSlotSD == null))
				{
					if (nkcpopupEmoticonSlotSD.GetEmoticonID() == emoticonID)
					{
						nkcpopupEmoticonSlotSD.SetSelected(true);
						nkcpopupEmoticonSlotSD.PlaySDAni();
						nkcpopupEmoticonSlotSD.transform.SetAsLastSibling();
						if (this.m_lstSDCollectionExceptPreset.Count > 0)
						{
							this.SetRightSidePage(NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_SD, "");
						}
						else
						{
							this.SetRightSidePage(NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_NONE, "SI_DP_EMOTICON_HAVE_NO_EMOTICON");
						}
					}
					else
					{
						nkcpopupEmoticonSlotSD.SetSelected(false);
						nkcpopupEmoticonSlotSD.StopSDAni();
					}
				}
			}
			for (int j = 0; j < this.m_lstNKCPopupEmoticonSlotCommentLeft.Count; j++)
			{
				NKCPopupEmoticonSlotComment nkcpopupEmoticonSlotComment = this.m_lstNKCPopupEmoticonSlotCommentLeft[j];
				if (!(nkcpopupEmoticonSlotComment == null))
				{
					if (nkcpopupEmoticonSlotComment.GetEmoticonID() == emoticonID)
					{
						this.m_RightSidePreviewEmoticonIDComment = emoticonID;
						nkcpopupEmoticonSlotComment.SetSelected(true);
						if (this.m_lstCommentCollectionExceptPreset.Count > 0)
						{
							this.SetRightSidePage(NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_TEXT, "");
						}
						else
						{
							this.SetRightSidePage(NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_TEXT, "SI_DP_EMOTICON_HAVE_NO_COMMENT");
						}
					}
					else
					{
						nkcpopupEmoticonSlotComment.SetSelected(false);
					}
				}
			}
		}

		// Token: 0x0600732E RID: 29486 RVA: 0x00264BFD File Offset: 0x00262DFD
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600732F RID: 29487 RVA: 0x00264C0B File Offset: 0x00262E0B
		public void Open()
		{
			base.UIOpened(true);
			this.SetUIWhenOpen();
		}

		// Token: 0x06007330 RID: 29488 RVA: 0x00264C1C File Offset: 0x00262E1C
		public void SetUIWhenOpen()
		{
			for (int i = 0; i < this.m_lstNKCPopupEmoticonSlotSDLeft.Count; i++)
			{
				NKCPopupEmoticonSlotSD nkcpopupEmoticonSlotSD = this.m_lstNKCPopupEmoticonSlotSDLeft[i];
				if (!(nkcpopupEmoticonSlotSD == null))
				{
					if (i < NKCEmoticonManager.m_lstAniPreset.Count)
					{
						NKCUtil.SetGameobjectActive(nkcpopupEmoticonSlotSD, true);
						nkcpopupEmoticonSlotSD.SetUI(NKCEmoticonManager.m_lstAniPreset[i]);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstNKCPopupEmoticonSlotSDLeft[i], false);
					}
				}
			}
			for (int j = 0; j < this.m_lstNKCPopupEmoticonSlotCommentLeft.Count; j++)
			{
				NKCPopupEmoticonSlotComment nkcpopupEmoticonSlotComment = this.m_lstNKCPopupEmoticonSlotCommentLeft[j];
				if (!(nkcpopupEmoticonSlotComment == null))
				{
					if (j < NKCEmoticonManager.m_lstTextPreset.Count)
					{
						NKCUtil.SetGameobjectActive(nkcpopupEmoticonSlotComment, true);
						nkcpopupEmoticonSlotComment.SetUI(NKCEmoticonManager.m_lstTextPreset[j]);
					}
					else
					{
						NKCUtil.SetGameobjectActive(nkcpopupEmoticonSlotComment, false);
					}
				}
			}
			this.UpdateSDCollectionExceptPreset();
			this.UpdateCommentCollectionExceptPreset();
			this.OnClickLeftEmoticon(0);
			this.SetRightSidePage(NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE.NPESRSP_NONE, "SI_DP_EMOTICON_SELECT_SLOT");
			this.m_RightSideSelectedEmoticonIDSD = -1;
			this.m_RightSideSelectedEmoticonIDComment = -1;
			this.m_RightSidePreviewEmoticonIDComment = -1;
			for (int k = 0; k < this.m_lstSlotSD.Count; k++)
			{
				NKCPopupEmoticonSlotSD nkcpopupEmoticonSlotSD2 = this.m_lstSlotSD[k];
				if (nkcpopupEmoticonSlotSD2 != null)
				{
					nkcpopupEmoticonSlotSD2.StopSDAni();
				}
			}
		}

		// Token: 0x04005F0B RID: 24331
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EMOTICON";

		// Token: 0x04005F0C RID: 24332
		private const string UI_ASSET_NAME = "NKM_UI_EMOTICON_DECK_POPUP";

		// Token: 0x04005F0D RID: 24333
		private static NKCPopupEmoticonSetting m_Instance;

		// Token: 0x04005F0E RID: 24334
		[Header("공통")]
		public EventTrigger m_etBG;

		// Token: 0x04005F0F RID: 24335
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04005F10 RID: 24336
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04005F11 RID: 24337
		[Header("왼쪽")]
		public List<NKCPopupEmoticonSlotSD> m_lstNKCPopupEmoticonSlotSDLeft;

		// Token: 0x04005F12 RID: 24338
		public List<NKCPopupEmoticonSlotComment> m_lstNKCPopupEmoticonSlotCommentLeft;

		// Token: 0x04005F13 RID: 24339
		[Header("오른쪽")]
		public GameObject m_objEmptyNotice;

		// Token: 0x04005F14 RID: 24340
		public Text m_lbEmptyNotice;

		// Token: 0x04005F15 RID: 24341
		private string m_EmptyNoticeString = "";

		// Token: 0x04005F16 RID: 24342
		public GameObject m_objSDCollection;

		// Token: 0x04005F17 RID: 24343
		public LoopVerticalScrollRect m_lvsrSD;

		// Token: 0x04005F18 RID: 24344
		public GameObject m_objTextCollection;

		// Token: 0x04005F19 RID: 24345
		public GameObject m_objCommentPreviewNone;

		// Token: 0x04005F1A RID: 24346
		public GameObject m_objCommentPreview;

		// Token: 0x04005F1B RID: 24347
		public NKCGameHudEmoticonComment m_NKCGameHudEmoticonCommentPreview;

		// Token: 0x04005F1C RID: 24348
		public LoopVerticalScrollRect m_lvsrComment;

		// Token: 0x04005F1D RID: 24349
		private NKCPopupEmoticonSetting.NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE m_eRightSidePage;

		// Token: 0x04005F1E RID: 24350
		private bool m_bFirstOpenPageSD = true;

		// Token: 0x04005F1F RID: 24351
		private bool m_bFirstOpenPageComment = true;

		// Token: 0x04005F20 RID: 24352
		private int m_RightSideSelectedEmoticonIDSD = -1;

		// Token: 0x04005F21 RID: 24353
		private int m_RightSideSelectedEmoticonIDComment = -1;

		// Token: 0x04005F22 RID: 24354
		private int m_RightSidePreviewEmoticonIDComment = -1;

		// Token: 0x04005F23 RID: 24355
		private List<NKCPopupEmoticonSlotSD> m_lstSlotSD = new List<NKCPopupEmoticonSlotSD>();

		// Token: 0x04005F24 RID: 24356
		private List<NKCPopupEmoticonSlotComment> m_lstSlotComment = new List<NKCPopupEmoticonSlotComment>();

		// Token: 0x04005F25 RID: 24357
		private List<int> m_lstSDCollectionExceptPreset = new List<int>();

		// Token: 0x04005F26 RID: 24358
		private List<int> m_lstCommentCollectionExceptPreset = new List<int>();

		// Token: 0x04005F27 RID: 24359
		private const int SD_SELECTED_SORT_LAYER = 101;

		// Token: 0x02001787 RID: 6023
		public enum NKC_POPUP_EMOTICON_SETTING_RIGHT_SIDE_PAGE
		{
			// Token: 0x0400A705 RID: 42757
			NPESRSP_NONE,
			// Token: 0x0400A706 RID: 42758
			NPESRSP_SD,
			// Token: 0x0400A707 RID: 42759
			NPESRSP_TEXT
		}
	}
}
