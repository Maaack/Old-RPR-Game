extends RigidBody2D


class_name PhysicalUnit2D

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

@onready var world_space = get_world_space()


func import_virtual_unit(virtual_unit:VirtualUnit):
	dictionary = virtual_unit.dictionary
	icon = virtual_unit.icon
	size = virtual_unit.size
	mass = virtual_unit.mass
	position = virtual_unit.position
	rotation = virtual_unit.rotation
	linear_velocity = virtual_unit.linear_velocity
	angular_velocity = virtual_unit.angular_velocity

func export_virtual_unit():
	var virtual_unit = VirtualUnit.new()
	virtual_unit.dictionary = dictionary
	virtual_unit.icon = icon
	virtual_unit.size = size
	virtual_unit.mass = mass
	virtual_unit.position = position
	virtual_unit.rotation = rotation
	virtual_unit.linear_velocity = linear_velocity
	virtual_unit.angular_velocity = angular_velocity
	return virtual_unit

func get_world_space():
	var parent = get_parent()
	if parent != null and parent.has_method("get_world_space"):
		return get_parent().get_world_space()
	return self

func get_position_in_world_space():
	return get_position_in_ancestor(world_space)

func get_rotation_in_world_space():
	return get_rotation_in_ancestor(world_space)

func get_position_in_ancestor(node:Node2D):
	if node == self:
		return Vector2(0.0, 0.0)
	var parent = get_parent()
	if parent != null and parent.has_method("get_position_in_ancestor"):
		var total_rotation = parent.get_rotation_in_ancestor(node)
		var total_position = position.rotated(total_rotation)
		return parent.get_position_in_ancestor(node) + total_position
	return Vector2(0.0, 0.0)

func get_rotation_in_ancestor(node:Node2D):
	if node == self:
		return 0.0
	var parent = get_parent()
	if parent != null and parent.has_method("get_rotation_in_ancestor"):
		return parent.get_rotation_in_ancestor(node) + rotation
	return 0.0
