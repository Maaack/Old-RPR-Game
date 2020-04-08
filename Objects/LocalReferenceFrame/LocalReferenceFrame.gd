extends Control


onready var camera_2d_node = $ViewportContainer/Viewport/Camera2D
onready var viewport_node = $ViewportContainer/Viewport
onready var new_space_node = $ViewportContainer/Viewport/NewSpace
onready var character = $ViewportContainer/Viewport/NewSpace/Character

export(float) var load_in_distance = 10000.0
#export(float, 1.0, 2.0) var load_in_out_ratio
#export(Array, Resource) var persistent_objects
#export(float, 0, 1) var updates_ratio
#export(float, 0, 1) var update_delay_ratio

#func _process(delta):
#	if persistent_objects == null or not persistent_objects is Array:
#		return
#	var updates_goal = floor(persistent_objects.size() * updates_ratio)
#	for i in range(updates_goal):
#		var persistent_object = persistent_objects.pop_back()
#		# Process persisten_object stuff
#		var player_distance = persistent_object.position.length()
#		if player_distance <= load_in_distance:
#			spawn(persistent_object.get_physical_unit())
#		persistent_objects.push_front(persistent_object)

var centered_on
var view_scene_instance

signal world_space_ready

func _ready():
	set_centered_on(character)

func _physics_process(_delta):
	if centered_on != null and is_instance_valid(centered_on):
		camera_2d_node.set_position(centered_on.get_position())
		camera_2d_node.set_rotation(centered_on.get_rotation())
	if new_space_node.physical_space == null:
		return
	for physical_unit in new_space_node.physical_space.physical_quantities:
		var player_distance = physical_unit.position.length()
		if player_distance <= load_in_distance:
			new_space_node.spawn(physical_unit)

func set_centered_on(target:Node2D):
	if target == null:
		return
	centered_on = target

func set_scene_instance(scene_instance:Node2D):
	if view_scene_instance != null:
		view_scene_instance.disconnect("ready", self, "_on_World_Space_ready")
		view_scene_instance.queue_free()
		if not is_instance_valid(view_scene_instance):
			view_scene_instance = null
	viewport_node.add_child(scene_instance)
	view_scene_instance = scene_instance
	return scene_instance



func despawn(node_2d:Node2D):
	pass

