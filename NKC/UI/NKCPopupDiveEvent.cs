using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A3C RID: 2620
	public class NKCPopupDiveEvent : NKCUIBase
	{
		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x060072C4 RID: 29380 RVA: 0x00261E02 File Offset: 0x00260002
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x060072C5 RID: 29381 RVA: 0x00261E05 File Offset: 0x00260005
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_DIVE_EVENT_POPUP;
			}
		}

		// Token: 0x060072C6 RID: 29382 RVA: 0x00261E0C File Offset: 0x0026000C
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			base.gameObject.SetActive(false);
			this.m_evtBG.triggers.Clear();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				base.Close();
			});
			this.m_evtBG.triggers.Add(entry);
		}

		// Token: 0x060072C7 RID: 29383 RVA: 0x00261E7C File Offset: 0x0026007C
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
				if (this.m_bAutoClose)
				{
					float num = Time.deltaTime;
					if (num > NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f)
					{
						num = NKCScenManager.GetScenManager().GetFixedFrameTime() * 2f;
					}
					this.m_fElapsedTimeToAutoClose += num;
					if (this.m_fElapsedTimeToAutoClose >= 5f / NKCClientConst.DiveAutoSpeed)
					{
						this.m_bAutoClose = false;
						base.Close();
					}
				}
			}
		}

		// Token: 0x060072C8 RID: 29384 RVA: 0x00261EFC File Offset: 0x002600FC
		public void Open(bool bAutoClose, NKMDiveSlot cNKMDiveSlot, NKMRewardData cRewardData, NKCPopupDiveEvent.OnCloseCallBack _OnCloseCallBack = null)
		{
			if (cNKMDiveSlot == null)
			{
				return;
			}
			this.m_bAutoClose = bAutoClose;
			this.m_fElapsedTimeToAutoClose = 0f;
			if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_ITEM)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_ITEM", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_UNIT)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_RESCUE_SIGNAL", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_REPAIR)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_REPAIR_KIT", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_SUPPLY)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_LOST_CONTAINER", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_ITEM)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_ITEM", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_UNIT)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_RESCUE_SIGNAL", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_REPAIR)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_REPAIR_KIT", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_LOSTSHIP_SUPPLY)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_LOST_CONTAINER", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_BEACON_DUNGEON)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_ENEMY_ATTACK", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, false);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_BEACON_BLANK)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_SAFETY", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, false);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_BEACON_ITEM)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_ITEM", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_BEACON_UNIT)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_RESCUE_SIGNAL", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			else if (cNKMDiveSlot.EventType == NKM_DIVE_EVENT_TYPE.NDET_BEACON_STORM)
			{
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_WEATHER", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, false);
			}
			else
			{
				if (cNKMDiveSlot.EventType != NKM_DIVE_EVENT_TYPE.NDET_ARTIFACT)
				{
					this.m_OnCloseCallBack = null;
					return;
				}
				this.m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL.texture = NKCResourceUtility.GetOrLoadAssetResource<Texture>("AB_UI_NKM_UI_WORLD_MAP_DIVE_EVENT_THUMBNAIL", "NKM_UI_WORLD_MAP_DIVE_EVENT_ITEM", false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_FX, true);
			}
			string text;
			string text2;
			NKCUtilString.GetDiveEventText(cNKMDiveSlot.EventType, out text, out text2);
			this.m_NKM_UI_DIVE_EVENT_POPUP_TITLE_TEXT.text = text;
			this.m_NKM_UI_DIVE_EVENT_POPUP_SUBTITLE_TEXT.text = text2;
			this.m_OnCloseCallBack = _OnCloseCallBack;
			if (cRewardData != null)
			{
				if (this.m_NKCUISlot == null)
				{
					this.m_NKCUISlot = NKCUISlot.GetNewInstance(this.m_NKM_UI_DIVE_EVENT_POPUP_ICON_SLOT_AREA.transform);
					this.m_NKCUISlot.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
					this.m_NKCUISlot.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
					this.m_NKCUISlot.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.m_NKCUISlot.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				List<NKCUISlot.SlotData> list = NKCUISlot.MakeSlotDataListFromReward(cRewardData, false, false);
				if (list.Count > 0)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_ICON_SLOT_AREA, true);
					this.m_NKCUISlot.SetData(list[0], true, null);
					NKCUtil.SetGameobjectActive(this.m_NKCUISlot, true);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_ICON_SLOT_AREA, false);
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_EVENT_POPUP_ICON_SLOT_AREA, false);
			}
			base.UIOpened(true);
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x00262351 File Offset: 0x00260551
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			if (this.m_OnCloseCallBack != null)
			{
				this.m_OnCloseCallBack();
			}
			this.m_OnCloseCallBack = null;
		}

		// Token: 0x04005EB1 RID: 24241
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_DIVE";

		// Token: 0x04005EB2 RID: 24242
		public const string UI_ASSET_NAME = "NKM_UI_DIVE_EVENT_POPUP";

		// Token: 0x04005EB3 RID: 24243
		public Text m_NKM_UI_DIVE_EVENT_POPUP_TITLE_TEXT;

		// Token: 0x04005EB4 RID: 24244
		public Text m_NKM_UI_DIVE_EVENT_POPUP_SUBTITLE_TEXT;

		// Token: 0x04005EB5 RID: 24245
		public RawImage m_NKM_UI_DIVE_EVENT_POPUP_THUMBNAIL;

		// Token: 0x04005EB6 RID: 24246
		public GameObject m_NKM_UI_DIVE_EVENT_POPUP_ICON_SLOT_AREA;

		// Token: 0x04005EB7 RID: 24247
		public GameObject m_NKM_UI_DIVE_EVENT_POPUP_FX;

		// Token: 0x04005EB8 RID: 24248
		public EventTrigger m_evtBG;

		// Token: 0x04005EB9 RID: 24249
		private NKCUISlot m_NKCUISlot;

		// Token: 0x04005EBA RID: 24250
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005EBB RID: 24251
		private NKCPopupDiveEvent.OnCloseCallBack m_OnCloseCallBack;

		// Token: 0x04005EBC RID: 24252
		private bool m_bAutoClose;

		// Token: 0x04005EBD RID: 24253
		private float m_fElapsedTimeToAutoClose;

		// Token: 0x0200177D RID: 6013
		// (Invoke) Token: 0x0600B379 RID: 45945
		public delegate void OnCloseCallBack();
	}
}
