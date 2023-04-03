using System;
using NKM;
using NKM.Templet.Office;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.Office
{
	// Token: 0x02000833 RID: 2099
	public class NKCOfficeFlatFuniture : NKCOfficeFuniture
	{
		// Token: 0x0600536B RID: 21355 RVA: 0x00196D70 File Offset: 0x00194F70
		public override void SetData(NKMOfficeInteriorTemplet templet, float tileSize)
		{
			base.SetData(templet, tileSize);
			this.m_eFunitureType = templet.Target;
			if (this.m_imgFuniture != null)
			{
				NKMAssetName cNKMAssetName = NKMAssetName.ParseBundleName(templet.PrefabName, templet.PrefabName);
				this.m_imgFuniture.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(cNKMAssetName);
				this.m_imgFuniture.type = Image.Type.Tiled;
			}
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x00196DD0 File Offset: 0x00194FD0
		public override void SetInvert(bool bInvert, bool bEditMode = false)
		{
			InteriorTarget eFunitureType = this.m_eFunitureType;
			if (eFunitureType > InteriorTarget.Tile)
			{
				if (eFunitureType != InteriorTarget.Wall)
				{
					return;
				}
				if (bInvert)
				{
					if (bEditMode)
					{
						this.m_rtFloor.rotation = Quaternion.Euler(-16.377f, 47.477f, -17.091f);
						return;
					}
					this.m_rtFloor.localRotation = Quaternion.identity;
					return;
				}
				else
				{
					if (bEditMode)
					{
						this.m_rtFloor.rotation = Quaternion.Euler(-16.377f, -47.477f, 17.091f);
						return;
					}
					this.m_rtFloor.localRotation = Quaternion.identity;
					return;
				}
			}
			else if (bInvert)
			{
				if (bEditMode)
				{
					this.m_rtFloor.rotation = Quaternion.Euler(66.5f, 0f, -45f);
					return;
				}
				this.m_rtFloor.localRotation = Quaternion.Euler(0f, 0f, -90f);
				return;
			}
			else
			{
				if (bEditMode)
				{
					this.m_rtFloor.rotation = Quaternion.Euler(66.5f, 0f, 45f);
					return;
				}
				this.m_rtFloor.localRotation = Quaternion.identity;
				return;
			}
		}
	}
}
