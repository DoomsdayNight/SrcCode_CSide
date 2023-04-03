using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using NKC.AI.PathFinder;
using NKC.Office;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000813 RID: 2067
	public abstract class BTOfficeActionBase : BehaviorDesigner.Runtime.Tasks.Action
	{
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060051D4 RID: 20948 RVA: 0x0018D4E8 File Offset: 0x0018B6E8
		protected NKCOfficeFloorBase Floor
		{
			get
			{
				NKCOfficeBuildingBase officeBuilding = this.m_OfficeBuilding;
				if (officeBuilding == null)
				{
					return null;
				}
				return officeBuilding.m_Floor;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x060051D5 RID: 20949 RVA: 0x0018D4FB File Offset: 0x0018B6FB
		protected long[,] FloorMap
		{
			get
			{
				NKCOfficeBuildingBase officeBuilding = this.m_OfficeBuilding;
				if (officeBuilding == null)
				{
					return null;
				}
				return officeBuilding.FloorMap;
			}
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x0018D50E File Offset: 0x0018B70E
		public override void OnAwake()
		{
			this.m_Character = base.GetComponent<NKCOfficeCharacter>();
			NKCOfficeCharacter character = this.m_Character;
			this.m_OfficeBuilding = ((character != null) ? character.OfficeBuilding : null);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x0018D534 File Offset: 0x0018B734
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.m_Character.PlayAnimCompleted())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0018D550 File Offset: 0x0018B750
		protected NKCAnimationInstance GetInvertDirectionInstance(bool bLeft)
		{
			List<NKCAnimationEventTemplet> list = new List<NKCAnimationEventTemplet>();
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "InvertDir",
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.INVERT_MODEL_X,
				m_BoolValue = bLeft
			});
			return new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, list, this.m_Character.transform.localPosition, this.m_Character.transform.localPosition);
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x0018D5CA File Offset: 0x0018B7CA
		protected NKCAnimationInstance GetWalkInstance(Vector3 startPos, Vector3 destination, float speed = 150f, string animName = "")
		{
			return new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, BTOfficeActionBase.DefaultWalkEvent(speed, animName), startPos, destination);
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x0018D5EC File Offset: 0x0018B7EC
		protected static List<NKCAnimationEventTemplet> DefaultWalkEvent(float speed = 150f, string animName = "")
		{
			List<NKCAnimationEventTemplet> list = new List<NKCAnimationEventTemplet>();
			if (string.IsNullOrEmpty(animName))
			{
				list.Add(new NKCAnimationEventTemplet
				{
					m_AniEventStrID = "WALK_DEFAULT",
					m_StartTime = 0f,
					m_AniEventType = AnimationEventType.ANIMATION_SPINE,
					m_StrValue = "SD_WALK",
					m_FloatValue = 1f,
					m_BoolValue = true
				});
			}
			else
			{
				list.Add(new NKCAnimationEventTemplet
				{
					m_AniEventStrID = "WALK_DEFAULT",
					m_StartTime = 0f,
					m_AniEventType = AnimationEventType.ANIMATION_NAME_SPINE,
					m_StrValue = animName,
					m_FloatValue = 1f,
					m_BoolValue = true
				});
			}
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "WALK_DEFAULT",
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_ABSOLUTE_MOVE_SPEED,
				m_FloatValue = speed
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "WALK_DEFAULT",
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.INVERT_MODEL_X_BY_DIRECTION,
				m_BoolValue = true
			});
			return list;
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0018D6F0 File Offset: 0x0018B8F0
		protected NKCAnimationInstance GetIdleInstance(float time, Vector3 position, string idleAnimName = "")
		{
			return new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, BTOfficeActionBase.DefaultIdleEvent(time, idleAnimName), position, position);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0018D714 File Offset: 0x0018B914
		protected static List<NKCAnimationEventTemplet> DefaultIdleEvent(float time, string idleAnimName = "")
		{
			List<NKCAnimationEventTemplet> list = new List<NKCAnimationEventTemplet>();
			if (string.IsNullOrEmpty(idleAnimName))
			{
				list.Add(new NKCAnimationEventTemplet
				{
					m_AniEventStrID = "IDLE_DEFAULT",
					m_StartTime = 0f,
					m_AniEventType = AnimationEventType.ANIMATION_SPINE,
					m_StrValue = "SD_IDLE",
					m_FloatValue = 1f,
					m_BoolValue = true
				});
			}
			else
			{
				list.Add(new NKCAnimationEventTemplet
				{
					m_AniEventStrID = "IDLE_DEFAULT",
					m_StartTime = 0f,
					m_AniEventType = AnimationEventType.ANIMATION_NAME_SPINE,
					m_StrValue = idleAnimName,
					m_FloatValue = 1f,
					m_BoolValue = true
				});
			}
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "IDLE_DEFAULT",
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_MOVE_SPEED,
				m_FloatValue = 0f
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "IDLE_DEFAULT",
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_POSITION,
				m_FloatValue = 0f
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "IDLE_DEFAULT",
				m_StartTime = time,
				m_AniEventType = AnimationEventType.SET_POSITION,
				m_FloatValue = 1f
			});
			return list;
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0018D84D File Offset: 0x0018BA4D
		protected NKCAnimationInstance GetRunInstance(Vector3 startPos, Vector3 destination, float speed = 600f, string animName = "")
		{
			return new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, BTOfficeActionBase.DefaultRunEvent(speed, animName), startPos, destination);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0018D870 File Offset: 0x0018BA70
		public static List<NKCAnimationEventTemplet> DefaultRunEvent(float speed = 600f, string animName = "")
		{
			List<NKCAnimationEventTemplet> list = new List<NKCAnimationEventTemplet>();
			if (string.IsNullOrEmpty(animName))
			{
				list.Add(new NKCAnimationEventTemplet
				{
					m_AniEventStrID = "RUN_DEFAULT",
					m_StartTime = 0f,
					m_AniEventType = AnimationEventType.ANIMATION_SPINE,
					m_StrValue = "SD_RUN",
					m_FloatValue = 1.2f,
					m_BoolValue = true
				});
			}
			else
			{
				list.Add(new NKCAnimationEventTemplet
				{
					m_AniEventStrID = "RUN_DEFAULT",
					m_StartTime = 0f,
					m_AniEventType = AnimationEventType.ANIMATION_NAME_SPINE,
					m_StrValue = animName,
					m_FloatValue = 1.2f,
					m_BoolValue = true
				});
			}
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "RUN_DEFAULT",
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.SET_ABSOLUTE_MOVE_SPEED,
				m_FloatValue = speed
			});
			list.Add(new NKCAnimationEventTemplet
			{
				m_AniEventStrID = "RUN_DEFAULT",
				m_StartTime = 0f,
				m_AniEventType = AnimationEventType.INVERT_MODEL_X_BY_DIRECTION,
				m_BoolValue = true
			});
			return list;
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x0018D974 File Offset: 0x0018BB74
		protected bool Move(List<NKCAnimationEventTemplet> lstMoveEventTemplet, Vector3 endLocalPos, bool ignoreObstacles)
		{
			if (ignoreObstacles)
			{
				NKCAnimationInstance instance = new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, lstMoveEventTemplet, this.transform.localPosition, endLocalPos);
				this.m_Character.EnqueueAnimation(instance);
				return true;
			}
			OfficeFloorPosition officeFloorPosition = this.m_OfficeBuilding.CalculateFloorPosition(endLocalPos, true);
			OfficeFloorPosition officeFloorPosition2 = this.m_OfficeBuilding.CalculateFloorPosition(this.transform.localPosition, true);
			List<ValueTuple<int, int>> path = new NKCAStar(this.FloorMap, officeFloorPosition2.ToPair, officeFloorPosition.ToPair).GetPath(true);
			if (path == null)
			{
				return false;
			}
			Vector3 startPos = this.transform.localPosition;
			for (int i = 0; i < path.Count - 1; i++)
			{
				ValueTuple<int, int> pos = path[i];
				Vector3 localPos = this.m_Character.GetLocalPos(pos, true);
				NKCAnimationInstance instance2 = new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, lstMoveEventTemplet, startPos, localPos);
				this.m_Character.EnqueueAnimation(instance2);
				startPos = localPos;
			}
			NKCAnimationInstance instance3 = new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, lstMoveEventTemplet, startPos, endLocalPos);
			this.m_Character.EnqueueAnimation(instance3);
			return true;
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x0018DA98 File Offset: 0x0018BC98
		protected bool Move(Vector3 endLocalPos, bool ignoreObstacles, float speed = 150f, string animName = "")
		{
			if (ignoreObstacles)
			{
				NKCAnimationInstance walkInstance = this.GetWalkInstance(this.transform.localPosition, endLocalPos, speed, animName);
				this.m_Character.EnqueueAnimation(walkInstance);
				return true;
			}
			OfficeFloorPosition officeFloorPosition = this.m_OfficeBuilding.CalculateFloorPosition(endLocalPos, true);
			OfficeFloorPosition officeFloorPosition2 = this.m_OfficeBuilding.CalculateFloorPosition(this.transform.localPosition, true);
			List<ValueTuple<int, int>> path = new NKCAStar(this.FloorMap, officeFloorPosition2.ToPair, officeFloorPosition.ToPair).GetPath(true);
			if (path == null)
			{
				Debug.LogWarning(string.Format("From {0} to {1} : path not found!", officeFloorPosition2, officeFloorPosition));
				return false;
			}
			Vector3 startPos = this.transform.localPosition;
			for (int i = 0; i < path.Count - 1; i++)
			{
				ValueTuple<int, int> pos = path[i];
				Vector3 localPos = this.m_Character.GetLocalPos(pos, true);
				NKCAnimationInstance walkInstance2 = this.GetWalkInstance(startPos, localPos, speed, animName);
				this.m_Character.EnqueueAnimation(walkInstance2);
				startPos = localPos;
			}
			NKCAnimationInstance walkInstance3 = this.GetWalkInstance(startPos, endLocalPos, speed, animName);
			this.m_Character.EnqueueAnimation(walkInstance3);
			return true;
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x0018DBAC File Offset: 0x0018BDAC
		protected bool Move(OfficeFloorPosition endPos, bool ignoreObstacles)
		{
			if (ignoreObstacles)
			{
				Vector3 localPos = this.Floor.GetLocalPos(endPos);
				NKCAnimationInstance walkInstance = this.GetWalkInstance(this.transform.localPosition, localPos, 150f, "");
				this.m_Character.EnqueueAnimation(walkInstance);
				return true;
			}
			OfficeFloorPosition officeFloorPosition = this.m_OfficeBuilding.CalculateFloorPosition(this.transform.localPosition, 1, 1, false);
			List<ValueTuple<int, int>> path = new NKCAStar(this.FloorMap, officeFloorPosition.ToPair, endPos.ToPair).GetPath(true);
			if (path == null)
			{
				Debug.LogWarning(string.Format("From {0} to {1} : path not found!", officeFloorPosition, endPos));
				return false;
			}
			Vector3 startPos = this.transform.localPosition;
			foreach (ValueTuple<int, int> pos in path)
			{
				Vector3 localPos2 = this.m_Character.GetLocalPos(pos, true);
				NKCAnimationInstance walkInstance2 = this.GetWalkInstance(startPos, localPos2, 150f, "");
				this.m_Character.EnqueueAnimation(walkInstance2);
				startPos = localPos2;
			}
			return true;
		}

		// Token: 0x04004220 RID: 16928
		protected NKCOfficeCharacter m_Character;

		// Token: 0x04004221 RID: 16929
		protected NKCOfficeBuildingBase m_OfficeBuilding;

		// Token: 0x04004222 RID: 16930
		protected bool bActionSuccessFlag;
	}
}
