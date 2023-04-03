using System;
using Cs.Logging;
using NKM;

namespace NKC
{
	// Token: 0x020006B0 RID: 1712
	public class NKCObjectPool : NKMObjectPool
	{
		// Token: 0x060038A2 RID: 14498 RVA: 0x00124EDE File Offset: 0x001230DE
		public override void Unload()
		{
			base.Unload();
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x00124EE8 File Offset: 0x001230E8
		protected override NKMObjectPoolData CreateNewObj(NKM_OBJECT_POOL_TYPE typeT, string bundleName = "", string name = "", bool bAsync = false)
		{
			NKMObjectPoolData nkmobjectPoolData = base.CreateNewObj(typeT, bundleName, name, bAsync);
			if (nkmobjectPoolData != null)
			{
				return nkmobjectPoolData;
			}
			switch (typeT)
			{
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCDamageEffect:
				nkmobjectPoolData = new NKCDamageEffect();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASMaterial:
				nkmobjectPoolData = new NKCASMaterial(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASLensFlare:
				nkmobjectPoolData = new NKCASLensFlare(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSprite:
				nkmobjectPoolData = new NKCASUnitSprite(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitSpineSprite:
				nkmobjectPoolData = new NKCASUnitSpineSprite(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitViewerSpineSprite:
				nkmobjectPoolData = new NKCASUnitViewerSpineSprite(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitShadow:
				nkmobjectPoolData = new NKCASUnitShadow(bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASUnitMiniMapFace:
				nkmobjectPoolData = new NKCASUnitMiniMapFace(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASUISpineIllust:
				nkmobjectPoolData = new NKCASUISpineIllust(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASEffect:
				nkmobjectPoolData = new NKCASEffect(bundleName, name, bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCASDangerChargeUI:
				nkmobjectPoolData = new NKCASDangerChargeUI(bAsync);
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCUnitClient:
				nkmobjectPoolData = new NKCUnitClient();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCUnitViewer:
				nkmobjectPoolData = new NKCUnitViewer();
				break;
			case NKM_OBJECT_POOL_TYPE.NOPT_NKCEffectReserveData:
				nkmobjectPoolData = new NKCEffectReserveData();
				break;
			default:
				Log.Error(string.Format("CreateNewObj null typeT:{0}, bundleName:{1}, name:{2}, bAsync:{3}", new object[]
				{
					typeT,
					bundleName,
					name,
					bAsync
				}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCObjectPool.cs", 90);
				break;
			}
			return nkmobjectPoolData;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x0012502C File Offset: 0x0012322C
		public override void Update()
		{
			if (NKCAssetResourceManager.IsLoadEnd())
			{
				int num = 0;
				while (num < 100 && this.m_qAsyncLoadObject.Count != 0 && NKCAssetResourceManager.IsLoadEnd())
				{
					NKMObjectPoolData nkmobjectPoolData = this.m_qAsyncLoadObject.Dequeue();
					if (nkmobjectPoolData.LoadComplete())
					{
						goto IL_60;
					}
					nkmobjectPoolData.Load(false);
					if (nkmobjectPoolData.LoadComplete())
					{
						goto IL_60;
					}
					Log.Info("LoadComplete Fail " + nkmobjectPoolData.m_ObjectPoolName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCObjectPool.cs", 118);
					IL_6D:
					num++;
					continue;
					IL_60:
					nkmobjectPoolData.m_bIsLoaded = true;
					nkmobjectPoolData.Open();
					goto IL_6D;
				}
			}
		}
	}
}
