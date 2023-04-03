using System;
using NKM;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A42 RID: 2626
	public class NKCPopupEmoticonSlotSD : MonoBehaviour
	{
		// Token: 0x06007333 RID: 29491 RVA: 0x00264DD0 File Offset: 0x00262FD0
		public static NKCPopupEmoticonSlotSD GetNewInstance(Transform parent)
		{
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_EMOTICON", "NKM_UI_EMOTICON_SLOT_SD", false, null);
			NKCPopupEmoticonSlotSD component = nkcassetInstanceData.m_Instant.GetComponent<NKCPopupEmoticonSlotSD>();
			if (component == null)
			{
				NKCAssetResourceManager.CloseInstance(nkcassetInstanceData);
				Debug.LogError("NKCPopupEmoticonSlotSD Prefab null!");
				return null;
			}
			component.m_cNKCAssetInstanceData = nkcassetInstanceData;
			if (parent != null)
			{
				component.transform.SetParent(parent);
			}
			component.transform.localPosition = new Vector3(component.transform.localPosition.x, component.transform.localPosition.y, 0f);
			component.gameObject.SetActive(false);
			return component;
		}

		// Token: 0x06007334 RID: 29492 RVA: 0x00264E74 File Offset: 0x00263074
		public void MakeCanvas()
		{
			if (this.m_Canvas == null)
			{
				this.m_Canvas = this.m_objSDRoot.AddComponent<Canvas>();
				this.m_Canvas.pixelPerfect = false;
				this.m_Canvas.overrideSorting = true;
				this.m_Canvas.sortingLayerName = "GAME_UI_FRONT";
			}
			if (this.m_GraphicRaycaster == null)
			{
				this.m_GraphicRaycaster = this.m_objSDRoot.AddComponent<GraphicRaycaster>();
			}
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x00264EE7 File Offset: 0x002630E7
		public void RemoveCanvas()
		{
			UnityEngine.Object.Destroy(this.m_GraphicRaycaster);
			this.m_GraphicRaycaster = null;
			UnityEngine.Object.Destroy(this.m_Canvas);
			this.m_Canvas = null;
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x00264F0D File Offset: 0x0026310D
		public void ResetCanvasLayer(int layer = 100)
		{
			if (this.m_Canvas != null)
			{
				this.m_Canvas.sortingOrder = layer;
			}
		}

		// Token: 0x06007337 RID: 29495 RVA: 0x00264F29 File Offset: 0x00263129
		private void ClearReusableData()
		{
			if (this.m_cNKCAssetInstanceDataEmoticonSD != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_cNKCAssetInstanceDataEmoticonSD);
				this.m_cNKCAssetInstanceDataEmoticonSD = null;
			}
			this.m_SkeletonGraphicEmoticon = null;
			if (this.m_PrevSoundID > 0)
			{
				NKCSoundManager.StopSound(this.m_PrevSoundID);
			}
			this.m_PrevSoundID = -1;
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x00264F67 File Offset: 0x00263167
		public void PlayChangeEffect()
		{
			this.m_amtorChangeEffect.Play("NKM_UI_EMOTICON_CHANGE_BASE", -1, 0f);
		}

		// Token: 0x06007339 RID: 29497 RVA: 0x00264F7F File Offset: 0x0026317F
		private void OnDestroy()
		{
			if (this.m_cNKCAssetInstanceData != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_cNKCAssetInstanceData);
				this.m_cNKCAssetInstanceData = null;
			}
			this.ClearReusableData();
		}

		// Token: 0x0600733A RID: 29498 RVA: 0x00264FA1 File Offset: 0x002631A1
		public void Reset_SD_Scale(float fScale = 0.6f)
		{
			this.m_objSDRoot.transform.localScale = new Vector3(fScale, fScale, 1f);
		}

		// Token: 0x0600733B RID: 29499 RVA: 0x00264FBF File Offset: 0x002631BF
		private void Start()
		{
			this.m_NKCUISlot.Init();
			this.m_csbtnChange.PointerClick.RemoveAllListeners();
			this.m_csbtnChange.PointerClick.AddListener(new UnityAction(this.OnClickChange));
		}

		// Token: 0x0600733C RID: 29500 RVA: 0x00264FF8 File Offset: 0x002631F8
		private void OnClickChange()
		{
			if (this.m_dOnClickChange != null)
			{
				this.m_dOnClickChange(this.GetEmoticonID());
			}
		}

		// Token: 0x0600733D RID: 29501 RVA: 0x00265013 File Offset: 0x00263213
		public void SetClickEvent(NKCUISlot.OnClick _dOnClick)
		{
			this.m_dOnClick = _dOnClick;
		}

		// Token: 0x0600733E RID: 29502 RVA: 0x0026501C File Offset: 0x0026321C
		public void SetClickEventForChange(NKCPopupEmoticonSlotSD.dOnClickChange _dOnClickChange)
		{
			this.m_dOnClickChange = _dOnClickChange;
		}

		// Token: 0x0600733F RID: 29503 RVA: 0x00265028 File Offset: 0x00263228
		public void SetUI(int emoticonID)
		{
			if (this.m_NKCUISlot.GetSlotData() != null && this.m_NKCUISlot.GetSlotData().ID != emoticonID)
			{
				this.ClearReusableData();
			}
			this.m_NKCUISlot.SetData(NKCUISlot.SlotData.MakeEmoticonData(emoticonID, 1), true, this.m_dOnClick);
			this.m_NKCUISlot.SetBGVisible(false);
		}

		// Token: 0x06007340 RID: 29504 RVA: 0x00265080 File Offset: 0x00263280
		public int GetEmoticonID()
		{
			if (this.m_NKCUISlot.GetSlotData() != null)
			{
				return this.m_NKCUISlot.GetSlotData().ID;
			}
			return 0;
		}

		// Token: 0x06007341 RID: 29505 RVA: 0x002650A1 File Offset: 0x002632A1
		public void SetSelected(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelected, bSet);
		}

		// Token: 0x06007342 RID: 29506 RVA: 0x002650AF File Offset: 0x002632AF
		public bool GetSelected()
		{
			return this.m_objSelected.activeSelf;
		}

		// Token: 0x06007343 RID: 29507 RVA: 0x002650BC File Offset: 0x002632BC
		public void SetSelectedWithChangeButton(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objSelectedForChange, bSet);
			NKCUtil.SetGameobjectActive(this.m_csbtnChange, bSet);
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x002650D8 File Offset: 0x002632D8
		public void StopSDAni()
		{
			if (this.m_SkeletonGraphicEmoticon != null)
			{
				this.m_SkeletonGraphicEmoticon.AnimationState.SetAnimation(0, "BASE_END", false);
			}
			if (this.m_PrevSoundID > 0)
			{
				NKCSoundManager.StopSound(this.m_PrevSoundID);
			}
			this.m_PrevSoundID = -1;
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x00265128 File Offset: 0x00263328
		public void PlaySDAni()
		{
			int emoticonID = this.GetEmoticonID();
			if (emoticonID <= 0)
			{
				return;
			}
			NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(emoticonID);
			if (nkmemoticonTemplet == null)
			{
				return;
			}
			if (this.m_cNKCAssetInstanceDataEmoticonSD == null)
			{
				this.m_cNKCAssetInstanceDataEmoticonSD = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_" + nkmemoticonTemplet.m_EmoticonAssetName, nkmemoticonTemplet.m_EmoticonAssetName, false, null);
				this.m_SkeletonGraphicEmoticon = this.m_cNKCAssetInstanceDataEmoticonSD.m_Instant.GetComponentInChildren<SkeletonGraphic>();
				if (this.m_SkeletonGraphicEmoticon == null)
				{
					Debug.LogError("PopupEmoticonSlotSD Can't find Skeleton graphic, AssetName : " + nkmemoticonTemplet.m_EmoticonAssetName);
					return;
				}
				this.m_cNKCAssetInstanceDataEmoticonSD.m_Instant.transform.SetParent(this.m_objSDRoot.transform, false);
			}
			if (this.m_SkeletonGraphicEmoticon != null)
			{
				this.m_SkeletonGraphicEmoticon.AnimationState.SetAnimation(0, "BASE", false);
				this.m_SkeletonGraphicEmoticon.AnimationState.AddAnimation(0, "BASE_END", false, 0f);
				if (this.m_PrevSoundID > 0)
				{
					NKCSoundManager.StopSound(this.m_PrevSoundID);
				}
				this.m_PrevSoundID = -1;
				if (!string.IsNullOrWhiteSpace(nkmemoticonTemplet.m_EmoticonSound))
				{
					this.m_PrevSoundID = NKCSoundManager.PlaySound("AB_FX_UI_EMOTICON_" + nkmemoticonTemplet.m_EmoticonSound, 1f, 0f, 0f, false, 0f, false, 0f);
				}
			}
		}

		// Token: 0x04005F28 RID: 24360
		public const string PLAY_ANI_NAME = "BASE";

		// Token: 0x04005F29 RID: 24361
		public const string STOP_ANI_NAME = "BASE_END";

		// Token: 0x04005F2A RID: 24362
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04005F2B RID: 24363
		public GameObject m_objSelected;

		// Token: 0x04005F2C RID: 24364
		public GameObject m_objSelectedForChange;

		// Token: 0x04005F2D RID: 24365
		public NKCUIComStateButton m_csbtnChange;

		// Token: 0x04005F2E RID: 24366
		public GameObject m_objSDRoot;

		// Token: 0x04005F2F RID: 24367
		public Animator m_amtorChangeEffect;

		// Token: 0x04005F30 RID: 24368
		private Canvas m_Canvas;

		// Token: 0x04005F31 RID: 24369
		private GraphicRaycaster m_GraphicRaycaster;

		// Token: 0x04005F32 RID: 24370
		private NKCUISlot.OnClick m_dOnClick;

		// Token: 0x04005F33 RID: 24371
		private NKCAssetInstanceData m_cNKCAssetInstanceDataEmoticonSD;

		// Token: 0x04005F34 RID: 24372
		private SkeletonGraphic m_SkeletonGraphicEmoticon;

		// Token: 0x04005F35 RID: 24373
		private NKCAssetInstanceData m_cNKCAssetInstanceData;

		// Token: 0x04005F36 RID: 24374
		private NKCPopupEmoticonSlotSD.dOnClickChange m_dOnClickChange;

		// Token: 0x04005F37 RID: 24375
		private int m_PrevSoundID = -1;

		// Token: 0x0200178A RID: 6026
		// (Invoke) Token: 0x0600B397 RID: 45975
		public delegate void dOnClickChange(int emoticonID);
	}
}
