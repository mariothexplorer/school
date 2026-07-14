// Check to see if the player wants to move left / right
var moveLeft = keyboard_check(ord("A"));
var moveRight = keyboard_check(ord("D"));

// Check to see if the player wants to move up / down
var moveUp = keyboard_check(ord("W"));
var moveDown = keyboard_check(ord("S"));

// Right and left cancel-out
vx = moveRight - moveLeft;
// up and down cancel-out
vy = moveDown - moveUp;

// If we aren't moving
if (vx == 0 && vy == 0)
{
	// Which ever way we were last moving... keep looking that direction.
	if (dir == 0) {
	    sprite_index = spr_player_idle_right;
	} else if (dir == 1) {
		sprite_index = spr_player_idle_up;
	} else if (dir == 2) {
		sprite_index = spr_player_idle_left;
	} else {
		sprite_index = spr_player_idle_down;
	}

// If we are moving
} else {
	// If we are moving sideways
	if (vx != 0) {
		if (vx > 0) {
			dir = 0;
			   sprite_index = spr_player_idle_right;
			//sprite_index = spr_player_walk_right;
		} else {
			dir = 2;
			sprite_index = spr_player_idle_left;
			//sprite_index = spr_player_walk_left;
		}
	// If we are moving vertical
	} else {
		if (vy > 0) {
			dir = 3;
				sprite_index = spr_player_idle_down;
			//sprite_index = spr_player_walk_down;
		} else {
			dir = 1;
			sprite_index = spr_player_idle_up;
			//sprite_index = spr_player_walk_up;
		}
	}
}

// Actually move the character.
x += vx * moveSpeed;
y += vy * moveSpeed;
