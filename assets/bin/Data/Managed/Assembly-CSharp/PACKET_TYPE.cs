public enum PACKET_TYPE
{
	ERROR_CONNECT_FAILED = -1,
	NONE = 0,
	REGISTER = 1,
	REGISTER_ACK = 2,
	ACK = 3,
	DISCONNECT = 4,
	ALIVE = 5,
	ROOM_ENTRY_CLOSE = 10,
	ROOM_JOINED = 11,
	ROOM_LEAVED = 12,
	ROOM_STAGE_CHANGE = 13,
	ROOM_STAGE_CHANGED = 14,
	ROOM_STAGE_REQUEST = 0xF,
	ROOM_STAGE_REQUESTED = 0x10,
	ROOM_STAGE_HOST_CHANGED = 17,
	BATTLE_START = 20,
	ENEMY_POP = 21,
	ENEMY_ATTACK = 22,
	ENEMY_OUT = 23,
	ENEMY_DEFEAT = 24,
	REWARD_GET = 25,
	REWARD_PICKUP = 26,
	ENEMY_EXTERMINATION = 27,
	UPDATE_BOOST = 28,
	UPDATE_BOOST_COMPLETE = 29,
	ROOM_TIME_CHECK = 30,
	ROOM_TIME_UPDATE = 0x1F,
	ENEMY_BOSS_POP = 0x20,
	WAVEMATCH_INFO = 33,
	EVENT_HAPPEN_QUEST = 40,
	CHAT_MESSAGE = 60,
	PARTY_REGISTER = 71,
	PARTY_REGISTER_ACK = 72,
	PARTY_ROOM_JOINED = 73,
	PARTY_ROOM_LEAVED = 74,
	LOUNGE_REGISTER = 81,
	LOUNGE_REGISTER_ACK = 82,
	LOUNGE_ROOM_ENTRY_CLOSE = 83,
	LOUNGE_ROOM_JOINED = 84,
	LOUNGE_ROOM_LEAVED = 85,
	LOUNGE_ROOM_HOST_CHANGED = 86,
	LOUNGE_ROOM_KICK = 87,
	LOUNGE_ROOM_MOVE = 88,
	LOUNGE_ROOM_POSITION = 89,
	LOUNGE_ROOM_ACTION = 90,
	LOUNGE_ROOM_AFK_KICK = 91,
	LOUNGE_MEMBER_LOUNGE = 95,
	LOUNGE_MEMBER_FIELD = 96,
	LOUNGE_MEMBER_QUEST = 97,
	LOUNGE_MEMBER_ARENA = 98,
	APPLICATION_ROOM_PACKET_START = 100,
	ROOM_KICK = 101,
	ROOM_START = 102,
	CLIENT_STATUS = 103,
	CLIENT_BECAME_HOST = 104,
	CLIENT_LOADING_PROGRESS = 105,
	CLIENT_EMOTION = 106,
	CLIENT_CHANGE_EQUIP = 107,
	CLIENT_BATTLE_RETIRE = 108,
	CLIENT_SERIES_PROGRESS = 109,
	ROOM_UPDATE_PORTAL_POINT = 110,
	ROOM_SYNC_EXPLORE_BOSS = 111,
	ROOM_SYNC_EXPLORE_BOSS_MAP = 112,
	ROOM_EXPLORE_BOSS_DEAD = 113,
	ROOM_NOTIFY_ENCOUNTER_BOSS = 114,
	ROOM_SYNC_PLAYER_STATUS = 115,
	ROOM_CHAT_STAMP = 116,
	ROOM_EXPLORE_BOSS_DAMAGE = 117,
	ROOM_EXPLORE_ALIVE = 118,
	ROOM_EXPLORE_ALIVE_REQUEST = 119,
	ROOM_SYNC_ALL_PORTAL_POINT = 120,
	ROOM_MOVE_FIELD = 121,
	ROOM_RUSH_REQUEST = 122,
	ROOM_RUSH_REQUESTED = 123,
	ROOM_NOTIFY_TRACE_BOSS = 124,
	APPLICATION_STAGE_PACKET_START = 200,
	STAGE_REQUEST = 201,
	STAGE_PLAYER_POP = 202,
	STAGE_ENEMY_POP = 203,
	STAGE_INFO = 204,
	STAGE_RESPONSE_END = 205,
	STAGE_QUEST_CLOSE = 206,
	STAGE_TIMEUP = 207,
	STAGE_CHAT = 208,
	STAGE_CHAT_STAMP = 209,
	OBJECT_DESTROY = 210,
	OBJECT_ATTACKED_HIT_OWNER = 211,
	OBJECT_ATTACKED_HIT_FIX = 212,
	OBJECT_KEEP_WAITING_PACKET = 213,
	CHARACTER_ACTION_TARGET = 214,
	CHARACTER_UPDATE_ACTION_POSITION = 215,
	CHARACTER_UPDATE_DIRECTION = 216,
	CHARACTER_PERIODIC_SYNC_ACTION_POSITION = 217,
	CHARACTER_IDLE = 218,
	CHARACTER_MOVE_VELOCITY = 219,
	CHARACTER_MOVE_VELOCITY_END = 220,
	CHARACTER_MOVE_TO_POSITION = 221,
	CHARACTER_MOVE_HOMING = 222,
	CHARACTER_ROTATE = 223,
	CHARACTER_ROTATE_MOTION = 224,
	CHARACTER_ATTACK = 225,
	CHARACTER_BUFFSYNC = 226,
	CHARACTER_BUFFRECEIVE = 227,
	CHARACTER_BUFFROUTINE = 228,
	CHARACTER_REACTION = 229,
	CHARACTER_REACTION_DELAY = 230,
	PLAYER_LOAD_COMPLETE = 231,
	PLAYER_INITIALIZE = 232,
	PLAYER_ATTACK_COMBO = 233,
	PLAYER_CHARGE_RELEASE = 234,
	PLAYER_AVOID = 235,
	PLAYER_BLOW_CLEAR = 236,
	PLAYER_STUNNED_END = 237,
	PLAYER_DEAD_COUNT = 238,
	PLAYER_DEAD_STANDUP = 239,
	PLAYER_STOP_COUNTER = 240,
	PLAYER_GATHER = 241,
	PLAYER_SKILL_ACTION = 242,
	PLAYER_GET_HEAL = 243,
	PLAYER_SPECIAL_ACTION = 244,
	PLAYER_SHOT_ARROW = 245,
	PLAYER_UPDATE_SKILL_INFO = 246,
	PLAYER_PRAYER_START = 247,
	PLAYER_PRAYER_END = 248,
	PLAYER_CHANGE_WEAPON = 249,
	PLAYER_APPLY_CHANGE_WEAPON = 250,
	PLAYER_SETSTATUS = 251,
	PLAYER_GET_RAREDROP = 252,
	ENEMY_LOAD_COMPLETE = 253,
	ENEMY_INITIALIZE = 254,
	ENEMY_STEP = 0xFF,
	ENEMY_REVIVE_REGION = 0x100,
	ENEMY_WARP = 257,
	ENEMY_TARGRTSHOT_EVENT = 258,
	ENEMY_RANDOMSHOT_EVENT = 259,
	ENEMY_UPDATE_BLEED_DAMAGE = 260,
	APPLICATION_PACKET_END = 261,
	CHARACTER_DEAD = 262,
	STAGE_REQUEST_POP = 263,
	ENEMY_ANGRY = 264,
	CHARACTER_CONTINUS_ATTACK_SYNC = 265,
	PLAYER_GRABBED = 266,
	PLAYER_GRABBED_END = 267,
	ENEMY_RELEASE_GRABBED_PLAYER = 268,
	PLAYER_RESTRAINT = 269,
	PLAYER_RESTRAINT_END = 270,
	ENEMY_SHOT = 271,
	CREATE_ICE_FLOOR = 272,
	ACTION_MINE = 273,
	STAGE_EXPLORE_BOSS_ESCAPE = 274,
	ENEMY_ESCAPE = 275,
	STAGE_SYNC_PLAYER_RECORD = 276,
	ENEMY_RECOVER_HP = 277,
	CHARACTER_MOVE_SIDEWAYS = 278,
	ENEMY_TURN_UP = 279,
	PLAYER_SET_PRESENT_BULLET = 280,
	PLAYER_PICK_PRESENT_BULLET = 281,
	CHARACTER_MOVE_POINT = 282,
	PLAYER_CANNON_STANDBY = 283,
	PLAYER_CANNON_SHOT = 284,
	PLAYER_CANNON_ROTATE = 285,
	PLAYER_GET_CHARGE_SKILLGAUGE = 286,
	PLAYER_SPECIAL_ACTION_CONTINUE = 287,
	PLAYER_SPECIAL_ACTION_GAUGE_SYNC = 288,
	PLAYER_RESURRECT = 289,
	PLAYER_GET_RESURRECT = 290,
	PLAYER_PRAYER_BOOST = 291,
	ENEMY_UPDATE_SHADOWSEALING = 292,
	PLAYER_CHARGE_EXPAND_RELEASE = 293,
	PLAYER_SHOT_ZONE_BULLET = 294,
	PLAYER_JUMP_RIZE = 295,
	PLAYER_JUMP_END = 296,
	PLAYER_SOUL_BOOST = 297,
	PLAYER_SHOT_DECOY_BULLET = 298,
	PLAYER_EXPLODE_DECOY_BULLET = 299,
	PLAYER_WARP = 300,
	PLAYER_EVOLVE_ACTION_SYNC = 301,
	CHARACTER_MOVE_LOOKAT = 302,
	ROOM_SYNC_DEFENSE_BATTLE = 303,
	ENEMY_SYNC_TARGET = 304,
	ENEMY_BOSS_ESCAPE = 305,
	ENEMY_BOSS_ALIVE_REQUEST = 306,
	ENEMY_BOSS_ALIVE_REQUESTED = 307,
	PLAYER_EVOLVE_SPECIAL_ACTION = 308,
	PLAYER_SNATCH_POS = 309,
	PLAYER_SNATCH_MOVE_START = 310,
	PLAYER_SNATCH_MOVE_END = 311,
	PLAYER_PAIR_SWORDS_LASER_END = 312,
	ENEMY_REGION_NODE_ACTIVATE = 313,
	PLAYER_SHOT_HEALING_HOMING = 314,
	OBJECT_BULLET_OBSERVABLE_SET = 315,
	OBJECT_BULLET_OBSERVABLE_BROKEN = 316,
	PLAYER_SHOT_SOUL_ARROW = 317,
	HEARTBEAT = 1000
}
