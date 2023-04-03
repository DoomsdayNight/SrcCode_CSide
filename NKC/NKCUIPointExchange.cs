using System;
using System.Linq;
using Cs.Logging;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009C9 RID: 2505
	public class NKCUIPointExchange : MonoBehaviour
	{
		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06006ADF RID: 27359 RVA: 0x0022B861 File Offset: 0x00229A61
		// (set) Token: 0x06006AE0 RID: 27360 RVA: 0x0022B869 File Offset: 0x00229A69
		public bool PlayingBGM { get; private set; }

		// Token: 0x06006AE1 RID: 27361 RVA: 0x0022B874 File Offset: 0x00229A74
		public void Init(NKCUIPointExchange.OnClose onClose, NKCUIPointExchange.OnInformation onInformation)
		{
			this.m_pointExchangeTemplet = NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime);
			if (this.m_loopScrollRect != null)
			{
				this.m_loopScrollRect.dOnGetObject += this.GetPresetSlot;
				this.m_loopScrollRect.dOnReturnObject += this.ReturnPresetSlot;
				this.m_loopScrollRect.dOnProvideData += this.ProvidePresetData;
				this.m_loopScrollRect.ContentConstraintCount = 1;
				this.m_loopScrollRect.TotalCount = 1;
				this.m_loopScrollRect.PrepareCells(0);
			}
			if (this.m_pointExchangeTemplet != null)
			{
				if (NKCSynchronizedTime.GetTimeLeft(NKMTime.LocalToUTC(this.m_pointExchangeTemplet.EndDate, 0)).TotalDays > (double)NKCSynchronizedTime.UNLIMITD_REMAIN_DAYS)
				{
					NKCUtil.SetLabelText(this.m_lbEventTime, NKCUtilString.GET_STRING_EVENT_DATE_UNLIMITED_TEXT);
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbEventTime, NKCUtilString.GetTimeIntervalString(this.m_pointExchangeTemplet.StartDate, this.m_pointExchangeTemplet.EndDate, NKMTime.INTERVAL_FROM_UTC, false));
				}
			}
			int num = (this.m_pointExchangeTemplet != null) ? this.m_pointExchangeTemplet.UsePointId.Count : 0;
			int num2 = (this.m_pointCoinInfoArray != null) ? this.m_pointCoinInfoArray.Length : 0;
			NKMUserData count = NKCScenManager.CurrentUserData();
			for (int i = 0; i < num2; i++)
			{
				if (i >= num)
				{
					NKCUtil.SetGameobjectActive(this.m_pointCoinInfoArray[i].objRoot, false);
				}
				else
				{
					this.m_pointCoinInfoArray[i].Init(this.m_pointExchangeTemplet.UsePointId[i]);
					this.m_pointCoinInfoArray[i].SetCount(count);
				}
			}
			this.m_dOnClose = onClose;
			this.m_dOnInformation = onInformation;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.OnClickClose));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnMissionInfo, new UnityAction(this.OnClickMissionInformation));
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x0022BA4C File Offset: 0x00229C4C
		public void ResetUI()
		{
			this.RefreshPoint();
			LoopScrollFlexibleRect loopScrollRect = this.m_loopScrollRect;
			if (loopScrollRect == null)
			{
				return;
			}
			loopScrollRect.SetIndexPosition(0);
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x0022BA68 File Offset: 0x00229C68
		public void RefreshPoint()
		{
			int num = (this.m_pointCoinInfoArray != null) ? this.m_pointCoinInfoArray.Length : 0;
			NKMUserData count = NKCScenManager.CurrentUserData();
			for (int i = 0; i < num; i++)
			{
				this.m_pointCoinInfoArray[i].SetCount(count);
			}
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x0022BAAD File Offset: 0x00229CAD
		public void RefreshScrollRect()
		{
			LoopScrollFlexibleRect loopScrollRect = this.m_loopScrollRect;
			if (loopScrollRect == null)
			{
				return;
			}
			loopScrollRect.RefreshCells(false);
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x0022BAC0 File Offset: 0x00229CC0
		public void PlayMusic()
		{
			if (!string.IsNullOrEmpty(this.m_bgmName) && !this.PlayingBGM)
			{
				NKCSoundManager.PlayMusic(this.m_bgmName, true, 1f, false, 0f, 0f);
				this.PlayingBGM = true;
			}
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x0022BAFA File Offset: 0x00229CFA
		public void RevertMusic()
		{
			if (this.PlayingBGM)
			{
				NKCSoundManager.PlayScenMusic();
				this.PlayingBGM = false;
			}
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x0022BB10 File Offset: 0x00229D10
		private RectTransform GetPresetSlot(int index)
		{
			if (this.m_pointExchangeTemplet == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(this.m_pointExchangeTemplet.PrefabId))
			{
				return null;
			}
			string text;
			string text2;
			if (this.m_pointExchangeTemplet.BannerId.Contains('@'))
			{
				NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(this.m_pointExchangeTemplet.PrefabId, this.m_pointExchangeTemplet.PrefabId);
				text = nkmassetName.m_BundleName;
				text2 = nkmassetName.m_AssetName;
			}
			else
			{
				text = this.m_pointExchangeTemplet.PrefabId;
				text2 = this.m_pointExchangeTemplet.PrefabId;
			}
			if (text == null || text2 == null)
			{
				return null;
			}
			NKCUIPointExchangeSlot newInstance = NKCUIPointExchangeSlot.GetNewInstance(null, text, text2 + "_SLOT_ALL");
			if (newInstance == null)
			{
				return null;
			}
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x0022BBBC File Offset: 0x00229DBC
		private void ReturnPresetSlot(Transform tr)
		{
			NKCUIPointExchangeSlot component = tr.GetComponent<NKCUIPointExchangeSlot>();
			tr.SetParent(null);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x0022BBF4 File Offset: 0x00229DF4
		private void ProvidePresetData(Transform tr, int index)
		{
			NKCUIPointExchangeSlot component = tr.GetComponent<NKCUIPointExchangeSlot>();
			if (component == null)
			{
				return;
			}
			if (this.m_pointExchangeTemplet == null)
			{
				return;
			}
			component.SetData(this.m_pointExchangeTemplet);
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x0022BC27 File Offset: 0x00229E27
		private void OnClickClose()
		{
			if (this.m_dOnClose != null)
			{
				this.m_dOnClose();
			}
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x0022BC3C File Offset: 0x00229E3C
		private void OnClickMissionInformation()
		{
			if (this.m_dOnInformation != null)
			{
				this.m_dOnInformation();
			}
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x0022BC54 File Offset: 0x00229E54
		private void OnDestroy()
		{
			this.m_pointExchangeTemplet = null;
			if (this.m_pointCoinInfoArray != null)
			{
				int num = this.m_pointCoinInfoArray.Length;
				for (int i = 0; i < num; i++)
				{
					this.m_pointCoinInfoArray[i].Release();
				}
			}
			this.m_dOnClose = null;
			this.m_dOnInformation = null;
		}

		// Token: 0x04005691 RID: 22161
		public NKCUIPointExchange.PointCoinInfo[] m_pointCoinInfoArray;

		// Token: 0x04005692 RID: 22162
		public Text m_lbEventTime;

		// Token: 0x04005693 RID: 22163
		public LoopScrollFlexibleRect m_loopScrollRect;

		// Token: 0x04005694 RID: 22164
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04005695 RID: 22165
		public NKCUIComStateButton m_csbtnMissionInfo;

		// Token: 0x04005696 RID: 22166
		public string m_bgmName;

		// Token: 0x04005698 RID: 22168
		private NKMPointExchangeTemplet m_pointExchangeTemplet;

		// Token: 0x04005699 RID: 22169
		private NKCUIPointExchange.OnClose m_dOnClose;

		// Token: 0x0400569A RID: 22170
		private NKCUIPointExchange.OnInformation m_dOnInformation;

		// Token: 0x020016CC RID: 5836
		[Serializable]
		public struct PointCoinInfo
		{
			// Token: 0x0600B157 RID: 45399 RVA: 0x003602F8 File Offset: 0x0035E4F8
			public void Init(int pointCoinId)
			{
				this.itemId = pointCoinId;
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(pointCoinId);
				if (nkmitemMiscTemplet == null)
				{
					Log.Debug(string.Format("PointExchange PointItem (Id: {0}) is not exist", pointCoinId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIPointExchange.cs", 32);
					NKCUtil.SetGameobjectActive(this.objRoot, false);
					return;
				}
				NKCUtil.SetGameobjectActive(this.objRoot, true);
				NKCUtil.SetImageSprite(this.pointCoinImage, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(nkmitemMiscTemplet), false);
				if (this.imageButton != null)
				{
					this.imageButton.PointerDown.RemoveAllListeners();
					this.imageButton.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnImageDown));
				}
			}

			// Token: 0x0600B158 RID: 45400 RVA: 0x003603A4 File Offset: 0x0035E5A4
			public void SetCount(NKMUserData userData)
			{
				if (!this.objRoot.activeSelf)
				{
					return;
				}
				if (userData != null)
				{
					NKCUtil.SetLabelText(this.pointCountText, userData.m_InventoryData.GetCountMiscItem(this.itemId).ToString());
				}
			}

			// Token: 0x0600B159 RID: 45401 RVA: 0x003603E6 File Offset: 0x0035E5E6
			public void Release()
			{
				this.slotData = null;
			}

			// Token: 0x0600B15A RID: 45402 RVA: 0x003603F0 File Offset: 0x0035E5F0
			private void OnImageDown(PointerEventData eventData)
			{
				if (this.slotData == null)
				{
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData == null)
					{
						return;
					}
					long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(this.itemId);
					this.slotData = NKCUISlot.SlotData.MakeMiscItemData(this.itemId, countMiscItem, 0);
				}
				NKCUITooltip.Instance.Open(this.slotData, new Vector2?(eventData.position));
			}

			// Token: 0x0400A53B RID: 42299
			public GameObject objRoot;

			// Token: 0x0400A53C RID: 42300
			public Image pointCoinImage;

			// Token: 0x0400A53D RID: 42301
			public Text pointCountText;

			// Token: 0x0400A53E RID: 42302
			public NKCUIComStateButton imageButton;

			// Token: 0x0400A53F RID: 42303
			private int itemId;

			// Token: 0x0400A540 RID: 42304
			private NKCUISlot.SlotData slotData;
		}

		// Token: 0x020016CD RID: 5837
		// (Invoke) Token: 0x0600B15C RID: 45404
		public delegate void OnClose();

		// Token: 0x020016CE RID: 5838
		// (Invoke) Token: 0x0600B160 RID: 45408
		public delegate void OnInformation();
	}
}
