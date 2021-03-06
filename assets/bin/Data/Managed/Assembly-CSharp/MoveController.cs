using System;
using UnityEngine;

public class MoveController
{
	[Flags]
	public enum MOVE_TYPE
	{
		NONE = 0x0,
		STOP = 0x1,
		AVOID = 0x2,
		SEEK = 0x4,
		ROTATE = 0x8
	}

	private const float AVOID_RANGE = 2f;

	private Brain brain;

	public MOVE_TYPE moveType;

	private float saveStopRange;

	private float stopTime;

	private RaycastHit _seekHit = default(RaycastHit);

	public bool isStopTimeOver => stopTime < Time.get_time();

	public RaycastHit seekHit => _seekHit;

	public Vector2 stickVec
	{
		get;
		private set;
	}

	public Vector3 targetPos
	{
		get;
		private set;
	}

	public PLACE avoidPlace
	{
		get;
		private set;
	}

	public Vector3 rootPosition
	{
		get;
		private set;
	}

	public MoveController(Brain brain)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		this.brain = brain;
	}

	private void TypeOn(MOVE_TYPE type)
	{
		moveType |= type;
	}

	private void TypeOff(MOVE_TYPE type)
	{
		moveType &= ~type;
	}

	private bool TypeIsOn(MOVE_TYPE type)
	{
		return (moveType & type) == type;
	}

	public void StopOn()
	{
		TypeOn(MOVE_TYPE.STOP);
	}

	public void AvoidOn()
	{
		TypeOn(MOVE_TYPE.AVOID);
	}

	public void SeekOn()
	{
		TypeOn(MOVE_TYPE.SEEK);
	}

	public void RotateOn()
	{
		TypeOn(MOVE_TYPE.ROTATE);
	}

	public void StopOff()
	{
		TypeOff(MOVE_TYPE.STOP);
	}

	public void AvoidOff()
	{
		TypeOff(MOVE_TYPE.AVOID);
	}

	public void SeekOff()
	{
		TypeOff(MOVE_TYPE.SEEK);
	}

	public void RotateOff()
	{
		TypeOff(MOVE_TYPE.ROTATE);
	}

	public bool IsStop()
	{
		return TypeIsOn(MOVE_TYPE.STOP);
	}

	public bool IsAvoid()
	{
		return TypeIsOn(MOVE_TYPE.AVOID);
	}

	public bool IsSeek()
	{
		return TypeIsOn(MOVE_TYPE.SEEK);
	}

	public bool IsRotate()
	{
		return TypeIsOn(MOVE_TYPE.ROTATE);
	}

	public void ChangeStopRange(float range)
	{
		saveStopRange = brain.owner.moveStopRange;
		brain.owner.moveStopRange = range;
	}

	public void ResetStopRange()
	{
		if (saveStopRange > 0f)
		{
			brain.owner.moveStopRange = saveStopRange;
		}
		saveStopRange = 0f;
	}

	public void SetStopTime(float time)
	{
		stopTime = time + Time.get_time();
	}

	public void SetSeek(Vector2 stick, Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		stickVec = stick;
		targetPos = pos;
	}

	public void SetTargetPos(Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		targetPos = pos;
	}

	public bool CanSeekToOpponent(Vector3 target_pos, float move_len)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		int obstacleMask = AIUtility.GetObstacleMask();
		return CanSeekToPosition(target_pos, move_len, obstacleMask);
	}

	public bool CanSeekToAlly(Vector3 target_pos, float move_len)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		int mask = AIUtility.GetObstacleMask() | AIUtility.GetOpponentMask(brain.owner);
		return CanSeekToPosition(target_pos, move_len, mask);
	}

	public bool CanSeekToPosition(Vector3 target_pos, float move_len, int mask)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = brain.owner._transform.get_position();
		if (AIUtility.RaycastForTargetPos(position, target_pos, mask, out _seekHit) && _seekHit.get_distance() <= move_len)
		{
			return false;
		}
		return true;
	}

	public void SetAvoid(PLACE place)
	{
		avoidPlace = place;
	}

	public bool CanRightAvoid()
	{
		return CanPlaceAvoid(PLACE.RIGHT);
	}

	public bool CanLeftAvoid()
	{
		return CanPlaceAvoid(PLACE.LEFT);
	}

	public bool CanBackAvoid()
	{
		return CanPlaceAvoid(PLACE.BACK);
	}

	public bool CanFrontAvoid()
	{
		return CanPlaceAvoid(PLACE.FRONT);
	}

	public bool CanPlaceAvoid(PLACE place)
	{
		return !AIUtility.IsHitObstacleOrOpponentWithPlace(brain.owner, place, 2f);
	}

	public void SetRootPosition(Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		rootPosition = pos;
	}
}
