extends Resource


class_name VirtualUnit

const READABLE_NAME = 'READABLE_NAME'
const GROUP_NAME_KEY = 'GROUP_NAME'
const DESCRIPTION_KEY = 'DESCRIPTION'

export(Dictionary) var dictionary = {
	READABLE_NAME : 'Readable Name',
	GROUP_NAME_KEY: 'GROUP_NAME',
	DESCRIPTION_KEY: 'Description',
}
export(Texture) var icon
export(Vector2) var size = Vector2(1.0, 1.0)
export(float) var mass
export(Vector2) var position
export(Vector2) var linear_velocity
export(float) var rotation
export(float) var angular_velocity

func get_name():
	return dictionary[READABLE_NAME]

func get_group_name():
	return dictionary[GROUP_NAME_KEY]

func get_description():
	return dictionary[DESCRIPTION_KEY]

func _to_string():
	return get_group_name() + "(" + str(get_instance_id()) + ")"

func physics_process(delta):
	position += linear_velocity * delta
	rotation += angular_velocity * delta

func get_area():
	return size.x * size.y
