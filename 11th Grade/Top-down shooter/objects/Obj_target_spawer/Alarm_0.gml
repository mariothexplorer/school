/// @description Insert description here
// You can write your code in this editor
var target = instance_create_layer(x,y,"intances", Obj_target)

with (target){
target.speed=5
target.direction = point_direction(x,y,Obj_ship.x, Obj_ship.y)
}