var bullet = instance_create_layer(x, y, "Instances", Obj_bullet);

with(bullet) {
direction = point_direction(x, y, mouse_x, mouse_y);
speed = 10;
}
