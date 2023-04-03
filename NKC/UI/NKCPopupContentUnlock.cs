using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A37 RID: 2615
	public class NKCPopupContentUnlock : NKCUIBase
	{
		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06007285 RID: 29317 RVA: 0x00260A88 File Offset: 0x0025EC88
		public static NKCPopupContentUnlock instance
		{
			get
			{
				if (NKCPopupContentUnlock.m_Instance == null)
				{
					NKCPopupContentUnlock.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupContentUnlock>("ab_ui_nkm_ui_unlock", "NKM_UI_POPUP_CONTENT_UNLOCK", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupContentUnlock.CleanupInstance)).GetInstance<NKCPopupContentUnlock>();
				}
				return NKCPopupContentUnlock.m_Instance;
			}
		}

		// Token: 0x06007286 RID: 29318 RVA: 0x00260AC2 File Offset: 0x0025ECC2
		private static void CleanupInstance()
		{
			NKCPopupContentUnlock.m_Instance = null;
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06007287 RID: 29319 RVA: 0x00260ACA File Offset: 0x0025ECCA
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06007288 RID: 29320 RVA: 0x00260ACD File Offset: 0x0025ECCD
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x00260AD4 File Offset: 0x0025ECD4
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_fDeltaTime = 0f;
			NKCPopupContentUnlock.OnClose onClose = this.dOnClose;
			if (onClose == null)
			{
				return;
			}
			onClose();
		}

		// Token: 0x0600728A RID: 29322 RVA: 0x00260B00 File Offset: 0x0025ED00
		private void Init()
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnUserInputEvent));
			EventTrigger eventTrigger = this.m_objBG.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = this.m_objBG.AddComponent<EventTrigger>();
			}
			eventTrigger.triggers.Clear();
			eventTrigger.triggers.Add(entry);
			this.m_bInitComplete = true;
		}

		// Token: 0x0600728B RID: 29323 RVA: 0x00260B7C File Offset: 0x0025ED7C
		public void Open(NKCContentManager.NKCUnlockableContent unlockableContent, NKCPopupContentUnlock.OnClose onClose)
		{
			if (!this.m_bInitComplete)
			{
				this.Init();
			}
			this.dOnClose = onClose;
			if (unlockableContent == null)
			{
				this.ForceClose();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objCounterCaseParent, false);
			NKCUtil.SetGameobjectActive(this.m_objEpisodeParent, false);
			NKCUtil.SetGameobjectActive(this.m_objActParent, false);
			NKCUtil.SetGameobjectActive(this.m_objNormalIconParent, string.IsNullOrEmpty(unlockableContent.m_PopupIconName));
			NKCUtil.SetGameobjectActive(this.m_objSpecialIconParent, !string.IsNullOrEmpty(unlockableContent.m_PopupIconName));
			if (this.m_objSpecialIconParent.activeSelf)
			{
				NKCUtil.SetImageSprite(this.m_imgSpecialIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(unlockableContent.m_PopupIconAssetBundleName, unlockableContent.m_PopupIconName, false), false);
			}
			ContentsType eContentsType = unlockableContent.m_eContentsType;
			if (eContentsType <= ContentsType.EPISODE)
			{
				if (eContentsType != ContentsType.ACT)
				{
					if (eContentsType != ContentsType.EPISODE)
					{
						goto IL_42B;
					}
				}
				else
				{
					NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(unlockableContent.m_ContentsValue);
					if (nkmstageTempletV == null || !nkmstageTempletV.EnableByTag)
					{
						this.ForceClose();
						return;
					}
					NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(nkmstageTempletV.EpisodeId, nkmstageTempletV.m_Difficulty);
					if (nkmepisodeTempletV == null || !nkmepisodeTempletV.EnableByTag)
					{
						this.ForceClose();
						return;
					}
					NKCUtil.SetGameobjectActive(this.m_objEpisodeParent, true);
					NKCUtil.SetGameobjectActive(this.m_objActParent, true);
					NKCUtil.SetImageSprite(this.m_imgMain, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_CONTENT_UNLOCK_TEXTURE", unlockableContent.m_PopupImageName, false), true);
					NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString(unlockableContent.m_PopupTitle, false));
					NKCUtil.SetLabelText(this.m_lbDesc, NKCStringTable.GetString(unlockableContent.m_PopupDesc, false));
					NKCUtil.SetLabelText(this.m_lbEpisodeTitle, nkmepisodeTempletV.GetEpisodeTitle());
					NKCUtil.SetLabelText(this.m_lbEpisodeName, nkmepisodeTempletV.GetEpisodeName());
					NKCUtil.SetLabelText(this.m_lbActNum, string.Format("{0}{1}", NKCStringTable.GetString("SI_DP_CONTENTS_UNLOCK_ACT_DISPLAY", false), nkmstageTempletV.ActId));
					goto IL_476;
				}
			}
			else if (eContentsType != ContentsType.COUNTERCASE_NEW_CHARACTER)
			{
				if (eContentsType != ContentsType.DUNGEON)
				{
					goto IL_42B;
				}
				NKMStageTempletV2 nkmstageTempletV2 = NKMStageTempletV2.Find(unlockableContent.m_ContentsValue);
				if (nkmstageTempletV2 == null || !nkmstageTempletV2.EnableByTag)
				{
					this.ForceClose();
					return;
				}
				NKMEpisodeTempletV2 nkmepisodeTempletV2 = NKMEpisodeTempletV2.Find(nkmstageTempletV2.EpisodeId, nkmstageTempletV2.m_Difficulty);
				if (nkmepisodeTempletV2 == null || !nkmepisodeTempletV2.EnableByTag)
				{
					this.ForceClose();
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objEpisodeParent, true);
				NKCUtil.SetImageSprite(this.m_imgMain, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_CONTENT_UNLOCK_TEXTURE", unlockableContent.m_PopupImageName, false), true);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString(unlockableContent.m_PopupTitle, false));
				NKCUtil.SetLabelText(this.m_lbDesc, NKCStringTable.GetString(unlockableContent.m_PopupDesc, false));
				NKCUtil.SetLabelText(this.m_lbEpisodeTitle, nkmepisodeTempletV2.GetEpisodeTitle());
				NKCUtil.SetLabelText(this.m_lbEpisodeName, nkmepisodeTempletV2.GetEpisodeName());
				goto IL_476;
			}
			else
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unlockableContent.m_ContentsValue);
				if (unitTempletBase == null || !unitTempletBase.CollectionEnableByTag)
				{
					this.ForceClose();
					return;
				}
				NKCUtil.SetGameobjectActive(this.m_objCounterCaseParent, true);
				NKCUtil.SetImageSprite(this.m_imgCounterCaseUnit, NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.FACE_CARD, unitTempletBase), false);
				NKCUtil.SetImageSprite(this.m_imgMain, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_CONTENT_UNLOCK_TEXTURE", "NKM_UI_CONTENTS_UNLOCK_COUNTERCASE", false), true);
				using (IEnumerator<NKMContentUnlockTemplet> enumerator = NKMTempletContainer<NKMContentUnlockTemplet>.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMContentUnlockTemplet nkmcontentUnlockTemplet = enumerator.Current;
						if (nkmcontentUnlockTemplet.m_eContentsType == ContentsType.COUNTERCASE)
						{
							NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString(nkmcontentUnlockTemplet.m_strPopupTitle, false));
							NKCUtil.SetLabelText(this.m_lbDesc, string.Format(NKCStringTable.GetString(nkmcontentUnlockTemplet.m_strPopupDesc, false), unitTempletBase.Name));
							break;
						}
					}
					goto IL_476;
				}
			}
			NKMStageTempletV2 nkmstageTempletV3 = NKMStageTempletV2.Find(unlockableContent.m_ContentsValue);
			if (nkmstageTempletV3 == null || !nkmstageTempletV3.EnableByTag)
			{
				this.ForceClose();
				return;
			}
			NKMEpisodeTempletV2 nkmepisodeTempletV3 = NKMEpisodeTempletV2.Find(nkmstageTempletV3.EpisodeId, nkmstageTempletV3.m_Difficulty);
			if (nkmepisodeTempletV3 == null || !nkmepisodeTempletV3.EnableByTag)
			{
				this.ForceClose();
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEpisodeParent, true);
			NKCUtil.SetImageSprite(this.m_imgMain, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_CONTENT_UNLOCK_TEXTURE", unlockableContent.m_PopupImageName, false), true);
			NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString(unlockableContent.m_PopupTitle, false));
			NKCUtil.SetLabelText(this.m_lbDesc, NKCStringTable.GetString(unlockableContent.m_PopupDesc, false));
			NKCUtil.SetLabelText(this.m_lbEpisodeTitle, nkmepisodeTempletV3.GetEpisodeTitle());
			NKCUtil.SetLabelText(this.m_lbEpisodeName, nkmepisodeTempletV3.GetEpisodeName());
			goto IL_476;
			IL_42B:
			NKCUtil.SetImageSprite(this.m_imgMain, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_CONTENT_UNLOCK_TEXTURE", unlockableContent.m_PopupImageName, false), true);
			NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString(unlockableContent.m_PopupTitle, false));
			NKCUtil.SetLabelText(this.m_lbDesc, NKCStringTable.GetString(unlockableContent.m_PopupDesc, false));
			IL_476:
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_bUserInput = false;
			this.m_fDeltaTime = 0f;
			base.UIOpened(true);
		}

		// Token: 0x0600728C RID: 29324 RVA: 0x00261034 File Offset: 0x0025F234
		private void ForceClose()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_fDeltaTime = 0f;
			NKCPopupContentUnlock.OnClose onClose = this.dOnClose;
			if (onClose == null)
			{
				return;
			}
			onClose();
		}

		// Token: 0x0600728D RID: 29325 RVA: 0x00261060 File Offset: 0x0025F260
		public void Update()
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_bUserInput)
				{
					base.Close();
				}
				if (this.m_fDeltaTime > this.m_fAutoCloseTime)
				{
					base.Close();
				}
			}
		}

		// Token: 0x0600728E RID: 29326 RVA: 0x002610AE File Offset: 0x0025F2AE
		public void OnUserInputEvent(BaseEventData eventData)
		{
		}

		// Token: 0x04005E61 RID: 24161
		private const string PREFAB_ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_unlock";

		// Token: 0x04005E62 RID: 24162
		private const string TEXTURE_ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_CONTENT_UNLOCK_TEXTURE";

		// Token: 0x04005E63 RID: 24163
		private const string ICON_ASSET_BUNDLE_NAME = "AB_INVEN_ICON_ITEM_MISC";

		// Token: 0x04005E64 RID: 24164
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONTENT_UNLOCK";

		// Token: 0x04005E65 RID: 24165
		private const string UI_CC_TEXTURE_ASSET_NAME = "NKM_UI_CONTENTS_UNLOCK_COUNTERCASE";

		// Token: 0x04005E66 RID: 24166
		private const string STR_CC_TITLE_KEY = "";

		// Token: 0x04005E67 RID: 24167
		private const string STR_CC_DESC_KEY = "";

		// Token: 0x04005E68 RID: 24168
		private static NKCPopupContentUnlock m_Instance;

		// Token: 0x04005E69 RID: 24169
		public GameObject m_objBG;

		// Token: 0x04005E6A RID: 24170
		public Animator m_Ani;

		// Token: 0x04005E6B RID: 24171
		public Image m_imgMain;

		// Token: 0x04005E6C RID: 24172
		public Text m_lbTitle;

		// Token: 0x04005E6D RID: 24173
		public Text m_lbDesc;

		// Token: 0x04005E6E RID: 24174
		public GameObject m_objCounterCaseParent;

		// Token: 0x04005E6F RID: 24175
		public Image m_imgCounterCaseUnit;

		// Token: 0x04005E70 RID: 24176
		public GameObject m_objEpisodeParent;

		// Token: 0x04005E71 RID: 24177
		public Text m_lbEpisodeTitle;

		// Token: 0x04005E72 RID: 24178
		public Text m_lbEpisodeName;

		// Token: 0x04005E73 RID: 24179
		public GameObject m_objActParent;

		// Token: 0x04005E74 RID: 24180
		public Text m_lbActNum;

		// Token: 0x04005E75 RID: 24181
		public GameObject m_objNormalIconParent;

		// Token: 0x04005E76 RID: 24182
		public GameObject m_objSpecialIconParent;

		// Token: 0x04005E77 RID: 24183
		public Image m_imgSpecialIcon;

		// Token: 0x04005E78 RID: 24184
		public float m_fAutoCloseTime = 3f;

		// Token: 0x04005E79 RID: 24185
		private NKCPopupContentUnlock.OnClose dOnClose;

		// Token: 0x04005E7A RID: 24186
		private float m_fDeltaTime;

		// Token: 0x04005E7B RID: 24187
		private bool m_bUserInput;

		// Token: 0x04005E7C RID: 24188
		private bool m_bInitComplete;

		// Token: 0x02001777 RID: 6007
		// (Invoke) Token: 0x0600B361 RID: 45921
		public delegate void OnClose();
	}
}
