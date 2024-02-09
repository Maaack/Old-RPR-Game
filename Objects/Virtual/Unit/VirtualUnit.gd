extends Resource


class_name VirtualUnit

const READABLE_NAME = 'READABLE_NAME'
const GROUP_NAME_KEY = 'GROUP_NAME'
const DESCRIPTION_KEY = 'DESCRIPTION'

@export var dictionary: Dictionary = {
	READABLE_NAME : 'Readable Name',
	GROUP_NAME_KEY: 'GROUP_NAME',
	DESCRIPTION_KEY: 'Description',
}
@export var icon: Texture2D
@export var size: Vector2 = Vector2(1.0, 1.0)
@export var mass: float
@export var position: Vector2
@export var linear_velocity: Vector2
@export var rotation: float
@export var angular_velocity: float

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
