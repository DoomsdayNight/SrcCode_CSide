using System;
using System.Collections;
using System.Collections.Generic;
using ClientPacket.Common;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Result;
using NKM;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A29 RID: 2601
	public class NKCPopupMessageToastSimple : NKCUIBase
	{
		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x060071E0 RID: 29152 RVA: 0x0025D8FC File Offset: 0x0025BAFC
		public static NKCPopupMessageToastSimple Instance
		{
			get
			{
				if (NKCPopupMessageToastSimple.m_Instance == null)
				{
					NKCPopupMessageToastSimple.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupMessageToastSimple>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_MESSAGE_TOAST", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupMessageToastSimple.CleanupInstance)).GetInstance<NKCPopupMessageToastSimple>();
					NKCPopupMessageToastSimple instance = NKCPopupMessageToastSimple.m_Instance;
					if (instance != null)
					{
						instance.Init();
					}
				}
				return NKCPopupMessageToastSimple.m_Instance;
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x060071E1 RID: 29153 RVA: 0x0025D951 File Offset: 0x0025BB51
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupMessageToastSimple.m_Instance != null && NKCPopupMessageToastSimple.m_Instance.IsOpen;
			}
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x0025D96C File Offset: 0x0025BB6C
		private static void CleanupInstance()
		{
			NKCPopupMessageToastSimple.m_Instance.Release();
			NKCPopupMessageToastSimple.m_Instance = null;
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x0025D980 File Offset: 0x0025BB80
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupMessageToastSimple.m_Instance != null && NKCPopupMessageToastSimple.m_Instance.RewardFromBattle)
			{
				NKCPopupMessageToastSimple.m_Instance.RewardFromBattle = false;
				return;
			}
			if (NKCPopupMessageToastSimple.m_Instance != null && NKCPopupMessageToastSimple.m_Instance.IsOpen)
			{
				NKCPopupMessageToastSimple.m_Instance.Close();
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x060071E4 RID: 29156 RVA: 0x0025D9D5 File Offset: 0x0025BBD5
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x060071E5 RID: 29157 RVA: 0x0025D9D8 File Offset: 0x0025BBD8
		public override string MenuName
		{
			get
			{
				return "Toast Message Simple";
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x060071E6 RID: 29158 RVA: 0x0025D9DF File Offset: 0x0025BBDF
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ONLY_MEMORY_SHORTAGE;
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x060071E7 RID: 29159 RVA: 0x0025D9E2 File Offset: 0x0025BBE2
		// (set) Token: 0x060071E8 RID: 29160 RVA: 0x0025D9EA File Offset: 0x0025BBEA
		public bool RewardFromBattle
		{
			get
			{
				return this.m_rewardFromBattle;
			}
			set
			{
				this.m_rewardFromBattle = value;
			}
		}

		// Token: 0x060071E9 RID: 29161 RVA: 0x0025D9F4 File Offset: 0x0025BBF4
		private void Init()
		{
			if (this.group.childCount <= 0)
			{
				GameObject orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<GameObject>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_MESSAGE_TOAST_SLOT", false);
				if (!(orLoadAssetResource != null))
				{
					Debug.LogWarning("NKM_UI_POPUP_MESSAGE_TOAST_SLOT is not created");
					return;
				}
				RectTransform component = UnityEngine.Object.Instantiate<GameObject>(orLoadAssetResource, this.group).GetComponent<RectTransform>();
				this.AddSlotObjectToGroup(component);
			}
			int num = this.group.childCount;
			for (int i = 0; i < num; i++)
			{
				this.group.GetChild(i).gameObject.SetActive(false);
				this.m_lstMsgSlot.Add(this.group.GetChild(i).GetComponent<NKCUIMessageToastSimpleSlot>());
			}
			int num2 = this.maxCount - this.group.childCount + 1;
			for (int j = 0; j < num2; j++)
			{
				RectTransform component2 = UnityEngine.Object.Instantiate<Transform>(this.group.GetChild(0), this.group).GetComponent<RectTransform>();
				if (!(component2 == null))
				{
					this.AddSlotObjectToGroup(component2);
					component2.gameObject.SetActive(false);
					this.m_lstMsgSlot.Add(component2.GetComponent<NKCUIMessageToastSimpleSlot>());
				}
			}
			if (this.group.childCount > 0)
			{
				this.slotHeight = this.group.GetChild(0).GetComponent<RectTransform>().GetHeight();
			}
			if (this.contentRect != null)
			{
				this.contentRect.SetHeight(this.slotHeight + (this.slotHeight + this.spacing) * (float)(this.maxCount - 1));
			}
			num = this.m_lstMsgSlot.Count;
			this.slotYPosition = new float[num];
			for (int k = 0; k < num; k++)
			{
				this.m_lstMsgSlot[k].ResetSlot();
				this.slotYPosition[k] = this.m_lstMsgSlot[k].transform.position.y + this.slotHeight;
			}
		}

		// Token: 0x060071EA RID: 29162 RVA: 0x0025DBDC File Offset: 0x0025BDDC
		public override void CloseInternal()
		{
			if (this.m_coroutine != null)
			{
				base.StopCoroutine(this.m_coroutine);
				this.m_coroutine = null;
			}
			this.m_queueList.Clear();
			int count = this.m_lstMsgSlot.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_lstMsgSlot[i].transform.DOKill(false);
				this.m_lstMsgSlot[i].gameObject.SetActive(false);
				this.m_lstMsgSlot[i].ResetSlot();
				Vector3 position = this.m_lstMsgSlot[i].transform.position;
				position.y = this.slotYPosition[i] - this.slotHeight;
				this.m_lstMsgSlot[i].transform.position = position;
			}
			this.m_rewardFromBattle = false;
			base.gameObject.SetActive(false);
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x0025DCC4 File Offset: 0x0025BEC4
		public void Open(NKMRewardData rewardData, NKMAdditionalReward additionalReward, NKCUIResult.OnClose onOpen = null)
		{
			List<NKCUISlot.SlotData> slotDataList = NKCUISlot.MakeSlotDataListFromReward(rewardData, false, false);
			if (additionalReward != null)
			{
				NKCUISlot.MakeSlotDataListFromReward(additionalReward).ForEach(delegate(NKCUISlot.SlotData e)
				{
					slotDataList.Add(e);
				});
			}
			int count = slotDataList.Count;
			int i = 0;
			while (i < count)
			{
				switch (slotDataList[i].eType)
				{
				case NKCUISlot.eSlotMode.Unit:
				case NKCUISlot.eSlotMode.UnitCount:
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData item = default(NKCPopupMessageToastSimple.MessageData);
					if (unitTempletBase != null)
					{
						item.sprite = NKCResourceUtility.GetorLoadUnitSprite(NKCResourceUtility.eUnitResourceType.INVEN_ICON, unitTempletBase);
					}
					item.name = NKCUISlot.GetName(slotDataList[i]);
					item.count = 1L;
					this.m_queueList.Add(item);
					break;
				}
				case NKCUISlot.eSlotMode.ItemMisc:
				{
					NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData item2 = default(NKCPopupMessageToastSimple.MessageData);
					if (itemMiscTempletByID != null)
					{
						item2.sprite = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
					}
					item2.name = NKCUISlot.GetName(slotDataList[i]);
					item2.count = slotDataList[i].Count;
					this.m_queueList.Add(item2);
					break;
				}
				case NKCUISlot.eSlotMode.Equip:
				case NKCUISlot.eSlotMode.EquipCount:
				{
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData messageData = default(NKCPopupMessageToastSimple.MessageData);
					if (equipTemplet != null)
					{
						messageData.sprite = NKCResourceUtility.GetOrLoadEquipIcon(equipTemplet);
						if (equipTemplet.m_EquipUnitStyleType == NKM_UNIT_STYLE_TYPE.NUST_ENCHANT)
						{
							int num = this.m_queueList.FindIndex((NKCPopupMessageToastSimple.MessageData e) => e.enchantModuleId == equipTemplet.m_ItemEquipID);
							if (num >= 0 && num < this.m_queueList.Count)
							{
								messageData = this.m_queueList[num];
								messageData.count += 1L;
								this.m_queueList[num] = messageData;
								break;
							}
							messageData.enchantModuleId = equipTemplet.m_ItemEquipID;
						}
					}
					messageData.name = NKCUISlot.GetName(slotDataList[i]);
					messageData.count = 1L;
					this.m_queueList.Add(messageData);
					break;
				}
				case NKCUISlot.eSlotMode.Skin:
					goto IL_495;
				case NKCUISlot.eSlotMode.Mold:
				{
					NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData item3 = default(NKCPopupMessageToastSimple.MessageData);
					if (itemMoldTempletByID != null)
					{
						item3.sprite = NKCResourceUtility.GetOrLoadMoldIcon(itemMoldTempletByID);
					}
					item3.name = NKCUISlot.GetName(slotDataList[i]);
					item3.count = slotDataList[i].Count;
					this.m_queueList.Add(item3);
					break;
				}
				case NKCUISlot.eSlotMode.DiveArtifact:
				{
					NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData item4 = default(NKCPopupMessageToastSimple.MessageData);
					if (nkmdiveArtifactTemplet != null)
					{
						item4.sprite = NKCResourceUtility.GetOrLoadDiveArtifactIcon(nkmdiveArtifactTemplet);
					}
					item4.name = NKCUISlot.GetName(slotDataList[i]);
					item4.count = slotDataList[i].Count;
					this.m_queueList.Add(item4);
					break;
				}
				case NKCUISlot.eSlotMode.Buff:
				{
					NKMCompanyBuffTemplet companyBuffTemplet = NKMCompanyBuffManager.GetCompanyBuffTemplet(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData item5 = default(NKCPopupMessageToastSimple.MessageData);
					if (companyBuffTemplet != null)
					{
						item5.sprite = NKCResourceUtility.GetOrLoadBuffIconForItemPopup(companyBuffTemplet);
					}
					item5.name = NKCUISlot.GetName(slotDataList[i]);
					item5.count = 1L;
					this.m_queueList.Add(item5);
					break;
				}
				case NKCUISlot.eSlotMode.Emoticon:
				{
					NKMEmoticonTemplet nkmemoticonTemplet = NKMEmoticonTemplet.Find(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData item6 = default(NKCPopupMessageToastSimple.MessageData);
					if (nkmemoticonTemplet != null)
					{
						item6.sprite = NKCResourceUtility.GetOrLoadEmoticonIcon(nkmemoticonTemplet);
					}
					item6.name = NKCUISlot.GetName(slotDataList[i]);
					item6.count = slotDataList[i].Count;
					this.m_queueList.Add(item6);
					break;
				}
				case NKCUISlot.eSlotMode.GuildArtifact:
				{
					GuildDungeonArtifactTemplet artifactTemplet = GuildDungeonTempletManager.GetArtifactTemplet(slotDataList[i].ID);
					NKCPopupMessageToastSimple.MessageData item7 = default(NKCPopupMessageToastSimple.MessageData);
					if (artifactTemplet != null)
					{
						item7.sprite = NKCResourceUtility.GetOrLoadGuildArtifactIcon(artifactTemplet);
					}
					item7.name = NKCUISlot.GetName(slotDataList[i]);
					item7.count = slotDataList[i].Count;
					this.m_queueList.Add(item7);
					break;
				}
				default:
					goto IL_495;
				}
				IL_4B6:
				i++;
				continue;
				IL_495:
				NKCPopupMessageToastSimple.MessageData item8 = default(NKCPopupMessageToastSimple.MessageData);
				item8.name = "Unidentified Data";
				this.m_queueList.Add(item8);
				goto IL_4B6;
			}
			if (this.m_queueList.Count > 0)
			{
				if (this.m_coroutine != null)
				{
					this.SetMessageToSlot(true);
				}
				base.gameObject.SetActive(true);
				if (this.m_coroutine == null)
				{
					this.m_coroutine = base.StartCoroutine(this.IUpdateMessageSlot());
				}
			}
			if (onOpen != null)
			{
				onOpen();
			}
			if (base.IsOpen)
			{
				return;
			}
			base.UIOpened(true);
		}

		// Token: 0x060071EC RID: 29164 RVA: 0x0025E1F0 File Offset: 0x0025C3F0
		private void AddSlotObjectToGroup(RectTransform slotRect)
		{
			if (slotRect == null)
			{
				return;
			}
			slotRect.SetParent(this.group);
			Vector3 localPosition = slotRect.localPosition;
			float height = slotRect.GetHeight();
			localPosition.y = -height * (1f - slotRect.pivot.y) - (height + this.spacing) * (float)(this.group.childCount - 1);
			slotRect.localPosition = localPosition;
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x0025E25C File Offset: 0x0025C45C
		private void SetFirstSlotToLastSibling()
		{
			Vector3 position = this.group.GetChild(0).position;
			position.y = this.slotYPosition[this.slotYPosition.Length - 1] - this.slotHeight;
			this.group.GetChild(0).position = position;
			this.group.GetChild(0).SetAsLastSibling();
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x0025E2C0 File Offset: 0x0025C4C0
		private void SetMessageToSlot(bool addedSlot)
		{
			int num = Mathf.Min(this.maxCount + 1, this.group.childCount);
			num = Mathf.Min(num, this.m_lstMsgSlot.Count);
			for (int i = 0; i < num; i++)
			{
				if (!this.m_lstMsgSlot[i].gameObject.activeSelf)
				{
					if (this.m_queueList.Count <= 0)
					{
						break;
					}
					NKCPopupMessageToastSimple.MessageData data = this.m_queueList[0];
					this.m_queueList.RemoveAt(0);
					this.m_lstMsgSlot[i].SetData(data);
					this.m_lstMsgSlot[i].gameObject.SetActive(true);
					if (addedSlot && i >= this.maxCount)
					{
						this.m_lstMsgSlot[i].PlayIdleAni();
					}
				}
			}
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x0025E38F File Offset: 0x0025C58F
		private IEnumerator IUpdateMessageSlot()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				this.m_rewardFromBattle = true;
			}
			while (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME)
			{
				yield return null;
			}
			this.SetMessageToSlot(false);
			if (this.m_lstMsgSlot.Count > 0)
			{
				while (this.m_lstMsgSlot[0].gameObject.activeSelf)
				{
					if (this.m_lstMsgSlot[0].m_animator.GetCurrentAnimatorStateInfo(0).IsName("NKM_UI_POPUP_MESSAGE_TOAST_OUTRO") && this.m_lstMsgSlot[0].m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
					{
						NKCPopupMessageToastSimple.<>c__DisplayClass36_0 CS$<>8__locals1 = new NKCPopupMessageToastSimple.<>c__DisplayClass36_0();
						CS$<>8__locals1.moveComplete = 0;
						int slotCount = this.m_lstMsgSlot.Count;
						for (int i = 0; i < slotCount; i++)
						{
							TweenerCore<Vector3, Vector3, VectorOptions> t = this.m_lstMsgSlot[i].transform.DOMoveY(this.slotYPosition[i], this.slotMovingUpDuration, false);
							TweenCallback action;
							if ((action = CS$<>8__locals1.<>9__0) == null)
							{
								action = (CS$<>8__locals1.<>9__0 = delegate()
								{
									int moveComplete = CS$<>8__locals1.moveComplete + 1;
									CS$<>8__locals1.moveComplete = moveComplete;
								});
							}
							t.OnComplete(action);
						}
						while (CS$<>8__locals1.moveComplete < slotCount)
						{
							yield return null;
						}
						this.m_lstMsgSlot[0].gameObject.SetActive(false);
						this.m_lstMsgSlot[0].ResetSlot();
						this.SetFirstSlotToLastSibling();
						NKCUIMessageToastSimpleSlot value = this.m_lstMsgSlot[0];
						for (int j = 0; j < slotCount - 1; j++)
						{
							this.m_lstMsgSlot[j] = this.m_lstMsgSlot[j + 1];
						}
						this.m_lstMsgSlot[slotCount - 1] = value;
						if (this.m_queueList.Count > 0)
						{
							this.SetMessageToSlot(true);
						}
						CS$<>8__locals1 = null;
					}
					yield return null;
				}
			}
			base.Close();
			this.m_coroutine = null;
			yield break;
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x0025E3A0 File Offset: 0x0025C5A0
		private void Release()
		{
			if (this.m_coroutine != null)
			{
				base.StopCoroutine(this.m_coroutine);
				this.m_coroutine = null;
			}
			List<NKCPopupMessageToastSimple.MessageData> queueList = this.m_queueList;
			if (queueList != null)
			{
				queueList.Clear();
			}
			this.m_queueList = null;
			List<NKCUIMessageToastSimpleSlot> lstMsgSlot = this.m_lstMsgSlot;
			if (lstMsgSlot != null)
			{
				lstMsgSlot.Clear();
			}
			this.m_lstMsgSlot = null;
			this.slotYPosition = null;
		}

		// Token: 0x04005DD7 RID: 24023
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x04005DD8 RID: 24024
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_MESSAGE_TOAST";

		// Token: 0x04005DD9 RID: 24025
		private static NKCPopupMessageToastSimple m_Instance;

		// Token: 0x04005DDA RID: 24026
		public int maxCount;

		// Token: 0x04005DDB RID: 24027
		public float spacing;

		// Token: 0x04005DDC RID: 24028
		public float slotMovingUpDuration;

		// Token: 0x04005DDD RID: 24029
		public RectTransform contentRect;

		// Token: 0x04005DDE RID: 24030
		public Transform group;

		// Token: 0x04005DDF RID: 24031
		private List<NKCUIMessageToastSimpleSlot> m_lstMsgSlot = new List<NKCUIMessageToastSimpleSlot>();

		// Token: 0x04005DE0 RID: 24032
		private List<NKCPopupMessageToastSimple.MessageData> m_queueList = new List<NKCPopupMessageToastSimple.MessageData>();

		// Token: 0x04005DE1 RID: 24033
		private Coroutine m_coroutine;

		// Token: 0x04005DE2 RID: 24034
		private float slotHeight;

		// Token: 0x04005DE3 RID: 24035
		private float[] slotYPosition;

		// Token: 0x04005DE4 RID: 24036
		private bool m_rewardFromBattle;

		// Token: 0x0200176C RID: 5996
		public struct MessageData
		{
			// Token: 0x0400A6D4 RID: 42708
			public int enchantModuleId;

			// Token: 0x0400A6D5 RID: 42709
			public Sprite sprite;

			// Token: 0x0400A6D6 RID: 42710
			public string name;

			// Token: 0x0400A6D7 RID: 42711
			public long count;
		}
	}
}
