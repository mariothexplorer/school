/// @description Insert description here
// You can write your code in this editor
var hinput = keyboard_check(ord("D")) - keyboard_check(ord("A"));
var vinput = keyboard_check(ord("S")) - keyboard_check(ord("W"));

if (hinput != 0 || vinput != 0)
{
    var dir = point_direction(0,0,hinput,vinput);
    
    hsp = lengthdir_x(moveSpeed, dir);
    vsp = lengthdir_y(moveSpeed, dir);
}

image_angle = point_direction(x,y,mouse_x,mouse_y);
x += hsp;
y += vsp;

hsp *= fric;
vsp *= fric;