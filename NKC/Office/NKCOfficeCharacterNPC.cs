using System;
using BehaviorDesigner.Runtime;
using NKC.UI.NPC;
using NKM;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x0200082B RID: 2091
	public class NKCOfficeCharacterNPC : NKCOfficeCharacter
	{
		// Token: 0x0600533B RID: 21307 RVA: 0x00195E1E File Offset: 0x0019401E
		public static NKCOfficeCharacterNPC GetNPCInstance()
		{
			return NKCOfficeCharacterNPC.GetNPCInstance(new NKMAssetName("ab_unit_office_sd", "UNIT_OFFICE_SD_NPC"));
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00195E34 File Offset: 0x00194034
		public static NKCOfficeCharacterNPC GetNPCInstance(NKMAssetName assetName)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(NKCResourceUtility.GetOrLoadAssetResource<GameObject>(assetName));
			if (gameObject == null)
			{
				return null;
			}
			NKCOfficeCharacterNPC component = gameObject.GetComponent<NKCOfficeCharacterNPC>();
			if (component == null)
			{
				Debug.LogError("NKCUIOfficeCharacter loadprefab failed!");
				UnityEngine.Object.DestroyImmediate(gameObject);
				return null;
			}
			return component;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x00195E7C File Offset: 0x0019407C
		public virtual void Init(NKCOfficeBuildingBase officeBuilding)
		{
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(this.SpineAssetName, this.SpineAssetName);
			base.SetSpineIllust(NKCResourceUtility.OpenSpineSD(nkmassetName.m_BundleName, nkmassetName.m_BundleName, false), true);
			if (this.m_SDIllust != null)
			{
				this.m_SDIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SD_IDLE, true, false, 0f);
				this.m_SDIllust.GetRectTransform().localPosition = Vector3.zero;
				this.m_SDIllust.GetRectTransform().pivot = new Vector2(0.5f, 0.5f);
				this.m_SDIllust.GetRectTransform().anchorMin = new Vector2(0.5f, 0.5f);
				this.m_SDIllust.GetRectTransform().anchorMax = new Vector2(0.5f, 0.5f);
			}
			base.transform.rotation = Quaternion.identity;
			this.m_OfficeBuilding = officeBuilding;
			this.BT = base.GetComponent<BehaviorTree>();
			if (this.BT == null)
			{
				Debug.LogWarning("Office SD : BT Not found. Using Default BT");
				this.BT = base.gameObject.AddComponent<BehaviorTree>();
				this.BT.StartWhenEnabled = false;
				if (string.IsNullOrEmpty(this.BTAssetName))
				{
					this.BT.ExternalBehavior = NKCResourceUtility.GetOrLoadAssetResource<ExternalBehavior>("ab_ui_office_bt", "OFFICE_BT_DEFAULT", false);
				}
				else
				{
					this.BT.ExternalBehavior = NKCResourceUtility.GetOrLoadAssetResource<ExternalBehavior>("ab_ui_office_bt", this.BTAssetName, false);
				}
			}
			else
			{
				this.BT.StartWhenEnabled = false;
				if (!string.IsNullOrEmpty(this.BTAssetName))
				{
					this.BT.ExternalBehavior = NKCResourceUtility.GetOrLoadAssetResource<ExternalBehavior>("ab_ui_office_bt", this.BTAssetName, false);
				}
			}
			this.BT.DisableBehavior();
			this.m_UnitData = null;
			NKCUtil.SetGameobjectActive(this.m_comLoyalty, false);
			this.BT.RestartWhenComplete = true;
			this.BT.OnBehaviorRestart += base.OnBTRestart;
			base.transform.SetParent(officeBuilding.trActorRoot);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x00196070 File Offset: 0x00194270
		protected override void PlayTouchVoice()
		{
			NKCNPCTemplet npctemplet = NKCUINPCBase.GetNPCTemplet(this.m_eNPCType, NPC_ACTION_TYPE.TOUCH);
			if (npctemplet != null)
			{
				NKCUINPCBase.PlayVoice(this.m_eNPCType, npctemplet, true, false, true);
			}
		}

		// Token: 0x040042BE RID: 17086
		public string SpineAssetName;

		// Token: 0x040042BF RID: 17087
		public string BTAssetName;

		// Token: 0x040042C0 RID: 17088
		public NPC_TYPE m_eNPCType;

		// Token: 0x040042C1 RID: 17089
		private const string NPC_BUNDLE_NAME = "ab_unit_office_sd";

		// Token: 0x040042C2 RID: 17090
		private const string NPC_ASSET_NAME = "UNIT_OFFICE_SD_NPC";
	}
}
