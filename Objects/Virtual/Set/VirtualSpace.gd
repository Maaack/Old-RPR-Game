extends PhysicalQuantities


class_name VirtualSpace

var reference_frame setget set_reference_frame

func set_reference_frame(physical_unit:PhysicalUnit):
	if physical_unit == null:
		return
	reference_frame = physical_unit

func reset_to_reference_frame():
	if reference_frame == null or not reference_frame is PhysicalUnit:
		return
	var relative_position = -(reference_frame.position)
	var relative_velocity = -(reference_frame.linear_velocity)
	add_to_positions(relative_position)
	add_to_linear_velocities(relative_velocity)

func physics_process(delta):
	for physical_quantity in physical_quantities:
		physical_quantity.physics_process(delta)

func add_to_positions(relative_position:Vector2):
	if relative_position == null:
		return
	for physical_quantity in physical_quantities:
		physical_quantity.position += relative_position

func add_to_linear_velocities(relative_velocity:Vector2):
	if relative_velocity == null:
		return
	for physical_quantity in physical_quantities:
		physical_quantity.linear_velocity += relative_velocity
	
func add_to_rotations(rotation:float):
	if rotation == null:
		return
	for physical_quantity in physical_quantities:
		physical_quantity.rotation += rotation
	
